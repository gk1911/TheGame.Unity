using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public static MapManager Instance { get; private set; }

	private readonly int numColumns = 20;
	private readonly int numRows = 20;

	private GameObject hexRoot;
	private GameObject unitRoot;

	private List<Hex> hexes = new List<Hex>();
	private List<Unit> units = new List<Unit>();

	private MapManager() { }

	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		}
		hexRoot = new GameObject("Hexes");
		unitRoot = new GameObject("Units");
		GenerateMap();
	}

	private void Start()
	{
		InputManager.Instance.SpacePressed += MoveUnit;

		//new MainGuy().Spawn(5, 5);
	}

	private void GenerateMap()
	{
		// go through columns and rows
		for (int q = 0; q < numColumns; q++) {
			for (int r = 0; r < numRows; r++) {
				// spawn a grassland hex or a water hex randomly
				switch (UnityEngine.Random.Range(0, 1)) {
					case 0:
						new Grassland(q, r).Spawn();
						break;
					case 1:
						new Water(q, r).Spawn();
						break;
				}
			}
		}
	}

	public Hex GetHex(int q, int r)
	{
		return hexes.Find(h => h.Q == q && h.R == r);
	}

	// i need the view to know which prefab to spawn,
	// but i need an already spawned gameObject to get a view instance...
	public IHexView SpawnHex(Hex hex)
	{
		// error handling, ...

		hexes.Add(hex);
		return Instantiate(hex.View.GetPrefab(), hexRoot.transform).GetComponent<IHexView>();
	}

	public IUnitView SpawnUnit(Unit unit)
	{
		if (unit.Hex == null) {
			throw new ArgumentException(
				"No Hex defined. To spawn a Unit, please call unit.Spawn()", "unit");
		}

		units.Add(unit);
		unit.Hex.SetUnit(unit);
		return Instantiate(unit.View.Prefab, unitRoot.transform).GetComponent<IUnitView>();
	}

	private void MoveUnit(object sender, EventArgs e)
	{
		Unit unit = new MainGuy();
		Debug.Log(string.Format("Someones gotta move. {0}", unit));
	}
}
