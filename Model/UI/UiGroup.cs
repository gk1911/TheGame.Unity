using System;

using TMPro;
using UnityEngine;

namespace gk1911.TheGame.Unity.Model.UI
{
	[Serializable]
	public class UiGroup
	{
		public GameObject DataPanel;
		public GameObject HpPanel;
		public RectTransform HpBar;
		public TextMeshProUGUI HpText;
	}
}
