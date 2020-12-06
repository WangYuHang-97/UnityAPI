using UnityEngine;
using UnityEngine.UI;

namespace ShootResources
{
	public partial class Ept_Turnplate
	{
		[SerializeField] public UnityEngine.RectTransform Turnplate;
        [SerializeField] public Text Text_Item;

		public void Clear()
		{
			Turnplate = null;
		}

		public  string ComponentName
		{
			get { return "Ept_Turnplate";}
		}
	}
}
