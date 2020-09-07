using System;
using Object = UnityEngine.Object;

namespace QFramework
{
    public enum ResState
    {
        Waiting,
        Loading,
        Loaded,
    }

    /// <summary>
    /// 加载与卸载类
    /// </summary>
    public abstract class Res : SimpleRC
    {
        public ResState State
        {
            get { return mState;}
            protected set
            {
                mState = value;

                if (mState == ResState.Loaded)//完成时调用完成事件机制
                {
                    if (mOnLoadedEvent != null)
                    {
                        mOnLoadedEvent.Invoke(this);
                    }
                }
            } 
        }

        private ResState mState;
        
        public Object Asset { get; protected set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; protected set; }

        private string mAssetPath;

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadSync();

        /// <summary>
        /// 异步加载
        /// </summary>
        public abstract void LoadAsync();

        /// <summary>
        /// 卸载资源
        /// </summary>
        protected abstract void OnReleaseRes();

        protected override void OnZeroRef()
        {
            OnReleaseRes();
        }

        /// <summary>
        /// 完成事件机制
        /// </summary>
        private event Action<Res> mOnLoadedEvent;

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="onLoaded">事件</param>
        public void RegisterOnLoadedEvent(Action<Res> onLoaded)
        {
            mOnLoadedEvent += onLoaded;
        }

        /// <summary>
        /// 卸载事件
        /// </summary>
        /// <param name="onLoaded">事件</param>
        public void UnRegisterOnLoadedEvent(Action<Res> onLoaded)
        {
            mOnLoadedEvent -= onLoaded;
        }
    }
}