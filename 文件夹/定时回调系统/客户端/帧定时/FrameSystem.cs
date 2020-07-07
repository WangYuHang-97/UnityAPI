using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameSystem : MonoBehaviour
{
    public static FrameSystem Intance;//单例模式
    private static readonly string obj = "lock";//锁定义
    private int id;//id定义
    private int frameCount;
    private List<PEFrameTask> tempFrameList = new List<PEFrameTask>();//缓存列表
    private List<PEFrameTask> taskFrameList = new List<PEFrameTask>();//操作列表
    private List<int> idList = new List<int>();//id列表
    private List<int> recycleList = new List<int>();//id回收列表

    /// <summary>
    /// 初始化
    /// </summary>
    public void InitFrameSys()
    {
        Intance = this;//单例赋值
    }

    private void Update()
    {
        BeginFraming();
    }

    /// <summary>
    /// 时间计时器主运行方法
    /// </summary>
    private void BeginFraming()
    {
        for (int tempIndex = 0; tempIndex < tempFrameList.Count; tempIndex++)//遍历缓存列表,并加入至操作列表
        {
            taskFrameList.Add(tempFrameList[tempIndex]);
        }
        frameCount++;
        tempFrameList.Clear();//清除缓存列表
        for (int i = 0; i < taskFrameList.Count; i++)//遍历操作列表进行操作判定
        {
            PEFrameTask frameTask = taskFrameList[i];//临时赋值
            if (frameCount < frameTask.destFrame)//没有达到目标时间.进行下一任务判断
            {
                continue;
            }
            else//达到当前时间
            {
                Action callback = frameTask.callback;//方法赋值
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
                if (frameTask.count == 1)
                {
                    taskFrameList.RemoveAt(i);//移除当前任务
                    i--;//转为下一任务
                    recycleList.Add(frameTask.id);
                }
                else if (frameTask.count != 0)
                {
                    frameTask.count--;
                    frameTask.destFrame += frameTask.delayFrame;
                }
                else
                {
                    frameTask.destFrame += frameTask.delayFrame;
                }
            }
        }
        RecycleId();
    }

    /// <summary>
    /// <para>添加任务至缓存列表</para>
    /// <para>callback:需要执行的方法</para>
    /// <para>delayFrame:延迟时间</para>
    /// <para>count:循环次数(0时无限循环)</para>
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="delayFrame"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int AddFrameTask(Action callback, int delayFrame, int count = 1)
    {
        int id = GetId();//获取ID
        tempFrameList.Add(CreatePEFrameTask(id, callback, delayFrame, count));//添加到任务列表
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
    /// <para>根据ID创建一个新PEFrameTask</para>
    /// <para>callback:需要执行的方法</para>
    /// <para>delayFrame:延迟时间</para>
    /// <para>count:循环次数(0时无限循环)</para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="callback"></param>
    /// <param name="delayFrame"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private PEFrameTask CreatePEFrameTask(int id, Action callback, int delayFrame, int count = 1)
    {
        PEFrameTask FrameTask = new PEFrameTask//新任务赋值
        {
            destFrame = frameCount + delayFrame,
            callback = callback,
            delayFrame = delayFrame,
            count = count,
            id = id
        };
        return FrameTask;
    }

    /// <summary>
    /// 传递id值删除该任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool DeleteFrameTask(int id)
    {
        return SearchIdExistAndDelete(id, tempFrameList) || SearchIdExistAndDelete(id, taskFrameList);//分别查询缓存列表和操作列表是否有该id,并删除
    }

    /// <summary>
    /// 给定id和PEFrameTask列表,查询该PEFrameTask列表中PEFrameTask是否有该id,并删除
    /// </summary>
    /// <param name="id"></param>
    /// <param name="FrameList"></param>
    /// <returns></returns>
    private bool SearchIdExistAndDelete(int id, List<PEFrameTask> FrameList)
    {
        for (int i = 0; i < FrameList.Count; i++)
        {
            PEFrameTask frameTask = FrameList[i];
            if (frameTask.id == id)
            {
                FrameList.RemoveAt(i);
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
    /// <param name="delayFrame"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool ReplaceFrameTask(int id, Action callback, int delayFrame, int count = 1)
    {
        PEFrameTask newFrameTask = CreatePEFrameTask(id, callback, delayFrame, count);
        return SearchIdExistAndReplace(id, taskFrameList, newFrameTask) ||
               SearchIdExistAndReplace(id, tempFrameList, newFrameTask);
    }

    /// <summary>
    /// 给定id、PEFrameTask列表和新PEFrameTask,查询该PEFrameTask列表中PEFrameTask是否有该id,并替代
    /// </summary>
    /// <param name="id"></param>
    /// <param name="FrameList"></param>
    /// <param name="frameTask"></param>
    /// <returns></returns>
    private bool SearchIdExistAndReplace(int id, List<PEFrameTask> FrameList, PEFrameTask frameTask)
    {
        for (int i = 0; i < FrameList.Count; i++)
        {
            if (FrameList[i].id == id)
            {
                FrameList[i] = frameTask;
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
