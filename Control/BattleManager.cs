using System.Collections.Generic;

using UnityEngine;

using gk1911.TheGame.Core.Model;

using gk1911.TheGame.Unity.Model;
using gk1911.TheGame.Unity.Model.UI;
using gk1911.TheGame.Unity.Control.Util;

using CGM = gk1911.TheGame.Core.Control.GameManager;

namespace gk1911.TheGame.Unity.Control
{
	internal class BattleManager : MonoBehaviour
	{
		[SerializeField] private Transform hexRoot = default;
		[SerializeField] private Transform unitRoot = default;

		public Unit SelectedUnit { get; private set; }

		private readonly List<HexController> hexControllers = new List<HexController>();
		private readonly Dictionary<Unit, GameObject> unitGOs = new Dictionary<Unit, GameObject>();

		private const string castingTrigger = "casting";

		private BattleManager() { }

		private void Awake() => TrafficLight.Subscribe(this);

		private void Prep()
		{
			CGM.Battle.MapSpawned += InstantiateMap;
			CGM.Battle.UnitSpawned += OnUnitSpawned;
			CGM.Battle.UnitSelected += OnUnitSelected;
			CGM.Battle.EffectActivated += OnEffectActivated;
			CGM.Battle.UnitSelected += (unit) => GameManager.UI.Select(unit);
			GameManager.Input.ClickInput += OnTransformClicked;
			GameManager.Input.ButtonPressed += OnButtonPressed;
		}

		private void OnUnitSelected(Unit unit) => SelectedUnit = unit;

		private void InstantiateMap(Map map)
		{
			for (int q = 0; q < map.Columns; q++) {
				for (int r = 0; r < map.Rows; r++) {
					Hex hex = map.Hexes[q, r];
					GameObject prefab = new PrefabLoader().Load(hex);
					Vector3 position = GetPosition(hex);
					GameObject gameObject = Instantiate(prefab, position, Quaternion.identity, hexRoot);
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
			GameObject prefab = new PrefabLoader().Load(unit);
			Vector3 position = GetPosition(hex);
			GameObject gameObject = Instantiate(prefab, position, Quaternion.identity, unitRoot);
			gameObject.name = prefab.name;
			unitGOs.Add(unit, gameObject);
		}

		private void OnEffectActivated(Effect effect, Unit unit)
		{
			Animator animator = unitGOs[unit].GetComponentInChildren<Animator>();
			animator.SetTrigger(castingTrigger);
			if (effect.Damage != 0) {
				GameManager.UI.UpdateHp(unit.Target);
			}
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
