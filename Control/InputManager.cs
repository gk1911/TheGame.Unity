using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using gk1911.TheGame.Unity.Model;
using gk1911.TheGame.Unity.Model.UI;

namespace gk1911.TheGame.Unity.Control
{
	internal class InputManager : MonoBehaviour
	{
		internal event Action<UiButton> ButtonPressed;
		internal event Action<Transform> ClickInput;
		internal event Action<Vector3> DragInput;
		internal event Action<Vector3> ZoomInput;

		[SerializeField] private LayerMask clickableLayers = default;
		[Space]
		[SerializeField] private Camera mainCamera = default;
		[SerializeField] private Button abilityBtn1 = default;
		[SerializeField] private Button abilityBtn2 = default;

		private Mouse mouse;

		private InputManager() { }

		private void Awake() => TrafficLight.Subscribe(this);

		private void Init()
		{
			Input.simulateMouseWithTouches = true;
			Input.multiTouchEnabled = false;
			Input.backButtonLeavesApp = false;
			mouse = new Mouse(mainCamera); ;
		}

		private void Prep()
		{
			abilityBtn1.onClick.AddListener(new UnityAction(() => ButtonPressed?.Invoke(UiButton.Ability1)));
			abilityBtn2.onClick.AddListener(new UnityAction(() => ButtonPressed?.Invoke(UiButton.Ability2)));
		}

		// Update is called once per frame
		private void Update()
		{
			HandleClickInput();
			HandleDragInput();
			HandleZoomInput();
			UpdateMouse();
		}

		private void HandleClickInput()
		{
			// do nothing if player clicked on a UI element
			if (EventSystem.current.IsPointerOverGameObject()) {
				return;
			}
			if (mouse.Up && !mouse.IsDragging) {
				Transform target = RaycastFromScreen(mouse.ScreenPosition, clickableLayers);
				if (target != null) {
					ClickInput?.Invoke(target);
				}
			}

			Transform RaycastFromScreen(Vector3 screenPoint, LayerMask targetLayers)
			{
				Ray ray = mainCamera.ScreenPointToRay(screenPoint);
				if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, targetLayers)) {
					return hitInfo.collider.transform.parent;
				} else {
					return null;
				}
			}
		}

		private void HandleDragInput()
		{
			if (mouse.IsDragging && mouse.WasMoved) {
				DragInput?.Invoke(mouse.LastPosition - mouse.Position);
				mouse.SavePosition();
			}
		}

		private void HandleZoomInput()
		{
			if (Mathf.Abs(mouse.Scrolled) > 0f) {
				Vector3 direction = mainCamera.transform.position - mouse.Position;
				Vector3 zoom = direction * mouse.Scrolled;
				ZoomInput?.Invoke(zoom);
			}
		}

		private void UpdateMouse()
		{
			if (mouse.Up) {
				mouse.IsDragging = false;
			} else if (mouse.Down) {
				mouse.SavePosition();
				// don't initialize dragging if mouse is on a UI element
			} else if (mouse.IsDown && mouse.WasMoved && !EventSystem.current.IsPointerOverGameObject()) {
				mouse.IsDragging = true;
				mouse.SavePosition();
			}
		}
	}
}
