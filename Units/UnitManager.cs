using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all existing <see cref="Unit"/> and <see cref="UnitView"/> instances.
/// </summary>
public class UnitManager : MonoBehaviour
{
	public static UnitManager Instance { get; private set; }

	[SerializeField]
	private List<GameObject> unitPrefabs;

	private GameObject root;
	private List<Unit> units = new List<Unit>();
	private List<UnitOnMap> unitsOnMap = new List<UnitOnMap>();

	private UnitManager()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	private void Awake()
	{
		root = new GameObject("Units");
	}

	/// <summary>
	/// Spawns a <see cref="Unit"/> at the target position defined by <paramref name="q"/> and <paramref name="r"/>.
	/// </summary>
	/// <param name="unit">The unit to spawn</param>
	/// <param name="q">The column of the target hex</param>
	/// <param name="r">The row of the target hex</param>
	/// <returns>Whether the unit could be spawned</returns>
	public bool SpawnUnit(Unit unit, int q, int r)
	{
		Hex hex = MapManager.Instance.GetHex(q, r);
		if (hex == default(Hex)) {
			return false;
		}
		units.Add(unit);
		unitsOnMap.Add(new UnitOnMap(unit, hex));
		unit.GameObject = Instantiate(unitPrefabs[0], hex.GetPosition(), Quaternion.identity, root.transform);
		unit.GameObject.name = unit.name;
		return true;
	}

	/// <summary>
	/// Destroys a <see cref="Unit"/> and its <see cref="GameObject"/>.
	/// </summary>
	/// <param name="unit">The unit to destroy</param>
	/// <returns>Whether the unit could be destroyed</returns>
	public bool DestroyUnit(Unit unit)
	{
		if (!units.Contains(unit) || unit.GameObject == null) {
			return false;
		}
		unitsOnMap.RemoveAll(unitOnMap => unitOnMap.Unit == unit);
		Destroy(unit.GameObject);
		return units.Remove(unit);
	}

	public bool MoveUnit(Unit unit, int q, int r)
	{
		if (unit == null) {
			unit = units[0];
		}

		Hex oldHex = unitsOnMap.Find(unitOnMap => unitOnMap.Unit == unit).Hex;
		Hex newHex = MapManager.Instance.GetHex(oldHex.Q + q, oldHex.R + r);
		if (!units.Contains(unit) || newHex == default(Hex)) {
			return false;
		}
		unitsOnMap.RemoveAll(unitOnMap => unitOnMap.Unit == unit);
		unitsOnMap.Add(new UnitOnMap(unit, newHex));
		unit.GameObject.transform.position = newHex.GameObject.transform.position;

		return true;
	}
}
