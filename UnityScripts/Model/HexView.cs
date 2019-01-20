using UnityEngine;

using gk1911.TheGame.Model;

namespace gk1911.TheGame.UnityScripts.Model
{
	internal class HexView : MonoBehaviour
	{
		[SerializeField]
		private Material newMaterial = default;

		public Hex Hex { get; set; }

		public HexView() { }

		public void ChangeMaterial()
		{
			transform.GetComponentInChildren<Renderer>().material = newMaterial;
		}
	}
}
