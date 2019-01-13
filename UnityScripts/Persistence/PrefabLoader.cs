using System.IO;

using UnityEngine;

using gk1911.TheGame.Model;

namespace gk1911.TheGame.UnityScripts.Persistence
{
	internal class PrefabLoader
	{
		public GameObject LoadPrefab(Unit unit)
		{
			return (GameObject) Resources.Load(Path.Combine("Prefabs", "Units", unit.GetType().Name));
		}

		public GameObject LoadPrefab(Hex hex)
		{
			return (GameObject) Resources.Load(Path.Combine("Prefabs", "Hexes", hex.GetType().Name));
		}
	}
}
