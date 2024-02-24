using System.Collections.Generic;
using UnityEngine;

namespace Models.Game
{
	public static class TileDataMatrixUtility
	{
		private static void Swap(int x1, int y1, int x2, int y2, TileData[,] tiles)
		{
			(tiles[x1, y1], tiles[x2, y2]) = (tiles[x2, y2], tiles[x1, y1]);
		}

		private static (TileData[], TileData[]) GetConnections(int originX, int originY, TileData[,] tiles)
		{
			var origin = tiles[originX, originY];

			var width = tiles.GetLength(0);
			var height = tiles.GetLength(1);

			var horizontalConnections = new List<TileData>();
			var verticalConnections = new List<TileData>();

			for (var x = originX - 1; x >= 0; x--)
			{
				var other = tiles[x, originY];

				if (other.TypeId != origin.TypeId || other.IsNotSwap || origin.IsNotSwap)
				{
					break;
				}

				if (horizontalConnections.Count + verticalConnections.Count < 2)
				{
					horizontalConnections.Add(other);
				}
			}

			for (var x = originX + 1; x < width; x++)
			{
				var other = tiles[x, originY];

				if (other.TypeId != origin.TypeId || other.IsNotSwap || origin.IsNotSwap) break;

				if (horizontalConnections.Count + verticalConnections.Count < 2)
				{
					horizontalConnections.Add(other);
				}
			}

			for (var y = originY - 1; y >= 0; y--)
			{
				var other = tiles[originX, y];

				if (other.TypeId != origin.TypeId || other.IsNotSwap || origin.IsNotSwap) break;

				if (horizontalConnections.Count + verticalConnections.Count < 2)
				{
					verticalConnections.Add(other);
				}
			}

			for (var y = originY + 1; y < height; y++)
			{
				var other = tiles[originX, y];

				if (other.TypeId != origin.TypeId || other.IsNotSwap || origin.IsNotSwap) break;

				if (horizontalConnections.Count + verticalConnections.Count < 2)
				{
					verticalConnections.Add(other);
				}
			}

			return (horizontalConnections.ToArray(), verticalConnections.ToArray());
		}

		public static Match FindBestMatch(TileData[,] tiles)
		{
			var bestMatch = default(Match);

			for (var y = 0; y < tiles.GetLength(1); y++)
			{
				for (var x = 0; x < tiles.GetLength(0); x++)
				{
					var tile = tiles[x, y];

					var (h, v) = GetConnections(x, y, tiles);

					var match = new Match(tile, h, v);

					if (match.Score < 0) continue;

					if (bestMatch == null)
					{
						bestMatch = match;
					}
					else if (match.Score > bestMatch.Score)
					{
						bestMatch = match;
					}
				}
			}

			return bestMatch;
		}

		private static (int, int) GetDirectionOffset(byte direction) => direction switch
		{
			0 => (-1, 0),
			1 => (0, -1),
			2 => (1, 0),
			3 => (0, 1),

			_ => (0, 0),
		};

		public static Move FindBestMove(TileData[,] tiles)
		{
			var tilesCopy = (TileData[,])tiles.Clone();

			var width = tilesCopy.GetLength(0);
			var height = tilesCopy.GetLength(1);

			var bestScore = int.MinValue;

			var bestMove = default(Move);

			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					for (byte d = 0; d <= 3; d++)
					{
						var (offsetX, offsetY) = GetDirectionOffset(d);

						var x2 = x + offsetX;
						var y2 = y + offsetY;

						if (x2 < 0 || x2 > width - 1 || y2 < 0 || y2 > height - 1)
						{
							continue;
						}

						if (tilesCopy[x2, y2].IsNotSwap || tilesCopy[x, y].IsNotSwap)
						{
							continue;
						}

						Swap(x, y, x2, y2, tilesCopy);

						var match = FindBestMatch(tilesCopy);

						if (match != null && match.Score > bestScore)
						{
							bestMove = new Move(x, y, x2, y2);

							bestScore = match.Score;
						}

						Swap(x, y, x2, y2, tilesCopy);
					}
				}
			}

			return bestMove;
		}
	}
}
