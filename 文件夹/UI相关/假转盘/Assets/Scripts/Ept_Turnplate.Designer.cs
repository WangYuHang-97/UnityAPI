using UnityEngine;

namespace ShootResources
{
	public partial class Ept_Turnplate
	{
		[SerializeField] public UnityEngine.RectTransform Turnplate;

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
