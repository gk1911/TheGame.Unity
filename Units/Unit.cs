using UnityEngine;

public class Unit
{
	public GameObject UnitGameObject;

	private string name;
	private int hp;

	public Unit(string name)
	{
		this.name = name;
		hp = 100;
	}

	public void AdjustHP(int amount)
	{
		hp += amount;
	}
}
