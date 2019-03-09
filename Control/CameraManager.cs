using UnityEngine;

namespace gk1911.TheGame.Unity.Control
{
	internal class CameraManager : MonoBehaviour
	{
		[SerializeField] private new Camera camera = default;

		private CameraManager() { }

		private void Awake() => TrafficLight.Subscribe(this);

		private void Prep()
		{
			GameManager.Input.DragInput += drag => camera.transform.Translate(drag, Space.World);
			GameManager.Input.ZoomInput += zoom => camera.transform.Translate(zoom, Space.World);
		}
	}
}
