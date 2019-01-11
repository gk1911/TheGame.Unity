using gk1911.TheGame.Model;

namespace gk1911.TheGame.Persistence
{
	public class PlayerDataDAO
	{
		public string PersistencePath;

		public void Serialize(PlayerData playerData)
		{

		}

		public PlayerData Deserialize()
		{
			return new PlayerData();
		}
	}
}
