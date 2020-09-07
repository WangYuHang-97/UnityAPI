using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace QFramework
{
	public class ResLoader
	{
		#region API		
		public T LoadSync<T>(string assetBundleName, string assetName) where T : Object
		{
			return DoLoadSync<T>(assetName, assetBundleName);
		}
		
        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetName">资源名称</param>
        /// <returns></returns>
		public T LoadSync<T>(string assetName) where T : Object
		{
			return DoLoadSync<T>(assetName);

		}

		public void LoadAsync<T>(string assetName, Action<T> onLoaded) where T : Object
		{
			DoLoadAsync(assetName, null, onLoaded);
		}

		public void LoadAsync<T>(string assetBundleName, string assetName, Action<T> onLoaded) where T : Object
		{
			DoLoadAsync(assetName, assetBundleName, onLoaded);
		}
		
		public void ReleaseAll()
		{
			mResRecord.ForEach(loadedAsset => loadedAsset.Release());

			mResRecord.Clear();
		}

		
		#endregion

		
		#region Private		
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">//资源名称</param>
        /// <param name="assetBundleName"></param>
        /// <returns></returns>
		private T DoLoadSync<T>(string assetName, string assetBundleName = null) where T : Object
		{
			var res = GetOrCreateRes(assetName);

			if (res != null)
			{
				switch (res.State)
				{
					case ResState.Loading://处于正在加载阶段
						throw new Exception(string.Format("请不要在异步加载资源 {0} 时，进行 {0} 的同步加载", res.Name));
					case ResState.Loaded://加载完成
						return res.Asset as T;
				}
			}

			// 真正加载资源
			res = CreateRes(assetName,assetBundleName);

			res.LoadSync();

			return res.Asset as T;
		}

		private void DoLoadAsync<T>(string assetName, string ownerBundleName, Action<T> onLoaded) where T : Object
		{
			// 查询当前的 资源记录
			var res = GetOrCreateRes(assetName);

			Action<Res> onResLoaded = null;

			onResLoaded = loadedRes =>
			{
				onLoaded(loadedRes.Asset as T);//触发事件

				res.UnRegisterOnLoadedEvent(onResLoaded);
			};

			if (res != null)
			{
				if (res.State == ResState.Loading)//处于正在加载阶段
                {
					res.RegisterOnLoadedEvent(onResLoaded); //加入Load队列
                }
				else if (res.State == ResState.Loaded)
				{
					onLoaded(res.Asset as T);
				}

				return;
			}

			// 真正加载资源
			res = CreateRes(assetName, ownerBundleName);

			res.RegisterOnLoadedEvent(onResLoaded);//加入Load队列

            res.LoadAsync();
		}


		private List<Res> mResRecord = new List<Res>();

        /// <summary>
        /// 若资源记录或全局资源中含有该资源就返回，否则创建
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
		private Res GetOrCreateRes(string assetName)
		{
			// 查询当前的 资源记录
			var res = GetResFromRecord(assetName);

			if (res != null)
			{
				return res;
			}

			// 查询全局资源池
			res = GetFromResMgr(assetName);

			if (res != null)
			{
				AddRes2Record(res);

				return res;
			}

			return res;
		}

        /// <summary>
        /// 创建资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="ownerBundle"></param>
        /// <returns></returns>
		private Res CreateRes(string assetName, string ownerBundle = null)
		{
			var res = ResFactory.Create(assetName, ownerBundle);//根据工厂子类型获得资源

			ResMgr.Instance.SharedLoadedReses.Add(res);//在全局资源池中加入

			AddRes2Record(res);

			return res;
		}

        /// <summary>
        /// 查找资源记录是否有该名字的资源
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <returns></returns>
		private Res GetResFromRecord(string assetName)
		{
			return mResRecord.Find(loadedAsset => loadedAsset.Name == assetName);
		}

        /// <summary>
        /// 查找全局资源池中是否有该名字资源
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <returns></returns>
		private Res GetFromResMgr(string assetName)
		{
			return ResMgr.Instance.SharedLoadedReses.Find(loadedAsset => loadedAsset.Name == assetName);
		}

        /// <summary>
        /// 增加资源到资源记录
        /// </summary>
        /// <param name="resFromResMgr"></param>
		private void AddRes2Record(Res resFromResMgr)
		{
			mResRecord.Add(resFromResMgr);
				
			resFromResMgr.Retain();
		}
		
		#endregion
	}
}