using System;
using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// AssetBundle加载与卸载
    /// </summary>
	public class AssetBundleRes : Res
	{		
		public AssetBundle AssetBundle
		{
			get { return Asset as AssetBundle; }
			set { Asset = value; }
		}

		private string mPath;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="assetName"></param>
		public AssetBundleRes(string assetName)
		{
			mPath = ResKitUtil.FullPathForAssetBundle(assetName);

			Name = assetName;

			State = ResState.Waiting;
		}
		
		private ResLoader mResLoader = new ResLoader();

        /// <summary>
        /// AssetBundle同步加载
        /// </summary>
        /// <returns></returns>
		public override bool LoadSync()
		{
			State = ResState.Loading;//加载状态更新

            var dependencyBundleNames = ResData.Instance.GetDirectDependencies(Name);

			foreach (var dependencyBundleName in dependencyBundleNames)
			{
				mResLoader.LoadSync<AssetBundle>(dependencyBundleName);
			}

			if (!ResMgr.IsSimulationModeLogic)
			{
				AssetBundle = AssetBundle.LoadFromFile(mPath);
			}

			State = ResState.Loaded;//加载状态更新

            return AssetBundle;
		}

		

        /// <summary>
        /// AssetBundle异步加载
        /// </summary>
        public override void LoadAsync()
		{
			State = ResState.Loading;//加载状态更新

            LoadDependencyBundlesAsync(() =>
			{
				if (ResMgr.IsSimulationModeLogic)
				{
					State = ResState.Loaded;//加载状态更新
                }
				else
				{
					var resRequest = AssetBundle.LoadFromFileAsync(mPath);

					resRequest.completed += operation =>
					{
						AssetBundle = resRequest.assetBundle;

						State = ResState.Loaded;//加载状态更新
                    };
				}
			});
		}

        /// <summary>
        /// 异步加载获取依赖
        /// </summary>
        /// <param name="onAllLoaded"></param>
	    private void LoadDependencyBundlesAsync(Action onAllLoaded)
	    {
	        var dependencyBundleNames = ResData.Instance.GetDirectDependencies(Name);//获取依赖

	        var loadedCount = 0;

	        if (dependencyBundleNames.Length == 0)
	        {
	            onAllLoaded();
	        }

	        foreach (var dependencyBundleName in dependencyBundleNames)
	        {
	            mResLoader.LoadAsync<AssetBundle>(dependencyBundleName,
	                dependBundle =>
	                {
	                    loadedCount++;

	                    if (loadedCount == dependencyBundleNames.Length)
	                    {
	                        onAllLoaded();
	                    }
	                });
	        }
	    }

        protected override void OnReleaseRes()
		{
			if (AssetBundle != null)
			{
				AssetBundle.Unload(true);
				AssetBundle = null;
				
				mResLoader.ReleaseAll();
				mResLoader = null;
			}

			ResMgr.Instance.SharedLoadedReses.Remove(this);
		}
	}
}