public class UnitOnMap
{
	public Unit Unit { get; private set; }
	public Hex Hex { get; private set; }

	public UnitOnMap(Unit unit, Hex hex)
	{
		Unit = unit;
		Hex = hex;
	}
}
