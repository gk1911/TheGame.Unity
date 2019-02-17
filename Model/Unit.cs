using System;
using System.Collections.Generic;

namespace gk1911.TheGame.Model
{
	public abstract class Unit
	{
		public static event Action<Unit> TargetChanged;
		public static event Action<Unit> HpChanged;

		protected Unit(string name, float maxHp, List<Effect> abilities = null)
		{
			id = nextId++;
			_name = name;
			_maxHp = maxHp;
			_hp = MaxHp;
			Abilities.AddRange(abilities);
		}

		private static int nextId = 0;
		private readonly int id;

		public Team Team { get; set; }

		private Unit _target;
		public Unit Target {
			get => _target;
			set {
				_target = value;
				TargetChanged?.Invoke(Target);
			}
		}

		private string _name;
		public string Name {
			get => _name;
			set {
				if (value.Length < 2) {
					throw new ArgumentException($"{nameof(Name)} must be longer than 2 Characters");
				}
				_name = value;
			}
		}

		private float _maxHp;
		public float MaxHp {
			get => _maxHp;
			set {
				if (value < 1) throw new ArgumentException($"{nameof(MaxHp)} can't be less than 1");
				_maxHp = value;
				HpChanged?.Invoke(this);
			}
		}

		private float _hp;

		public float Hp {
			get => _hp;
			set {
				if (value < 0) throw new ArgumentException($"{nameof(Hp)} can't be less than 0");
				if (value > MaxHp) throw new ArgumentException($"{nameof(Hp)} can't be higher than {nameof(MaxHp)}");
				_hp = value;
				HpChanged?.Invoke(this);
			}
		}

		public float HpPercentage {
			get => Hp / MaxHp * 100;
			set {
				if (value < 0 || value > 100) throw new ArgumentException($"{nameof(HpPercentage)} must be between 0 and 100");
				Hp = MaxHp * value;
			}
		}

		public List<Effect> Abilities { get; } = new List<Effect>();
	}
}
