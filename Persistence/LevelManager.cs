using gk1911.TheGame.Model;
using gk1911.TheGame.Impl.Levels;
using gk1911.TheGame.Impl.Units;

namespace gk1911.TheGame.Persistence
{
	public static class LevelManager
	{
		public static Level GetLevel(PlayerData playerData)
		{
			playerData.Units.Add(new MapCoordinates(2, 2), new MainGuy());
			return new Level1();
		}
	}
}
