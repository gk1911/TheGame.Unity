using UnityEngine;

public class WaterView : MonoBehaviour, IHexView
{
	[SerializeField]
	private GameObject Prefab;

	public GameObject GetPrefab()
	{
		return Prefab;
	}

	//private void Awake()
	//{
	//	Prefab = (GameObject) Resources.Load("Prefabs/Units/MainGuy");
	//}
}
