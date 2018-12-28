using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public static MapManager Instance { get; private set; }

	[SerializeField]
	private List<GameObject> hexPrefabs;

	private readonly int numColumns = 20;
	private readonly int numRows = 20;

	private GameObject hexRoot;
	private GameObject unitRoot;
	private GameObject objectRoot;

	private List<Hex> hexes = new List<Hex>();

	private MapManager()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	private void Awake()
	{
		hexRoot = new GameObject("Map");
		unitRoot = new GameObject("Units");
		objectRoot = new GameObject("Objects");
		GenerateMap();
	}

	private void Start()
	{
		new MainGuy().Spawn(5, 5);
	}

	private void GenerateMap()
	{
		for (int column = 0; column < numColumns; column++) {
			for (int row = 0; row < numRows; row++) {
				SpawnHex(new Hex(column, row));
			}
		}
	}

	private void SpawnHex(Hex hex)
	{
		hexes.Add(hex);
		GameObject prefab = hexPrefabs[UnityEngine.Random.Range(0, hexPrefabs.Count)];
		hex.mapObject.View
			= Instantiate(prefab, hex.GetPosition(), Quaternion.identity, hexRoot.transform).GetComponent<IUnitView>();
		hex.GameObject.name = hex.Q + ", " + hex.R;
	}

	public Hex GetHex(int q, int r)
	{
		return hexes.Find(h => h.Q == q && h.R == r);
	}

	public IUnitView Spawn(Unit<IUnitView> mapObject)
	{
		if (mapObject.Hex == null) {
			throw new ArgumentException(
				"No Hex defined. To spawn a mapObject, please call mapObject.Spawn()", "mapObject");
		}
		mapObject.Hex.mapObject = mapObject;
		return Instantiate(mapObject.View.Prefab, mapObject.Hex.View.gameObject.transform).GetComponent<IUnitView>();
	}
}
