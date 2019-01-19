using UnityEngine;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class CameraManager : MonoBehaviour
	{
		private CameraManager() { }

		private void Start()
		{
			GameManagers.Input.DragInput += OnMapDragged;
			GameManagers.Input.ZoomInput += OnMapZoomed;
		}

		private void OnMapZoomed(object sender, Vector3 zoom) => transform.Translate(zoom, Space.World);

		private void OnMapDragged(object sender, Vector3 drag) => transform.Translate(drag, Space.World);
	}
}
