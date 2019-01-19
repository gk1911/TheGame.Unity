using UnityEngine;

namespace gk1911.TheGame.UnityScripts.Control
{
	internal static class GameManagers
	{
		public static BattleManager Battle { get; }
		public static InputManager Input { get; }

		static GameManagers()
		{
			GameObject gameManager = GameObject.Find("GameManagers");
			Battle = gameManager.GetComponent<BattleManager>();
			Input = gameManager.GetComponent<InputManager>();
		}
	}
}
