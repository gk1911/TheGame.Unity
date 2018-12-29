using UnityEngine;
using System;

public class InputManager : MonoBehaviour
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
			SpacePressed(this, EventArgs.Empty);
		}
	}
}
