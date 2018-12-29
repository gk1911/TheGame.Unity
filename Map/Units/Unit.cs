using System;

public abstract class Unit
{
	private static long nextId = 0;
	private readonly long id;

	public Hex Hex { get; private set; }
	public IUnitView View { get; private set; }

	public string Name { get; protected set; }
	public int Hp { get; protected set; }

	protected Unit()
	{
		id = nextId++;
	}

	public void Spawn(int q, int r)
	{
		Hex hex = MapManager.Instance.GetHex(q, r);
		Spawn(hex);
	}

	public void Spawn(Hex hex)
	{
		if (Hex != null) {
			throw new InvalidOperationException(string.Format("The Unit {0} is already spawned", this));
		}
		//if (!MapManager.Instance.units.Contains(hex)) {
		//	throw new ArgumentException(string.Format("The Hex {0} is not part of the instantiated map", hex), "hex");
		//}
		if (hex.Unit != null) {
			throw new InvalidOperationException(string.Format("The Hex {0} already has a Unit", hex));
		}

		Hex = hex;
		View = MapManager.Instance.SpawnUnit(this);
		if (View == null) {
			throw new InvalidOperationException("You tried to spawn a Unit which does not have an IUnitView on its prefab");
		}
	}

	public void AdjustHP(int amount)
	{
		Hp += amount;
	}
}
