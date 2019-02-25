using UnityEngine;

using CGM = gk1911.TheGame.Core.Control.GameManager;

namespace gk1911.TheGame.Unity.Core
{
	internal class GameManager : MonoBehaviour
	{
		internal static InputManager Input { get; private set; }

		private GameManager() { }

		private void Awake() => TrafficLight.Subscribe(this);

		private void Init() => Input = GetComponent<InputManager>();

		private void Begin() => CGM.LoadLevel();
	}
}
