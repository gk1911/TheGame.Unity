namespace gk1911.TheGame.Model
{
	public abstract class Unit
	{
		public string Name { get; protected set; }
		public int Hp { get; protected set; }

		private static int nextId = 0;
		private readonly int id;

		protected Unit()
		{
			id = nextId++;
		}
	}
}
