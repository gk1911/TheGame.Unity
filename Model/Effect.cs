using System;

namespace gk1911.TheGame.Model
{
	public abstract class Effect
	{
		private string _name;
		public string Name {
			get => _name;
			protected set {
				if (value.Length < 2) throw new ArgumentException("Name must be longer than 2 Characters");
				_name = value;
			}
		}

		public int Damage { get; protected set; }
	}
}
