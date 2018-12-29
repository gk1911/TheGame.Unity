using UnityEngine;

public class GrasslandView : MonoBehaviour, IHexView
{
	[SerializeField]
	private GameObject Prefab;

	public GameObject GetPrefab()
	{
		return Prefab;
	}
}
