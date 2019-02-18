using System;
using System.Linq;

namespace gk1911.TheGame.Model
{
	public class MapCoordinates
	{
		// Q + R + S = 0
		// S = -(Q + R)
		public int Q { get; } // Column
		public int R { get; } // Row
		public int S { get; }

		public MapCoordinates(int q, int r)
		{
			Q = q;
			R = r;
			S = -(q + r);
		}

		public int GetDistance(MapCoordinates cords)
			=> new int[] { Math.Abs(Q - cords.Q), Math.Abs(R - cords.R), Math.Abs(S - cords.S) }.Max();
	}
}
