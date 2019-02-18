using System;
using System.Collections.Generic;

using gk1911.TheGame.Model;

namespace gk1911.TheGame.Core
{
	public class BattleController
	{
		public event Action<Map> MapSpawned;
		public event Action<Unit> UnitSpawned;
		public event Action<Unit> UnitSelected;

		private Map _map;
		public Map Map {
			get => _map;
			private set {
				_map = value;
				MapSpawned?.Invoke(Map);
			}
		}

		private Unit _selectedUnit;
		public Unit SelectedUnit {
			get => _selectedUnit;
			set {
				_selectedUnit = value;
				UnitSelected?.Invoke(SelectedUnit);
			}
		}

		private readonly Dictionary<Hex, Unit> UnitsByHex = new Dictionary<Hex, Unit>();

		public void LoadLevel(PlayerData playerData, Level level)
		{
			// spawn the map
			Map = level.Map;
			// spawn all enemies defined in the level
			foreach (KeyValuePair<MapCoordinates, Unit> entry in level.Units) {
				entry.Value.Team = Team.Empire;
				SpawnUnit(entry.Value, entry.Key);
			}
			// spawn all player units defined in the player data
			foreach (KeyValuePair<MapCoordinates, Unit> entry in playerData.Units) {
				entry.Value.Team = Team.Republic;
				SpawnUnit(entry.Value, entry.Key);
				// TEMP
				SelectedUnit = entry.Value;
			}
		}

		/// <summary>
		/// Activate the <see cref="Effect"/> of the <see cref="SelectedUnit"/> at position <paramref name="index"/>.
		/// </summary>
		/// <param name="index"></param>
		public void ActivateEffect(int index) => ActivateEffect(SelectedUnit.Abilities[index], SelectedUnit, SelectedUnit.Target);

		private void ActivateEffect(Effect effect, Unit origin, Unit target)
		{
			target.Hp -= effect.Damage;
		}

		#region public queries
		public Hex GetHex(MapCoordinates cords) => Map.Hexes[cords.Q, cords.R];

		public Hex GetHex(Unit unit)
		{
			foreach (KeyValuePair<Hex, Unit> entry in UnitsByHex) {
				if (entry.Value == unit) {
					return entry.Key;
				}
			}
			return null;
		}

		public Unit GetUnit(Hex hex) => UnitsByHex.TryGetValue(hex, out Unit unit) ? unit : null;

		public bool IsSpawned(Hex hex) => Map.Contains(hex);

		public bool IsSpawned(Unit unit) => UnitsByHex.ContainsValue(unit);
		#endregion

		private void SpawnUnit(Unit unit, MapCoordinates cords)
		{
			Hex hex = GetHex(cords);
			SpawnUnit(unit, hex);
		}

		private void SpawnUnit(Unit unit, Hex hex)
		{
			UnitsByHex.Add(hex, unit);
			UnitSpawned?.Invoke(unit);
		}

	}
}
