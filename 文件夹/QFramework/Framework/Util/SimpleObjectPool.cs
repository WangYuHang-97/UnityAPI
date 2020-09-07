using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public interface IPool<T>
    {
        T Allocate();

        bool Recycle(T obj);
    }

    public interface IObjectFactory<T>
    {
        T Create();
    }

    public abstract class Pool<T> : IPool<T>
    {
        protected Stack<T> mCacheStack = new Stack<T>();

        protected IObjectFactory<T> mFactory;

        /// <summary>
        /// Gets the current count.
        /// </summary>
        /// <value>The current count.</value>
        public int CurCount
        {
            get { return mCacheStack.Count; }
        }

        public T Allocate()
        {
            return mCacheStack.Count > 0 ? mCacheStack.Pop() : mFactory.Create();
        }

        public abstract bool Recycle(T obj);
    }

    public class CustomObjectFactroy<T> : IObjectFactory<T>
    {
        private Func<T> mFactroyMethod;

        public CustomObjectFactroy(Func<T> factroyMethod)
        {
            mFactroyMethod = factroyMethod;
        }

        public T Create()
        {
            return mFactroyMethod();
        }
    }

    public class SimpleObjectPool<T> : Pool<T>
    {
        Action<T> mResetMethod;

        public SimpleObjectPool(Func<T> factroyMethod,Action<T> resetMethod = null,int initCount = 0)
        {
            mFactory = new CustomObjectFactroy<T>(factroyMethod);
            mResetMethod = resetMethod;

            for (var i = 0; i < initCount; i++)
            {
                mCacheStack.Push(mFactory.Create());
            }
        }

        public override bool Recycle(T obj)
        {
            if (mResetMethod != null)
            {
                mResetMethod(obj);
            }

            mCacheStack.Push(obj);

            return true;
        }
    }
}