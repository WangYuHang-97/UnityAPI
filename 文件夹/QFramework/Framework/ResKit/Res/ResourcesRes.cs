using System;
using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// 资源的加载与卸载
    /// </summary>
    public class ResourcesRes : Res
    {
        private string mAssetPath;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="assetPath"></param>
        public ResourcesRes(string assetPath)
        {
            mAssetPath = assetPath.Substring("resources://".Length);//去除resources://

            Name = assetPath;//Name赋值

            State = ResState.Waiting;//加载状态更新
        }

        /// <summary>
        /// Resources同步加载
        /// </summary>
        /// <returns></returns>
        public override bool LoadSync()
        {
            State = ResState.Loading;//加载状态更新

            Asset = Resources.Load(mAssetPath);//资源加载

            State = ResState.Loaded;//加载状态更新

            return Asset;
        }

        /// <summary>
        /// Resources异步加载
        /// </summary>
        public override void LoadAsync()
        {
            State = ResState.Loading;//加载状态更新

            var resRequest = Resources.LoadAsync(mAssetPath);//资源异步加载

            resRequest.completed += operation =>
            {
                Asset = resRequest.asset;

                State = ResState.Loaded;                
            };
        }

        /// <summary>
        /// Resources卸载
        /// </summary>
        protected override void OnReleaseRes()
        {
            if (Asset is GameObject)
            {
                Asset = null;
                
                Resources.UnloadUnusedAssets();
            }
            else
            {
                Resources.UnloadAsset(Asset);
            }

            ResMgr.Instance.SharedLoadedReses.Remove(this);
                
            Asset = null; 
        }
    }
}