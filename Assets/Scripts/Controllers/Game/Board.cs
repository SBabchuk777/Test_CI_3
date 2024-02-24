using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using DG.Tweening;

using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

using Models.Game;
using Views.Game;

namespace Controllers.Game
{
	public sealed class Board : MonoBehaviour
	{
		public Action<int> CellsSwappedAction
		{
			get;
			set;
		}

		public Action<int> PressBtnAction
		{
			get;
			set;
		}

		[SerializeField] 
		private TileTypeAsset[] _tileTypes;
		[SerializeField] 
		private Row[] _rows;
		[SerializeField] 
		private float _tweenDuration;
		[SerializeField] 
		private Transform _swappingOverlay;
		[SerializeField] 
		private bool _ensureNoStartingMatches;

		private readonly List<Tile> _selection = new List<Tile>();

		private bool _isSwapping;
		private bool _isMatching;
		private bool _isShuffling;
		private bool _isDestroyingSameCells;
		private bool _canDestroyingSameCells;
		private bool _isDestroyingOneColumn;
		private bool _canDestroyOneColumn;

		public event Action<TileTypeAsset, int> OnMatch;

		public void SetSwappingOverlayTransform(Transform swappingOverlay)
		{
			_swappingOverlay = swappingOverlay;
		}

		public void ChangeCanDestroySameCells()
		{
			_canDestroyingSameCells = true;
		}

		public void ChangeCanDestroyOneColumn()
		{
			_canDestroyOneColumn = true;
		}

		private TileData[,] Matrix
		{
			get
			{
				var width = _rows.Max(row => row.tiles.Length);
				var height = _rows.Length;

				var data = new TileData[width, height];

				for (var y = 0; y < height; y++)
					for (var x = 0; x < width; x++)
						data[x, y] = GetTile(x, y).Data;

				return data;
			}
		}

		private void Start()
		{
			for (var y = 0; y < _rows.Length; y++)
			{
				for (var x = 0; x < _rows.Max(row => row.tiles.Length); x++)
				{
					var tile = GetTile(x, y);

					tile.x = x;
					tile.y = y;

					tile.Type = _tileTypes[Random.Range(0, _tileTypes.Length)];

					tile.button.onClick.AddListener(() => Select(tile));
				}
			}

			if (_ensureNoStartingMatches) StartCoroutine(EnsureNoStartingMatches());

			OnMatch += (type, count) => Debug.Log($"Matched {count}x {type.name}.");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				var bestMove = TileDataMatrixUtility.FindBestMove(Matrix);

				if (bestMove != null)
				{
					Select(GetTile(bestMove.X1, bestMove.Y1));
					Select(GetTile(bestMove.X2, bestMove.Y2));
				}
			}
		}

		private IEnumerator EnsureNoStartingMatches()
		{
			var wait = new WaitForEndOfFrame();

			while (TileDataMatrixUtility.FindBestMatch(Matrix) != null)
			{
				Shuffle();

				yield return wait;
			}
		}

		private Tile GetTile(int x, int y) => _rows[y].tiles[x];

		private Tile[] GetTiles(IList<TileData> tileData)
		{
			var length = tileData.Count;

			var tiles = new Tile[length];

			for (var i = 0; i < length; i++) tiles[i] = GetTile(tileData[i].X, tileData[i].Y);

			return tiles;
		}

		private async void Select(Tile tile)
		{
			if (_isSwapping || _isMatching || _isShuffling || _isDestroyingSameCells || _isDestroyingOneColumn) return;
			
			PressBtnAction.Invoke(0);

			if (_canDestroyingSameCells)
			{
				if (_selection.Count > 0)
				{
					_selection.Clear();
				}

				await DestroySameCells(tile);
			}
			else if(_canDestroyOneColumn)
			{
				if (_selection.Count > 0)
				{
					_selection.Clear();
				}
				
				await DestroyOneColumn(tile);
			}
			else
			{
				if (!_selection.Contains(tile))
				{
					if (_selection.Count > 0)
					{
						if (Math.Abs(tile.x - _selection[0].x) == 1 && Math.Abs(tile.y - _selection[0].y) == 0
						    || Math.Abs(tile.y - _selection[0].y) == 1 && Math.Abs(tile.x - _selection[0].x) == 0)
						{
							_selection.Add(tile);
						}
						else
						{
							_selection.Clear();
						}
					}
					else
					{
						_selection.Add(tile);
					}
				}
			}

			if (_selection.Count < 2) return;

			await SwapAsync(_selection[0], _selection[1]);

			if (!await TryMatchAsync()) await SwapAsync(_selection[0], _selection[1]);

			var matrix = Matrix;

			while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
			{
				Shuffle();

				matrix = Matrix;
			}

			_selection.Clear();
		}

