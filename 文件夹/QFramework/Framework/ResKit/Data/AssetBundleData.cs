using System.Collections.Generic;

namespace QFramework
{
	public class AssetBundleData
	{
		public string Name;
		
		public List<AssetData> AssetDataList = new List<AssetData>();

		public string[] DependencyBundleNames;
	}
}