using UnityEngine;

using gk1911.TheGame.Model;
using gk1911.TheGame.Unity.Model.UI;

using CGM = gk1911.TheGame.Core.GameManager;

namespace gk1911.TheGame.Unity.Core
{
	internal class UiManager : MonoBehaviour
	{
		[SerializeField] private UiGroup selectedUi = default;
		[SerializeField] private UiGroup targetUi = default;
		[Space]
		[SerializeField] private GameObject abilityPanel = default;

		private UiManager() { }

		private void Awake() => TrafficLight.Subscribe(this);

		private void Prep()
		{
			Unit.HpChanged += OnHpChanged;
			Unit.TargetChanged += OnTargetChanged;
			CGM.Battle.UnitSelected += OnUnitSelected;
		}

		private void OnHpChanged(Unit unit)
		{
			if (unit == CGM.Battle.SelectedUnit) {
				SetDataPanelValues(this.selectedUi, unit);
			}
			if (unit == CGM.Battle.SelectedUnit.Target) {
				SetDataPanelValues(this.targetUi, unit);
			}
		}

		private void OnTargetChanged(Unit unit)
		{
			if (unit is null) {
				targetUi.DataPanel.SetActive(false);
			} else {
				targetUi.DataPanel.SetActive(true);
				SetDataPanelValues(targetUi, unit);
			}
		}

		private void OnUnitSelected(Unit unit)
		{
			if (unit.Team == Team.Republic) {
				abilityPanel.SetActive(true);
				selectedUi.DataPanel.SetActive(true);
				SetDataPanelValues(selectedUi, unit);
			} else {
				abilityPanel.SetActive(false);
				selectedUi.DataPanel.SetActive(false);
			}
		}

		private void SetDataPanelValues(UiGroup uiGroup, Unit unit)
		{
			uiGroup.HpText.text = $"{unit.Hp} / {unit.MaxHp}";
			float height = uiGroup.HpBar.rect.height;
			float maxWidth = uiGroup.HpPanel.GetComponent<RectTransform>().rect.width;
			float width = unit.HpPercentage * maxWidth / 100;
			uiGroup.HpBar.sizeDelta = new Vector2(width, height);
		}
	}
}
