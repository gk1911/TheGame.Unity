using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;

namespace gk1911.TheGame.Unity.Core
{
	internal class TrafficLight : MonoBehaviour
	{
		private enum Phase { Init, Prep, Begin, }

		private static readonly List<object> roadUsers = new List<object>();
		private readonly IReadOnlyList<Phase> phases = ((Phase[])Enum.GetValues(typeof(Phase))).ToList();
		private readonly BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic
			| BindingFlags.Instance | BindingFlags.Static
			| BindingFlags.DeclaredOnly;

		public static void Subscribe(object roadUser) => roadUsers.Add(roadUser);

		private TrafficLight() { }

		private void Start()
		{
			((List<Phase>)phases).ForEach(phase
			 => roadUsers.ForEach(roadUser
				 => InvokeMethod(phase, roadUser)));
			roadUsers.Clear();
		}

		private void InvokeMethod(Phase method, object obj)
			=> obj.GetType().GetMethod(method.ToString(), bindingFlags)?.Invoke(obj, null);
	}
}
