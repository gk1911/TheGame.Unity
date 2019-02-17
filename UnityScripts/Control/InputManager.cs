using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using gk1911.TheGame.UnityScripts.Model;
using gk1911.TheGame.UnityScripts.Impl.UI;
using gk1911.TheGame.UnityScripts.Control.Util;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class InputManager : MonoBehaviour
	{
		public event Action<UiButton> ButtonPressed;
		public event Action<Transform> ClickInput;
		public event Action<Vector3> DragInput;
		public event Action<Vector3> ZoomInput;

		[SerializeField] private LayerMask clickableLayers = default;
		[Space]
		[SerializeField] private Button abilityBtn1 = default;
		[SerializeField] private Button abilityBtn2 = default;

		private Mouse mouse = new Mouse();

		private InputManager() { }

		private void Awake() => TrafficLight.RoadUsers.Add(this);

		private void Prep()
		{
			Input.simulateMouseWithTouches = true;
			Input.multiTouchEnabled = false;
			Input.backButtonLeavesApp = false;

			abilityBtn1.onClick.AddListener(new UnityAction(delegate { ButtonPressed?.Invoke(UiButton.Ability1); }));
			abilityBtn2.onClick.AddListener(new UnityAction(delegate { ButtonPressed?.Invoke(UiButton.Ability2); }));
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
				Transform target = new Raycaster().RaycastFromScreen(mouse.ScreenPosition, clickableLayers);
				if (target != null) {
					ClickInput?.Invoke(target);
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
				Vector3 direction = Camera.main.transform.position - mouse.Position;
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
