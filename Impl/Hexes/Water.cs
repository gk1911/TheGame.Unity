using gk1911.TheGame.Model;

namespace gk1911.TheGame.Impl.Hexes
{
	public class Water : Hex
	{
		public Water(int q, int r) => cords = new MapCoordinates(q, r);
	}
}
