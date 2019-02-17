using System.Collections.Generic;

using UnityEngine;

using gk1911.TheGame.Model;
using gk1911.TheGame.Control;
using gk1911.TheGame.UnityScripts.Model;
using gk1911.TheGame.UnityScripts.Persistence;
using gk1911.TheGame.UnityScripts.Impl.UI;
using gk1911.TheGame.UnityScripts.Control.Util;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class BattleManager : MonoBehaviour
	{
		[SerializeField] private GameObject hexRoot = default;
		[SerializeField] private GameObject unitRoot = default;

		private readonly List<HexView> hexViews = new List<HexView>();
		private readonly List<UnitView> unitViews = new List<UnitView>();

		private BattleManager() { }

		private void Awake() => TrafficLight.RoadUsers.Add(this);

		private void Prep()
		{
			BattleController.MapSpawned += InstantiateMap;
			BattleController.UnitSpawned += OnUnitSpawned;
			GameManager.Input.ClickInput += OnTransformClicked;
			GameManager.Input.ButtonPressed += OnButtonPressed;
		}
		
		private void InstantiateMap(Map map)
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

		private void OnUnitSpawned(Unit unit) => InstantiateUnit(unit);

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

		private void OnTransformClicked(Transform transform)
		{
			HexView hexView = transform.GetComponent<HexView>();
			if (hexView is null) return;

			Unit unit = GameController.Battle.GetUnit(hexView.Hex);
			GameController.Battle.SelectedUnit.Target = unit ?? null;
		}

		private void OnButtonPressed(UiButton button)
		{
			switch (button) {
				case UiButton.Ability1:
					GameController.Battle.ActivateEffect(0);
					break;
				case UiButton.Ability2:
					GameController.Battle.ActivateEffect(1);
					break;
			}
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
