using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
	[SerializeField]
	private GameObject[] hexPrefabs;
	[SerializeField]
	private GameObject mainGuyPrefab;

	private readonly int numColumns = 20;
	private readonly int numRows = 20;

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
		Instantiate(hexPrefabs[Random.Range(0, hexPrefabs.Length)], hex.Position(), Quaternion.identity, transform)
			.name = hex.Q + ", " + hex.R;
	}

	public void SpawnUnitAt(GameObject prefab, int q, int r, string name = "Unit")
	{
		Instantiate(prefab, new Hex(q, r).Position(), Quaternion.identity, transform).name = name;
	}
}
