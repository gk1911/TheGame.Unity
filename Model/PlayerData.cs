using System.Collections.Generic;

namespace gk1911.TheGame.Model
{
	public class PlayerData
	{
		public Dictionary<MapCoordinates, Unit> Units { get; } = new Dictionary<MapCoordinates, Unit>();
	}
}
