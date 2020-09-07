using System;
using System.Reflection;

namespace QFramework
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static T mInstance;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

                    if (ctor == null)
                        throw new Exception("Non-public ctor() not found!");

                    mInstance = ctor.Invoke(null) as T;
                }

                return mInstance;
            }
        }

        protected Singleton()
        {

        }
    }
}