		private async Task SwapAsync(Tile tile1, Tile tile2)
		{
			_isSwapping = true;

			var icon1 = tile1.icon;
			var icon2 = tile2.icon;

			var icon1Transform = icon1.transform;
			var icon2Transform = icon2.transform;

			icon1Transform.SetParent(_swappingOverlay);
			icon2Transform.SetParent(_swappingOverlay);

			icon1Transform.SetAsLastSibling();
			icon2Transform.SetAsLastSibling();

			var sequence = DOTween.Sequence();

			sequence.Join(icon1Transform.DOMove(icon2Transform.position, _tweenDuration).SetEase(Ease.OutBack))
			        .Join(icon2Transform.DOMove(icon1Transform.position, _tweenDuration).SetEase(Ease.OutBack));

			await sequence.Play()
			              .AsyncWaitForCompletion();

			icon1Transform.SetParent(tile2.transform);
			icon2Transform.SetParent(tile1.transform);

			tile1.icon = icon2;
			tile2.icon = icon1;

			(tile1.Type, tile2.Type) = (tile2.Type, tile1.Type);

			_isSwapping = false;
		}

		private async Task<bool> TryMatchAsync()
		{
			var didMatch = false;

			_isMatching = true;

			var match = TileDataMatrixUtility.FindBestMatch(Matrix);

			if (match == null)
			{
				PressBtnAction.Invoke(1);
			}

			while (match != null)
			{
				didMatch = true;

				var tiles = GetTiles(match.Tiles);

				var deflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
					tile.SetSelectedImage(true);
					deflateSequence.Join(tile.icon.transform.DOScale(Vector3.zero, _tweenDuration).SetEase(Ease.InBack));
				}

				CellsSwappedAction.Invoke(0);
				
				await deflateSequence.Play()
				                     .AsyncWaitForCompletion();

				var inflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
					tile.SetSelectedImage(false);
					tile.Type = _tileTypes[Random.Range(0, _tileTypes.Length)];

					inflateSequence.Join(tile.icon.transform.DOScale(Vector3.one, _tweenDuration).SetEase(Ease.OutBack));
				}

				await inflateSequence.Play()
				                     .AsyncWaitForCompletion();

				OnMatch?.Invoke(Array.Find(_tileTypes, tileType => tileType.id == match.TypeId), match.Tiles.Length);

				match = TileDataMatrixUtility.FindBestMatch(Matrix);
			}

			_isMatching = false;

			return didMatch;
		}

		private async Task DestroySameCells(Tile selectedTile)
		{
			_isDestroyingSameCells = true;
			
			List<TileData> tileDataList = new List<TileData>();

			foreach (var row in _rows)
			{ 
				foreach (var tile in row.tiles)
				{
					if (tile.Type.id == selectedTile.Type.id && !tile.IsNotSwap)
					{
						tileDataList.Add(tile.Data);
					}
				}
			}

			if (tileDataList.Count > 0)
			{
				var tiles = GetTiles(tileDataList);
				
				var deflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
					tile.SetSelectedImage(true);
					deflateSequence.Join(tile.icon.transform.DOScale(Vector3.zero, _tweenDuration).SetEase(Ease.InBack));
				}

				await deflateSequence.Play()
					.AsyncWaitForCompletion();

				var inflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
					tile.SetSelectedImage(false);
					tile.Type = _tileTypes[Random.Range(0, _tileTypes.Length)];

					inflateSequence.Join(tile.icon.transform.DOScale(Vector3.one, _tweenDuration).SetEase(Ease.OutBack));
				}

				await inflateSequence.Play()
					.AsyncWaitForCompletion();
				
				var matrix = Matrix;

				while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
				{
					Shuffle();

					matrix = Matrix;
				}
				
				CellsSwappedAction.Invoke(1);
			}

			_isDestroyingSameCells = false;
			_canDestroyingSameCells = false;
		}

		private async Task DestroyOneColumn(Tile selectedTile)
		{
			_isDestroyingOneColumn = true;

			List<TileData> tileDataList = new List<TileData>();

			foreach (var row in _rows)
			{
				foreach (var tile in row.tiles)
				{
					if (tile.x == selectedTile.x && !tile.IsNotSwap)
					{
						tileDataList.Add(tile.Data);
					}
				}
			}

			if (tileDataList.Count > 0)
			{
				var tiles = GetTiles(tileDataList);

				var deflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
					tile.SetSelectedImage(true);
					deflateSequence.Join(tile.icon.transform.DOScale(Vector3.zero, _tweenDuration)
						.SetEase(Ease.InBack));
				}

				await deflateSequence.Play()
					.AsyncWaitForCompletion();

				var inflateSequence = DOTween.Sequence();

				foreach (var tile in tiles)
				{
					tile.SetSelectedImage(false);
					tile.Type = _tileTypes[Random.Range(0, _tileTypes.Length)];

					inflateSequence.Join(tile.icon.transform.DOScale(Vector3.one, _tweenDuration)
						.SetEase(Ease.OutBack));
				}

				await inflateSequence.Play()
					.AsyncWaitForCompletion();

				var matrix = Matrix;

				while (TileDataMatrixUtility.FindBestMove(matrix) == null || TileDataMatrixUtility.FindBestMatch(matrix) != null)
				{
					Shuffle();

					matrix = Matrix;
				}
				
				CellsSwappedAction.Invoke(2);
			}

			_isDestroyingOneColumn = false;
			_canDestroyOneColumn = false;
		}

		private void Shuffle()
		{
			_isShuffling = true;

			foreach (var row in _rows)
				foreach (var tile in row.tiles)
					tile.Type = _tileTypes[Random.Range(0, _tileTypes.Length)];

			_isShuffling = false;
		}
	}
}
