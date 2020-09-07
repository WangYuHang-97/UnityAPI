namespace QFramework
{
    /// <summary>
    /// 计数器接口
    /// </summary>
    public interface IRefCounter
    {
        int RefCount { get; }

        void Retain(object refOwner = null);

        void Release(object refOwner = null);
    }

    /// <summary>
    /// 简易计数器
    /// </summary>
    public class SimpleRC : IRefCounter
    {
        public int RefCount { get; private set; }

        /// <summary>
        /// 计数加一
        /// </summary>
        /// <param name="refOwner"></param>
        public void Retain(object refOwner = null)
        {
            RefCount++;
        }

        /// <summary>
        /// 计数减一并判断是否为0
        /// </summary>
        /// <param name="refOwner"></param>
        public void Release(object refOwner = null)
        {
            RefCount--;

            if (RefCount == 0)
            {
                OnZeroRef();
            }
        }

        /// <summary>
        /// 计数器为0时调用方法
        /// </summary>
        protected virtual void OnZeroRef()
        {

        }
    }
}