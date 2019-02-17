using gk1911.TheGame.UnityScripts.Control.Util;
using UnityEngine;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class CameraManager : MonoBehaviour
	{
		private CameraManager() { }

		private void Awake() => TrafficLight.RoadUsers.Add(this);

		private void Prep()
		{
			GameManager.Input.DragInput += OnMapDragged;
			GameManager.Input.ZoomInput += OnMapZoomed;
		}

		private void OnMapZoomed(Vector3 zoom) => transform.Translate(zoom, Space.World);

		private void OnMapDragged(Vector3 drag) => transform.Translate(drag, Space.World);
	}
}
