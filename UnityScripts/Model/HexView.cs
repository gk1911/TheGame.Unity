using UnityEngine;

using gk1911.TheGame.Model;

namespace gk1911.TheGame.UnityScripts.Model
{
	internal class HexView : MonoBehaviour
	{
		public Material newMaterial;

		public Hex Hex { get; set; }

		public HexView() { }

		public void ChangeMaterial()
		{
			transform.GetComponentInChildren<Renderer>().material = newMaterial;
		}
	}
}
