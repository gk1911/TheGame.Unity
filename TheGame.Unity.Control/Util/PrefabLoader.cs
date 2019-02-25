using System.IO;

using UnityEngine;

using gk1911.TheGame.Core.Model;

namespace gk1911.TheGame.Unity.Core.Util
{
	internal class PrefabLoader
	{
		public GameObject LoadPrefab(Unit unit)
			=> (GameObject)Resources.Load(Path.Combine("Prefabs", "Units", unit.GetType().Name));

		public GameObject LoadPrefab(Hex hex)
			=> (GameObject)Resources.Load(Path.Combine("Prefabs", "Hexes", hex.GetType().Name));
	}
}
