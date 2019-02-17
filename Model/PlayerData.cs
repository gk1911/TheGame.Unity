using System.Collections.Generic;

namespace gk1911.TheGame.Model
{
	public class PlayerData
	{
		public readonly Dictionary<MapCoordinates, Unit> Units = new Dictionary<MapCoordinates, Unit>();
	}
}
