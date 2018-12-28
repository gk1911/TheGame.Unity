using System;
using System.Collections.Generic;

// TODO: maybe simplify TView to IUnitView
public abstract class Unit<TView> where TView : IUnitView
{
	private static long nextId = 0;
	private readonly long id;

	public Hex Hex { get; private set; }
	public TView View { get; private set; }

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
		Hex = hex;
		try {
			View = MapManager.Instance.Spawn((dynamic) this);
		} catch (InvalidCastException e) {
			throw new InvalidOperationException(string.Format(
				"The IUnitView of the Unit {0} does not align with the IUnitView component on the corresponding prefab", this), e);
		}
	}

	public void AdjustHP(int amount)
	{
		Hp += amount;
	}

	public override bool Equals(object obj)
	{
		var @object = obj as Unit<TView>;
		return @object != null &&
			   id == @object.id &&
			   EqualityComparer<Hex>.Default.Equals(Hex, @object.Hex) &&
			   EqualityComparer<TView>.Default.Equals(View, @object.View);
	}

	public override int GetHashCode()
	{
		var hashCode = -1684316558;
		hashCode = hashCode * -1521134295 + id.GetHashCode();
		hashCode = hashCode * -1521134295 + EqualityComparer<Hex>.Default.GetHashCode(Hex);
		hashCode = hashCode * -1521134295 + EqualityComparer<TView>.Default.GetHashCode(View);
		return hashCode;
	}

	public static bool operator ==(Unit<TView> object1, Unit<TView> object2)
	{
		return EqualityComparer<Unit<TView>>.Default.Equals(object1, object2);
	}

	public static bool operator !=(Unit<TView> object1, Unit<TView> object2)
	{
		return !(object1 == object2);
	}
}
