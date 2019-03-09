using UnityEngine;

using CGM = gk1911.TheGame.Core.Control.GameManager;

namespace gk1911.TheGame.Unity.Control
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private InputManager _input = default;
		internal static InputManager Input { get => instance._input; }
		[SerializeField] private BattleManager _battle = default;
		internal static BattleManager Battle { get => instance._battle; }
		[SerializeField] private UiManager _ui = default;
		internal static UiManager UI { get => instance._ui; }
		[SerializeField] private VfxManager _vfx = default;
		public static VfxManager VFX { get => instance._vfx; }

		private static GameManager instance;

		private GameManager() { }

		private void Awake() => TrafficLight.Subscribe(this);

		private void Init() => instance = this;

		private void Begin() => CGM.LoadLevel();
	}
}
