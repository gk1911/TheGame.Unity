using System;

namespace gk1911.TheGame.Model
{
	public abstract class Map
	{
		public abstract int Columns { get; }
		public abstract int Rows { get; }

		public Hex[,] Hexes { get; }

		protected Map()
		{
			if (Columns < 1 || Rows < 1) {
				throw new InvalidOperationException("You need to define the Columns and Rows to be >= 1");
			}
			Hexes = new Hex[Columns, Rows];
		}

		public bool Contains(Hex hex)
		{
			for (int q = 0; q < Columns; q++) {
				for (int r = 0; r < Rows; r++) {
					if (Hexes[q, r] == hex) {
						return true;
					}
				}
			}
			return false;
		}
	}
}
