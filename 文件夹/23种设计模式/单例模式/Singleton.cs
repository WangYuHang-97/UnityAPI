using UnityEngine;

/// <summary>
/// 饿汉式单例模式
/// </summary>
public class Singleton : MonoBehaviour
{
    private static Singleton _instance;
    public static Singleton Instance { get => _instance;}

    private void Awake()
    {
        _instance = this;
    }
}

/// <summary>
/// 懒汉式单例模式
/// </summary>
public class Singleton2 : MonoBehaviour
{
    private static Singleton _instance;
    public static Singleton Instance {
        get
        {
            if(_instance == null) _instance = new Singleton();
            return _instance;
        }
    }
}

/// <summary>
/// 饿汉式单例模式模板
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonTemplate<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance { get => _instance; }

    private void Awake()
    {
        _instance = this as T;
    }

}

/// <summary>
/// 饿汉式单例模式模板示例
/// </summary>
public class Manager : SingletonTemplate<Manager>
{

}
