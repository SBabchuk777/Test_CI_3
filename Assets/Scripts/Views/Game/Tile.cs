using Models.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
	public sealed class Tile : MonoBehaviour
	{
		[SerializeField] 
		private bool _isNotSwap;
		[SerializeField] 
		private Image _selectedImage;
		
		public int x;
		public int y;

		public Image icon;

		public Button button;

		private TileTypeAsset _type;

		public bool IsNotSwap => _isNotSwap;

		public TileTypeAsset Type
		{
			get => _type;

			set
			{
				if (_type == value) return;

				_type = value;

				icon.sprite = _type.sprite;
				icon.SetNativeSize();
				RectTransform iconRect = icon.GetComponent<RectTransform>();
				icon.raycastTarget = false;
				iconRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,iconRect.rect.width/7f);
				iconRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,iconRect.rect.height/7f);
			}
		}

		public void SetSelectedImage(bool value)
		{
			if (!_isNotSwap)
			{
				_selectedImage.gameObject.SetActive(value);
			}
		}

		public TileData Data => new TileData(x, y, _type.id, _isNotSwap);
	}
}
