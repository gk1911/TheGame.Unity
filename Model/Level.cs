using System.Collections.Generic;

namespace gk1911.TheGame.Model
{
	public abstract class Level
	{
		public Map Map { get; protected set; }
		public Dictionary<MapCoordinates, Unit> Units { get; } = new Dictionary<MapCoordinates, Unit>();
	}
}
