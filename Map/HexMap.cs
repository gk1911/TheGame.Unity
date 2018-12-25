using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
	public static HexMap Instance { get; private set; }

	[SerializeField]
	private GameObject[] hexPrefabs;
	[SerializeField]
	private GameObject mainGuyPrefab;

	private readonly int numColumns = 20;
	private readonly int numRows = 20;

	private List<Hex> allHexes = new List<Hex>();
	private List<Unit> allUnits = new List<Unit>();

	private HexMap()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	private void Start()
	{
		GenerateMap();
		SpawnUnitAt(mainGuyPrefab, 5, 5, "MainGuy");
	}

	private void GenerateMap()
	{
		for (int column = 0; column < numColumns; column++) {
			for (int row = 0; row < numRows; row++) {
				SpawnHexTile(new Hex(column, row));
			}
		}
	}

	private void SpawnHexTile(Hex hex)
	{
		GameObject hexTile = Instantiate(hexPrefabs[Random.Range(0, hexPrefabs.Length)], hex.GetPosition(), Quaternion.identity, transform);
		hexTile.name = hex.Q + ", " + hex.R;
		hex.HexGameObject = hexTile;
		allHexes.Add(hex);
	}

	private void SpawnUnitAt(GameObject prefab, int q, int r, string unitName = "Unit")
	{
		Hex hex = GetHexAt(q, r);
		if (hex == default(Hex) || hex.HexGameObject == null) {
			Debug.Log(string.Format("The hex at {0}, {1} does not exist on the map", q, r));
			return;
		}
		GameObject unitGO = Instantiate(prefab, hex.GetPosition(), Quaternion.identity, hex.HexGameObject.transform);
		unitGO.name = unitName;
		Unit unit = new Unit(hex, unitName);
		unit.UnitGameObject = unitGO;
		allUnits.Add(unit);
	}

	public Hex GetHexAt(int q, int r)
	{
		return allHexes.Find(h => h.Q == q && h.R == r);
	}
}
