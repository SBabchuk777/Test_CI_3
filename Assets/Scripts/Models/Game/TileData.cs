namespace Models.Game
{
	public readonly struct TileData
	{
		public readonly int X;
		public readonly int Y;

		public readonly int TypeId;

		public readonly bool IsNotSwap;

		public TileData(int x, int y, int typeId, bool isNotSwap)
		{
			X = x;
			Y = y;

			TypeId = typeId;

			IsNotSwap = isNotSwap;
		}
	}
}
