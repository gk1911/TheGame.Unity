using System.Collections.Generic;

using gk1911.TheGame.Impl.Effects;
using gk1911.TheGame.Model;

namespace gk1911.TheGame.Impl.Units
{
	public class MainGuy : Unit
	{
		public MainGuy() : base(
			name: "Main Guy the Cool",
			maxHp: 1000,
			abilities: new List<Effect> {
				new Fireball()
			})
		{ }
	}
}
