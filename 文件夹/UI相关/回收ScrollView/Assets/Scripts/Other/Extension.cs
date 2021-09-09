using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    /// <summary>
    /// 通过反射方式调用函数
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="methodName">方法名</param>
    /// <param name="args">参数</param>
    /// <returns></returns>
    public static object InvokeByReflect(this object obj, string methodName, params object[] args)
    {
        var methodInfo = obj.GetType().GetMethod(methodName);
        return methodInfo == null ? null : methodInfo.Invoke(obj, args);
    }

    public static List<object> ToListObj<T>(this IList<T> list)
    {
        List<object> objects = new List<object>();
        foreach (var obj in list)
        {
            objects.Add(obj);
        }
        return objects;
    }
}
