using System.Collections.Generic;

public class MainGuy : Unit<MainGuyView>
{
	public MainGuy()
	{
		Name = "Main Guy the Cool";
		Hp = 100;
	}

	public override bool Equals(object obj)
	{
		var unit = obj as MainGuy;
		return unit != null &&
			   base.Equals(obj) &&
			   Name == unit.Name &&
			   Hp == unit.Hp;
	}

	public override int GetHashCode()
	{
		var hashCode = -1967058303;
		hashCode = hashCode * -1521134295 + base.GetHashCode();
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
		hashCode = hashCode * -1521134295 + Hp.GetHashCode();
		return hashCode;
	}

	public static bool operator ==(MainGuy unit1, MainGuy unit2)
	{
		return EqualityComparer<MainGuy>.Default.Equals(unit1, unit2);
	}

	public static bool operator !=(MainGuy unit1, MainGuy unit2)
	{
		return !(unit1 == unit2);
	}
}
