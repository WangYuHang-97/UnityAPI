using System;
using System.Collections;
using System.Collections.Generic;

public class EventCenter
{

    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();


    /// <summary>
    /// 监听事件加入方法基方法
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    private static void OnListenerAdding(EventType eventType, Delegate callback)
    {
        if (!m_EventTable.ContainsKey(eventType))//若没有该事件,则添加
        {
            m_EventTable.Add(eventType, null);
        }

        Delegate m_delegate = m_EventTable[eventType];
        if (m_delegate != null && m_delegate.GetType() != callback.GetType())//若该事件对应委托类型不一致
        {
            throw new Exception(String.Format("尝试为事件{0}添加不同类型的委托，当前委托类型为{1}，要添加的委托类型为{2}", eventType, m_delegate.GetType(), callback.GetType()));
        }
    }

    /// <summary>
    /// 无参数监听事件加入方法
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void AddListener(EventType eventType, Callback callback)
    {
        OnListenerAdding(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback + callback;
    }

    /// <summary>
    /// 单参数监听事件加入方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void AddListener<T>(EventType eventType, Callback<T> callback)
    {
        OnListenerAdding(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T> + callback;
    }

    /// <summary>
    /// 双参数监听事件加入方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void AddListener<T, X>(EventType eventType, Callback<T, X> callback)
    {
        OnListenerAdding(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X> + callback;
    }

    /// <summary>
    /// 三参数监听事件加入方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void AddListener<T, X, Z>(EventType eventType, Callback<T, X, Z> callback)
    {
        OnListenerAdding(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X, Z> + callback;
    }

    /// <summary>
    /// 四参数监听事件加入方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void AddListener<T, X, Z, W>(EventType eventType, Callback<T, X, Z, W> callback)
    {
        OnListenerAdding(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X, Z, W> + callback;
    }

    /// <summary>
    /// 五参数监听事件加入方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void AddListener<T, X, Y, Z, W>(EventType eventType, Callback<T, X, Y, Z, W> callback)
    {
        OnListenerAdding(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X, Y, Z, W> + callback;
    }


    /// <summary>
    /// 监听事件移除方法基方法1
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    private static void OnListenerRemoving(EventType eventType, Delegate callback)
    {
        if (m_EventTable.ContainsKey(eventType))//若包含该事件
        {
            Delegate m_delegate = m_EventTable[eventType];
            if (m_delegate == null)//该事件委托类型为空
            {
                throw new Exception(String.Format("移除监听错误，没有该事件{0}对应委托", eventType));
            }
            else if (m_delegate.GetType() != callback.GetType())//该事件委托类型不一致
            {
                throw new Exception(String.Format("移除监听错误，该事件{0}对应委托错误,当前委托类型为{1}，要移除的委托类型为{2}", eventType, m_delegate.GetType(), callback.GetType()));
            }
        }
        else//没有该事件
        {
            throw new Exception(String.Format("没有该事件{0}", eventType));
        }
    }

    private static void OnListenerRemoved(EventType eventType)
    {
        if (m_EventTable[eventType] == null)//若该事件所有委托都已移除，则移除该事件
        {
            m_EventTable.Remove(eventType);
        }
    }

    /// <summary>
    /// 无参数监听事件移除方法
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void RemoveListener(EventType eventType, Callback callback)
    {
        OnListenerRemoving(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback - callback;
        OnListenerRemoved(eventType);
    }

    /// <summary>
    /// 单参数监听事件移除方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void RemoveListener<T>(EventType eventType, Callback<T> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T> - callback;
        OnListenerRemoved(eventType);
    }

    /// <summary>
    /// 双参数监听事件移除方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void RemoveListener<T, X>(EventType eventType, Callback<T, X> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X> - callback;
        OnListenerRemoved(eventType);
    }

    /// <summary>
    /// 三参数监听事件移除方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void RemoveListener<T, X, Y>(EventType eventType, Callback<T, X, Y> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X, Y> - callback;
        OnListenerRemoved(eventType);
    }


    /// <summary>
    /// 四参数监听事件移除方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void RemoveListener<T, X, Y, Z>(EventType eventType, Callback<T, X, Y, Z> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X, Y, Z> - callback;
        OnListenerRemoved(eventType);
    }

    /// <summary>
    /// 五参数监听事件移除方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="callback"></param>
    public static void RemoveListener<T, X, Y, Z, W>(EventType eventType, Callback<T, X, Y, Z, W> callback)
    {
        OnListenerRemoving(eventType, callback);
        m_EventTable[eventType] = m_EventTable[eventType] as Callback<T, X, Y, Z, W> - callback;
        OnListenerRemoved(eventType);
    }

    /// <summary>
    /// 无参数广播事件
    /// </summary>
    /// <param name="eventType"></param>
    public static void Broadcast(EventType eventType)
    {
        Delegate m_delegate;
        if (m_EventTable.TryGetValue(eventType, out m_delegate))
        {
            Callback callback = m_delegate as Callback;
            if (callback != null)//不为空执行该委托
            {
                callback();
            }
            else//委托类型不一致
            {
                throw new Exception(String.Format("广播事件错误:事件{0}对应委托不适于该委托类型(导入参数不同)", eventType));
            }
        }
    }

    /// <summary>
    /// 单参数广播事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="arg"></param>
    public static void Broadcast<T>(EventType eventType, T arg)
    {
        Delegate m_delegate;
        if (m_EventTable.TryGetValue(eventType, out m_delegate))
        {
            Callback<T> callback = m_delegate as Callback<T>;
            if (callback != null)//不为空执行该委托
            {
                callback(arg);
            }
            else//委托类型不一致
            {
                throw new Exception(String.Format("广播事件错误:事件{0}对应委托不适于该委托类型(导入参数不同)", eventType));
            }
        }
    }

    /// <summary>
    /// 双参数广播事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    public static void Broadcast<T, X>(EventType eventType, T arg1, X arg2)
    {
        Delegate m_delegate;
        if (m_EventTable.TryGetValue(eventType, out m_delegate))
        {
            Callback<T, X> callback = m_delegate as Callback<T, X>;
            if (callback != null)//不为空执行该委托
            {
                callback(arg1, arg2);
            }
            else//委托类型不一致
            {
                throw new Exception(String.Format("广播事件错误:事件{0}对应委托不适于该委托类型(导入参数不同)", eventType));
            }
        }
    }

    /// <summary>
    /// 三参数广播事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <param name="arg3"></param>
    public static void Broadcast<T, X, Z>(EventType eventType, T arg1, X arg2, Z arg3)
    {
        Delegate m_delegate;
        if (m_EventTable.TryGetValue(eventType, out m_delegate))
        {
            Callback<T, X, Z> callback = m_delegate as Callback<T, X, Z>;
            if (callback != null)//不为空执行该委托
            {
                callback(arg1, arg2, arg3);
            }
            else//委托类型不一致
            {
                throw new Exception(String.Format("广播事件错误:事件{0}对应委托不适于该委托类型(导入参数不同)", eventType));
            }
        }
    }

    /// <summary>
    /// 四参数广播事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <param name="arg3"></param>
    /// <param name="arg4"></param>
    public static void Broadcast<T, X, Y, Z>(EventType eventType, T arg1, X arg2, Y arg3, Z arg4)
    {
        Delegate m_delegate;
        if (m_EventTable.TryGetValue(eventType, out m_delegate))
        {
            Callback<T, X, Y, Z> callback = m_delegate as Callback<T, X, Y, Z>;
            if (callback != null)//不为空执行该委托
            {
                callback(arg1, arg2, arg3, arg4);
            }
            else//委托类型不一致
            {
                throw new Exception(String.Format("广播事件错误:事件{0}对应委托不适于该委托类型(导入参数不同)", eventType));
            }
        }
    }

    /// <summary>
    /// 五参数广播事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <param name="arg3"></param>
    /// <param name="arg4"></param>
    /// <param name="arg5"></param>
    public static void Broadcast<T, X, Y, Z, W>(EventType eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
    {
        Delegate m_delegate;
        if (m_EventTable.TryGetValue(eventType, out m_delegate))
        {
            Callback<T, X, Y, Z, W> callback = m_delegate as Callback<T, X, Y, Z, W>;
            if (callback != null)//不为空执行该委托
            {
                callback(arg1, arg2, arg3, arg4, arg5);
            }
            else//委托类型不一致
            {
                throw new Exception(String.Format("广播事件错误:事件{0}对应委托不适于该委托类型(导入参数不同)", eventType));
            }
        }
    }
}
