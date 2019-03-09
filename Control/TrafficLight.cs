using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

using UnityEngine;

namespace gk1911.TheGame.Unity.Control
{
	internal class TrafficLight : MonoBehaviour
	{
		private enum Phase { Init, Prep, Begin, }

		private static bool isAcceptingTraffic = true;
		private static readonly List<object> traffic = new List<object>();

		private readonly ReadOnlyCollection<Phase> phases = new ReadOnlyCollection<Phase>((Phase[])Enum.GetValues(typeof(Phase)));
		private readonly BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic
			| BindingFlags.Instance | BindingFlags.Static
			| BindingFlags.DeclaredOnly;

		public static void Subscribe(object roadUser)
		{
			if (isAcceptingTraffic) {
				traffic.Add(roadUser);
			} else {
				throw new InvalidOperationException("The traffic has already been regulated. " +
					"Please subscribe at an earlier point in time.");
			}
		}

		private TrafficLight() { }

		private void Start()
		{
			new List<Phase>(phases).ForEach(phase
			 => traffic.ForEach(roadUser
				 => InvokeMethod(phase, roadUser)));
			isAcceptingTraffic = false;
			traffic.Clear();
			Destroy(this);
		}

		private void InvokeMethod(Phase method, object obj)
			=> obj.GetType().GetMethod(method.ToString(), bindingFlags)?.Invoke(obj, null);
	}
}
