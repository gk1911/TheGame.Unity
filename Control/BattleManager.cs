using System.Collections.Generic;

using UnityEngine;

using gk1911.TheGame.Core.Model;

using gk1911.TheGame.Unity.Model;
using gk1911.TheGame.Unity.Model.UI;
using gk1911.TheGame.Unity.Core.Util;

using CGM = gk1911.TheGame.Core.Control.GameManager;

namespace gk1911.TheGame.Unity.Core
{
	internal class BattleManager : MonoBehaviour
	{
		[SerializeField] private GameObject hexRoot = default;
		[SerializeField] private GameObject unitRoot = default;

		private readonly List<HexController> hexControllers = new List<HexController>();

		private BattleManager() { }

		private void Awake() => TrafficLight.Subscribe(this);

		private void Prep()
		{
			CGM.Battle.MapSpawned += InstantiateMap;
			CGM.Battle.UnitSpawned += OnUnitSpawned;
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
					HexController HexController = gameObject.GetComponent<HexController>();
					HexController.Hex = hex;
					hexControllers.Add(HexController);
				}
			}
		}

		private void OnUnitSpawned(Unit unit) => InstantiateUnit(unit);

		private void InstantiateUnit(Unit unit)
		{
			Hex hex = CGM.Battle.GetHex(unit);
			GameObject prefab = new PrefabLoader().LoadPrefab(unit);
			Vector3 position = GetPosition(hex);
			GameObject gameObject = Instantiate(prefab, position, Quaternion.identity, unitRoot.transform);
			gameObject.name = prefab.name;
		}

		private void OnTransformClicked(Transform transform)
		{
			HexController HexController = transform.GetComponent<HexController>();
			if (HexController is null) return;
			Unit unit = CGM.Battle.GetUnit(HexController.Hex);
			CGM.Battle.SelectedUnit.Target = unit ?? null;
		}

		private void OnButtonPressed(UiButton button)
		{
			switch (button) {
				case UiButton.Ability1:
					CGM.Battle.ActivateEffect(0);
					break;
				case UiButton.Ability2:
					CGM.Battle.ActivateEffect(1);
					break;
			}
		}

		private Vector3 GetPosition(Hex hex)
		{
			float radius = 1f;
			float hight = radius * 2;
			float width = Mathf.Sqrt(3) / 2 * hight;
			float vert = hight * 0.75f;
			return new Vector3(width * (hex.cords.Q + (hex.cords.R % 2 / 2f)), 0, vert * hex.cords.R);
		}
	}
}
