using UnityEngine;

public class MainGuyView : MonoBehaviour, IUnitView
{
	public GameObject Prefab { get; private set; }

	private void Awake()
	{
		Prefab = (GameObject) Resources.Load("Prefabs/Units/MainGuy");
	}
}
