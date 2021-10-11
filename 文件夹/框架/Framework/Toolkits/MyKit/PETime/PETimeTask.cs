using System;

/// <summary>
/// <para>数据类</para>
/// <para>destTime:任务执行时间</para>
/// <para>callback:需要执行的方法</para>
/// <para>count:执行次数(0时为无限循环)</para>
/// <para>delayTime:延迟时间</para>
/// </summary>
public class PETimeTask
{
    public double destTime;
    public float delayTime;
    public Action callback;
    public Action<double> updateCallback;
    public int count;
    public int id;
    public bool updateModel;
}

/// <summary>
/// 时间单位枚举,包括毫秒、秒、分、小时、天
/// </summary>
public enum PETimeUnit
{
    Second,
    Minute,
    Hour,
    Day
}
