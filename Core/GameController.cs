using System;

using gk1911.TheGame.Model;

namespace gk1911.TheGame.Control
{
	public static class GameController
	{
		public static event EventHandler<Level> LevelLoaded;

		public static BattleController Battle { get; private set; }

		public static void LoadLevel(Level level)
		{
			if (level == null) {
				throw new ArgumentException("level can't be null");
			}
			if (Battle != null) {
				throw new InvalidOperationException("Can't load a Level if Battle is not null");
			}
			Battle = new BattleController(level);
			LevelLoaded?.Invoke(null, level);
		}
	}
}
