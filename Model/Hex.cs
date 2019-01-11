namespace gk1911.TheGame.Model
{
	public abstract class Hex
	{
		public MapCoordinates cords { get; }

		protected Hex(int q, int r)
		{
			cords = new MapCoordinates(q, r);
		}
	}
}
