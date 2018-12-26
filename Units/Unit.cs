using UnityEngine;

public class Unit
{
	private GameObject _gameObject;
	public GameObject GameObject
	{
		get { return _gameObject; }
		set {
			// only allow set if GameObject is not a Prefab
			if (_gameObject == null || _gameObject.scene.name != "") {
				_gameObject = value;
			}
		}
	}

	public readonly string name;
	private int hp;

	public Unit(string name)
	{
		this.name = name;
		hp = 100;
	}

	public void DoTurn()
	{
		// move me one tile to the right
		//Hex oldHex = hex;
		//Hex newHex = HexMap.Instance.GetHexAt(oldHex.Q + 1, oldHex.R);
		//hex = newHex;
	}

	public void AdjustHP(int amount)
	{
		hp += amount;
	}
}
