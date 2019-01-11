using System;

namespace gk1911.TheGame.Model
{
	public class EffectActivatedEventArgs : EventArgs
	{
		public Effect Effect { get; }
		public Unit Origin { get; }
		public Unit Target { get; }

		public EffectActivatedEventArgs(Effect effect, Unit origin, Unit target)
		{
			Effect = effect;
			Origin = origin;
			Target = target;
		}
	}
}
