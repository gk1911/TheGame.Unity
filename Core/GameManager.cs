using System;

using gk1911.TheGame.Model;
using gk1911.TheGame.Persistence;

namespace gk1911.TheGame.Core
{
	public static class GameManager
	{
		public static PlayerData PlayerData { get; } = new PlayerData();
		public static BattleManager Battle { get; } = new BattleManager();

		public static void LoadLevel()
		{
			Level level = LevelManager.GetLevel(PlayerData);
			if (level is null) throw new InvalidOperationException($"Can't load a {nameof(Level)} if {nameof(level)} is null.");
			Battle.LoadLevel(PlayerData, level);
		}
	}
}
