using System;
using System.Collections.Generic;

using gk1911.TheGame.Model;

namespace gk1911.TheGame.Control
{
	public class BattleController
	{
		public event EventHandler<Unit> UnitSpawned;
		public event EventHandler<EffectActivatedEventArgs> EffectActivated;

		private readonly Map map;
		private readonly Dictionary<Hex, Unit> unitsByHex = new Dictionary<Hex, Unit>();

		public BattleController(Level level)
		{
			// spawn the map
			this.map = level.Map;
			// spawn all units defined in the level
			foreach (KeyValuePair<MapCoordinates, Unit> entry in level.UnitsByCords) {
				SpawnUnit(entry.Value, entry.Key);
			}
		}

		private void SpawnUnit(Unit unit, MapCoordinates cords)
		{
			Hex hex = GetHex(cords);
			SpawnUnit(unit, hex);
		}

		private void SpawnUnit(Unit unit, Hex hex)
		{
			unitsByHex.Add(hex, unit);
			UnitSpawned?.Invoke(this, unit);
		}

		#region public queries
		public Hex GetHex(MapCoordinates cords) => map.Hexes[cords.Q, cords.R];

		public Hex GetHex(Unit unit)
		{
			foreach (KeyValuePair<Hex, Unit> entry in unitsByHex) {
				if (entry.Value == unit) {
					return entry.Key;
				}
			}
			return null;
		}

		public Unit GetUnit(Hex hex) => unitsByHex[hex];

		public bool IsSpawned(Hex hex) => map.Contains(hex);

		public bool IsSpawned(Unit unit) => unitsByHex.ContainsValue(unit);
		#endregion
	}
}
