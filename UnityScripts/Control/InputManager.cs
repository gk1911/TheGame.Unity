using System;

using UnityEngine;

using gk1911.TheGame.UnityScripts.Model;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class InputManager : MonoBehaviour
	{
		public event EventHandler<Transform> ClickInput;
		public event EventHandler<Vector3> DragInput;
		public event EventHandler<Vector3> ZoomInput;

		[SerializeField]
		private LayerMask clickableLayers = default;

		private Mouse mouse = new Mouse();

		private InputManager() { }

		private void Awake()
		{
			Input.simulateMouseWithTouches = true;
			Input.multiTouchEnabled = false;
			Input.backButtonLeavesApp = false;
		}

		// Update is called once per frame
		private void Update()
		{
			HandleClickInput();
			HandleDragInput();
			HandleZoomInput();
		}

		private void HandleClickInput()
		{
			// whenever the left mouse button goes up,
			// it is treated as a click at the position of the screen the mouse is currently at
			// TODO: fix clicking when the button is released after a dragging motion 
			if (mouse.Up) {
				Transform target = new Raycaster().RaycastFromScreen(mouse.ScreenPosition, clickableLayers);
				if (target != null) {
					ClickInput?.Invoke(this, target);
				}
			}
		}

		private void HandleDragInput()
		{
			if (mouse.IsDragging) {
				DragInput?.Invoke(this, mouse.LastPosition - mouse.Position);
				mouse.SavePosition();
			}

			if (mouse.Up) {
				mouse.IsDragging = false;
			} else if (mouse.Down) {
				mouse.SavePosition();
				mouse.IsDragging = true;
			}
		}

		private void HandleZoomInput()
		{
			Vector3 direction = Camera.main.transform.position - mouse.Position;
			Vector3 zoom = direction * mouse.Scrolled;
			ZoomInput?.Invoke(this, zoom);
		}
	}
}
