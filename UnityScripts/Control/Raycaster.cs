using UnityEngine;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class Raycaster
	{
		public Transform RaycastFromScreen(Vector3 screenPoint, LayerMask targetLayers, Camera camera = null)
		{
			camera = camera ?? Camera.main;
			Ray ray = camera.ScreenPointToRay(screenPoint);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, targetLayers)) {
				return hitInfo.collider.transform.parent;
			} else {
				return null;
			}
		}
	}
}
