using gk1911.TheGame.Model;

namespace gk1911.TheGame.Persistence
{
	public class PlayerDataDAO
	{
		public string PersistencePath { get; set; }

		public void Serialize(PlayerData playerData)
		{

		}

		public PlayerData Deserialize() => new PlayerData();
	}
}
