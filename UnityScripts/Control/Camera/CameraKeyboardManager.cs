using UnityEngine;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class CameraKeyboardManager : MonoBehaviour
	{
		[SerializeField]
		private int cameraSpeed = 15;

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			Vector3 translate = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			transform.Translate(translate * cameraSpeed * Time.deltaTime, Space.World);
		}
	}
}
