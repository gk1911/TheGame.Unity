using UnityEngine;

public class Unit
{
	private static long nextId = 0;
	public long Id { get; private set; }

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
