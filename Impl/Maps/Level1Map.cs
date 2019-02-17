using System;

using gk1911.TheGame.Model;
using gk1911.TheGame.Impl.Hexes;

namespace gk1911.TheGame.Impl.Maps
{
	public class Level1Map : Map
	{
		public Level1Map()
		{
			Columns = 10;
			Rows = 10;
			GenerateMap();
		}

		private void GenerateMap()
		{
			Random random = new Random();
			for (int q = 0; q < Columns; q++) {
				for (int r = 0; r < Rows; r++) {
					int rnd = random.Next(0, 2);
					if (rnd == 0) {
						Hexes[q, r] = new Grassland(q, r);
					} else {
						Hexes[q, r] = new Water(q, r);
					}
				}
			}
		}
	}
}
