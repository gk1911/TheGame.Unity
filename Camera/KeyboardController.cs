using UnityEngine;

public class KeyboardController : MonoBehaviour
{
	[SerializeField]
	private int cameraSpeed;

	// Update is called once per frame
	private void Update()
	{
		Vector3 translate = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		transform.Translate(translate * cameraSpeed * Time.deltaTime, Space.World);
	}
}
