using UnityEngine;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class CameraMouseManager : MonoBehaviour
	{
		[SerializeField]
		private float minDistanceFromGround = default;
		[SerializeField]
		private float maxDistanceFromGround = default;

		private bool isDraggingCamera = false;
		private Vector3 lastMousePosition;

		// Update is called once per frame
		void Update()
		{
			// camera dragging
			Vector3 mousePos = getMousePosition();
			if (Input.GetMouseButtonDown(0)) {
				isDraggingCamera = true;
				lastMousePosition = mousePos;
			} else if (Input.GetMouseButtonUp(0)) {
				isDraggingCamera = false;
			}
			if (isDraggingCamera) {
				Vector3 diff = lastMousePosition - mousePos;
				Camera.main.transform.Translate(diff, Space.World);
				lastMousePosition = getMousePosition();
			}

			// zooming with scrollwheel
			float amountScrolled = -Input.GetAxis("Mouse ScrollWheel");
			if (Mathf.Abs(amountScrolled) > 0.01f) {
				Vector3 dir = Camera.main.transform.position - getMousePosition();
				Camera.main.transform.Translate(dir * amountScrolled, Space.World);
				// zoom back if threshold reached
				if (Camera.main.transform.position.y < minDistanceFromGround || Camera.main.transform.position.y > maxDistanceFromGround) {
					Camera.main.transform.Translate(-dir * amountScrolled, Space.World);
				}
			}
		}

		private Vector3 getMousePosition()
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayLength = mouseRay.origin.y / mouseRay.direction.y;
			return mouseRay.origin - (mouseRay.direction * rayLength);
		}
	}
}
