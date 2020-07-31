using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    public static TimerSystem Intance;//单例模式
    private static readonly string obj = "lock";//锁定义
    private int id;//id定义
    private List<PETimeTask> tempTimeList = new List<PETimeTask>();//缓存列表
    private List<PETimeTask> taskTimeList = new List<PETimeTask>();//操作列表
    private List<int> idList = new List<int>();//id列表
    private List<int> recycleList = new List<int>();//id回收列表

    /// <summary>
    /// 初始化
    /// </summary>
    public void InitTimerSys()
    {
        Intance = this;//单例赋值
    }

    private void Update()
    {
        BeginTiming();
    }

    /// <summary>
    /// 时间计时器主运行方法
    /// </summary>
    private void BeginTiming()
    {
        for (int tempIndex = 0; tempIndex < tempTimeList.Count; tempIndex++)//遍历缓存列表,并加入至操作列表
        {
            taskTimeList.Add(tempTimeList[tempIndex]);
        }
        tempTimeList.Clear();//清除缓存列表
        for (int i = 0; i < taskTimeList.Count; i++)//遍历操作列表进行操作判定
        {
            PETimeTask timeTask = taskTimeList[i];//临时赋值
            if (Time.realtimeSinceStartup * 1000 < timeTask.destTime)//没有达到目标时间.进行下一任务判断
            {
                continue;
            }
            else//达到当前时间
            {
                Action callback = timeTask.callback;//方法赋值
                try
                {
                    if (callback != null)//安全判断
                    {
                        callback();//执行方法
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                if (timeTask.count == 1)
                {
                    taskTimeList.RemoveAt(i);//移除当前任务
                    i--;//转为下一任务
                    recycleList.Add(timeTask.id);
                }
                else if (timeTask.count != 0)
                {
                    timeTask.count--;
                    timeTask.destTime += timeTask.delayTime;
                }
                else
                {
                    timeTask.destTime += timeTask.delayTime;
                }
            }
        }
        RecycleId();
    }

    /// <summary>
    /// <para>添加任务至缓存列表</para>
    /// <para>callback:需要执行的方法</para>
    /// <para>delayTime:延迟时间</para>
    /// <para>PETimeUnit:时间单位(毫秒,秒,分,小时,天)</para>
    /// <para>count:循环次数(0时无限循环)</para>
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="delayTime"></param>
    /// <param name="timeUnit"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int AddTimeTask(Action callback, float delayTime, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1)
    {
        int id = GetId();//获取ID
        tempTimeList.Add(CreatePETimeTask(id, callback, delayTime, timeUnit, count));//添加到任务列表
        idList.Add(id);//添加到id列表
        return id;//返回该id值
    }

    /// <summary>
    /// 获取一个没有使用过的Id
    /// </summary>
    /// <returns></returns>
    private int GetId()
    {
        lock (obj)//锁住该代码块,防止其他线程进入
        {
            id++;//id自增
            while (true)
            {
                if (id == int.MaxValue)//安全验证,防止超出int最大字节
                {
                    id = 0;
                }
                bool used = false;//定义该id是否被使用过
                for (int i = 0; i < idList.Count; i++)
                {
                    if (id == idList[i])//遍历id列表,被使用过后直接进行下面id自增,直到查询到没有被使用过的id
                    {
                        used = true;
                        break; ;
                    }
                }
                if (!used)//没有被使用过直接跳出循环
                {
                    break;
                }
                else
                {
                    id++;
                }
            }
        }
        return id;//返回该id值
    }

    /// <summary>
    /// <para>根据ID创建一个新PETimeTask</para>
    /// <para>callback:需要执行的方法</para>
    /// <para>delayTime:延迟时间</para>
    /// <para>PETimeUnit:时间单位(毫秒,秒,分,小时,天)</para>
    /// <para>count:循环次数(0时无限循环)</para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="callback"></param>
    /// <param name="delayTime"></param>
    /// <param name="timeUnit"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private PETimeTask CreatePETimeTask(int id, Action callback, float delayTime, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1)
    {
        switch (timeUnit)//根据PETimeUnit类型赋值
        {
            case PETimeUnit.Millisecond:
                break;
            case PETimeUnit.Second:
                delayTime *= 1000;
                break;
            case PETimeUnit.Minute:
                delayTime *= 1000 * 60;
                break;
            case PETimeUnit.Hour:
                delayTime *= 1000 * 60 * 60;
                break;
            case PETimeUnit.Day:
                delayTime *= 1000 * 60 * 60 * 24;
                break;
            default:
                Debug.Log("没有当前的计时单位");
                break; ;
        }
        float destTime = Time.realtimeSinceStartup * 1000 + delayTime;//由于默认为毫秒,因此×1000
        PETimeTask timeTask = new PETimeTask//新任务赋值
        {
            destTime = destTime,
            callback = callback,
            delayTime = delayTime,
            count = count,
            id = id
        };
        return timeTask;
    }

    /// <summary>
    /// 传递id值删除该任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool DeleteTimeTask(int id)
    {
        return SearchIdExistAndDelete(id, tempTimeList) || SearchIdExistAndDelete(id, taskTimeList);//分别查询缓存列表和操作列表是否有该id,并删除
    }

    /// <summary>
    /// 给定id和PETimeTask列表,查询该PETimeTask列表中PETimeTask是否有该id,并删除
    /// </summary>
    /// <param name="id"></param>
    /// <param name="TimeList"></param>
    /// <returns></returns>
    private bool SearchIdExistAndDelete(int id, List<PETimeTask> TimeList)
    {
        for (int i = 0; i < TimeList.Count; i++)
        {
            PETimeTask timeTask = TimeList[i];
            if (timeTask.id == id)
            {
                TimeList.RemoveAt(i);
                for (int j = 0; j < idList.Count; j++)
                {
                    if (id == idList[j])
                    {
                        idList.RemoveAt(j);
                        break; ;
                    }
                }
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 添加一个新任务并传递一个ID,替代该ID任务
    /// </summary>
    /// <param name="id"></param>
    /// <param name="callback"></param>
    /// <param name="delayTime"></param>
    /// <param name="timeUnit"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool ReplaceTimeTask(int id, Action callback, float delayTime, PETimeUnit timeUnit = PETimeUnit.Millisecond,
        int count = 1)
    {
        PETimeTask newTimeTask = CreatePETimeTask(id, callback, delayTime, timeUnit, count);
        return SearchIdExistAndReplace(id, taskTimeList, newTimeTask) ||
               SearchIdExistAndReplace(id, tempTimeList, newTimeTask);
    }

    /// <summary>
    /// 给定id、PETimeTask列表和新PETimeTask,查询该PETimeTask列表中PETimeTask是否有该id,并替代
    /// </summary>
    /// <param name="id"></param>
    /// <param name="TimeList"></param>
    /// <param name="timeTask"></param>
    /// <returns></returns>
    private bool SearchIdExistAndReplace(int id, List<PETimeTask> TimeList, PETimeTask timeTask)
    {
        for (int i = 0; i < TimeList.Count; i++)
        {
            if (TimeList[i].id == id)
            {
                TimeList[i] = timeTask;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 删除回收ID列表中的所有id
    /// </summary>
    private void RecycleId()
    {
        if (recycleList.Count > 0)
        {
            for (int i = 0; i < recycleList.Count; i++)
            {
                int id = recycleList[i];
                for (int j = 0; j < idList.Count; j++)
                {
                    if (id == idList[j])
                    {
                        idList.RemoveAt(j);
                        break;
                    }
                }
            }
            recycleList.Clear();
        }
    }
}
