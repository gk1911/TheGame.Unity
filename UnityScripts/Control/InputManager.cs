using UnityEngine;
using System;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal class InputManager : MonoBehaviour
	{
		public static InputManager Instance { get; private set; }

		public event EventHandler SpacePressed;

		private InputManager() { }

		private void Awake()
		{
			if (Instance == null) {
				Instance = this;
			}
		}

		// Update is called once per frame
		private void Update()
		{
			if (Input.GetButtonDown("Jump")) {
				Debug.Log("Space pressed");
				SpacePressed?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
