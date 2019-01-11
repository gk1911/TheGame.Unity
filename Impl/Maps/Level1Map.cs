using System;

using gk1911.TheGame.Model;
using gk1911.TheGame.Impl.Hexes;

namespace gk1911.TheGame.Impl.Maps
{
	public class Level1Map : Map
	{
		public override int Columns { get; } = 10;
		public override int Rows { get; } = 10;

		public Level1Map()
		{
			Random random = new Random();
			for (int q = 0; q < Columns; q++) {
				for (int r = 0; r < Rows; r++) {
					if (random.Next(0, 2) == 0) {
						Hexes[q, r] = new Grassland(q, r);
					} else {
						Hexes[q, r] = new Water(q, r);
					}
				}
			}
		}
	}
}
