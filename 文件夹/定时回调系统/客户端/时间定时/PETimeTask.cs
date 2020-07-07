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
    public float destTime;
    public float delayTime;
    public Action callback;
    public int count;
    public int id;
}

/// <summary>
/// 时间单位枚举,包括毫秒、秒、分、小时、天
/// </summary>
public enum PETimeUnit
{
    Millisecond,
    Second,
    Minute,
    Hour,
    Day
}
