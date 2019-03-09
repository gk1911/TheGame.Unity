using UnityEngine;

using gk1911.TheGame.Unity.Control.Util;

namespace gk1911.TheGame.Unity.Control
{
	public class VfxManager : MonoBehaviour
	{
		[SerializeField] private Transform vfxRoot = default;

		public void Spawn(string name, Vector3 position)
		{
			GameObject particlePrefab = new PrefabLoader().LoadVfx(name);
			Instantiate(particlePrefab, position, Quaternion.identity, vfxRoot).name = name;
		}
	}
}
