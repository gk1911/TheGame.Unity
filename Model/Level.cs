using System.Collections.Generic;

namespace gk1911.TheGame.Model
{
	public abstract class Level
	{
		public Map Map { get; protected set; }
		public readonly Dictionary<MapCoordinates, Unit> UnitsByCords = new Dictionary<MapCoordinates, Unit>();
	}
}
