using UnityEngine;

using gk1911.TheGame.Control;
using gk1911.TheGame.Impl.Levels;

namespace gk1911.TheGame.UnityScripts.Control
{
	public class TempLevelLoader : MonoBehaviour
	{
		private TempLevelLoader() { }

		private void Start()
		{
			GameController.LoadLevel(new Level1());
		}
	}
}
