using System.IO;

using UnityEngine;

using gk1911.TheGame.Core.Model;

namespace gk1911.TheGame.Unity.Control.Util
{
	internal class PrefabLoader
	{
		public GameObject Load(Unit unit)
			=> (GameObject)Resources.Load(Path.Combine("Prefabs", "Units", unit.GetType().Name));

		public GameObject Load(Hex hex)
			=> (GameObject)Resources.Load(Path.Combine("Prefabs", "Hexes", hex.GetType().Name));

		public GameObject LoadVfx(string name)
			=> (GameObject)Resources.Load(Path.Combine("Prefabs", "VFX", name));
	}
}
