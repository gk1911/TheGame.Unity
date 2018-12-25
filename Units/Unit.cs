using UnityEngine;

public class Unit
{
	public GameObject UnitGameObject;

	private Hex hex;
	private string name;
	private int hp;

	public Unit(Hex hex, string name)
	{
		this.hex = hex;
		this.name = name;
		hp = 100;
	}

	public void DoTurn()
	{
		// move me one tile to the right
		Hex oldHex = hex;
		Hex newHex = HexMap.Instance.GetHexAt(oldHex.Q + 1, oldHex.R);
		hex = newHex;
	}

	public void AdjustHP(int amount)
	{
		hp += amount;
	}
}
