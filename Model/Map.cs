using System;

namespace gk1911.TheGame.Model
{
	public abstract class Map
	{
		private int _columns;
		public int Columns {
			get => _columns;
			protected set {
				if (value < 1) {
					throw new InvalidOperationException("The number of Columns must be greater than 0");
				}
				_columns = value;
				if (_rows != 0) {
					Hexes = new Hex[_columns, _rows];
				}
			}
		}

		private int _rows;
		public int Rows {
			get => _rows;
			protected set {
				if (value < 1) {
					throw new InvalidOperationException("The number of Rows must be greater than 0");
				}
				_rows = value;
				if (_columns != 0) {
					Hexes = new Hex[_columns, _rows];
				}
			}
		}

		public Hex[,] Hexes { get; private set; }

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
