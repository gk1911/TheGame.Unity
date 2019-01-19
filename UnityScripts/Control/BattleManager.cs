using System.Collections.Generic;

using UnityEngine;

using gk1911.TheGame.Model;
using gk1911.TheGame.Control;
using gk1911.TheGame.UnityScripts.Model;
using gk1911.TheGame.UnityScripts.Persistence;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class BattleManager : MonoBehaviour
	{
		private GameObject hexRoot;
		private GameObject unitRoot;

		private readonly List<HexView> hexViews = new List<HexView>();
		private readonly List<UnitView> unitViews = new List<UnitView>();

		private BattleManager() { }

		private void Start()
		{
			hexRoot = GameObject.Find("Map");
			unitRoot = GameObject.Find("Units");
			GameController.LevelLoaded += OnLevelLoaded;
			GameManagers.Input.ClickInput += OnTransformClicked;
		}

		private void OnLevelLoaded(object sender, Level level)
		{
			void InstantiateMap(Map map)
			{
				for (int q = 0; q < map.Columns; q++) {
					for (int r = 0; r < map.Rows; r++) {
						Hex hex = map.Hexes[q, r];
						GameObject prefab = new PrefabLoader().LoadPrefab(hex);
						Vector3 position = GetPosition(hex);
						GameObject gameObject = Instantiate(prefab, position, Quaternion.identity, hexRoot.transform);
						gameObject.name = prefab.name;

						HexView hexView = gameObject.GetComponent<HexView>();
						hexView.Hex = hex;
						hexViews.Add(hexView);
					}
				}
			}

			GameController.Battle.UnitSpawned += OnUnitSpawned;
			GameController.Battle.EffectActivated += OnEffectActivated;

			InstantiateMap(level.Map);
			foreach (Unit unit in level.UnitsByCords.Values) {
				InstantiateUnit(unit);
			}
		}

		private void OnUnitSpawned(object sender, Unit unit)
		{
			InstantiateUnit(unit);
		}

		private void InstantiateUnit(Unit unit)
		{
			Hex hex = GameController.Battle.GetHex(unit);
			GameObject prefab = new PrefabLoader().LoadPrefab(unit);
			Vector3 position = GetPosition(hex);
			GameObject gameObject = Instantiate(prefab, position, Quaternion.identity, unitRoot.transform);
			gameObject.name = prefab.name;

			UnitView unitView = gameObject.GetComponent<UnitView>();
			unitView.Unit = unit;
			unitViews.Add(unitView);
		}

		private void OnTransformClicked(object sender, Transform transform)
		{
			transform.GetComponent<HexView>()?.ChangeMaterial();
		}

		private void OnEffectActivated(object sender, EffectActivatedEventArgs e)
		{

		}

		private Vector3 GetPosition(Hex hex)
		{
			float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
			float radius = 1f;
			float hight = radius * 2;
			float width = WIDTH_MULTIPLIER * hight;

			float vert = hight * 0.75f;
			float horiz = width;

			return new Vector3(horiz * (hex.cords.Q + (hex.cords.R % 2 / 2f)), 0, vert * hex.cords.R);
		}
	}
}
