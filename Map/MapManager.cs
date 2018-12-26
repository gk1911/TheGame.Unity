using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public static MapManager Instance { get; private set; }

	[SerializeField]
	private List<GameObject> hexPrefabs;

	private readonly int numColumns = 20;
	private readonly int numRows = 20;

	private GameObject root;
	private List<Hex> hexes = new List<Hex>();

	private MapManager()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	private void Awake()
	{
		root = new GameObject("Map");
		GenerateMap();
	}

	private void Start()
	{
		UnitManager.Instance.SpawnUnit(new Unit("mainGuy"), 5, 5);
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
		GameObject prefab = hexPrefabs[Random.Range(0, hexPrefabs.Count)];
		hex.GameObject = Instantiate(prefab, hex.GetPosition(), Quaternion.identity, root.transform);
		hex.GameObject.name = hex.Q + ", " + hex.R;
	}

	public Hex GetHex(int q, int r)
	{
		return hexes.Find(h => h.Q == q && h.R == r);
	}
}
