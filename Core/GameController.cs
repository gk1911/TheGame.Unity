using System;

using gk1911.TheGame.Model;
using gk1911.TheGame.Persistence;

namespace gk1911.TheGame.Core
{
	public static class GameController
	{
		public static PlayerData PlayerData { get; private set; } = new PlayerData();
		public static BattleController Battle { get; private set; } = new BattleController();

		public static void LoadLevel()
		{
			Level level = LevelManager.GetLevel(PlayerData);
			if (level is null) throw new InvalidOperationException($"Can't load a {nameof(Level)} if {nameof(level)} is null.");
			Battle.LoadLevel(PlayerData, level);
		}
	}
}
