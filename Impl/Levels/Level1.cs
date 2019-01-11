using gk1911.TheGame.Impl.Maps;
using gk1911.TheGame.Impl.Units;
using gk1911.TheGame.Model;

namespace gk1911.TheGame.Impl.Levels
{
	public class Level1 : Level
	{
		public Level1()
		{
			Map = new Level1Map();
			UnitsByCords.Add(new MapCoordinates(5, 5), new MainGuy());
		}
	}
}
