using System.Linq;

using UnityEngine;

using gk1911.TheGame.Unity.Control;

namespace gk1911.TheGame.Unity.Impl.MainGuy
{
	public class MainGuyAbilities : MonoBehaviour
	{
		public void CastFireball()
		{
			Vector3 fingertips = transform.GetComponentsInChildren<Transform>()
				.First(child => child.name == "mixamorig_LeftHandIndex4").position;
			GameManager.VFX.Spawn("Fireball", fingertips);
		}
	}
}