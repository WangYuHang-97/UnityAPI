using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IState
{
    void Handle();
}

/// <summary>
/// 状态机
/// </summary>
public class Context
{
    private IState mState;//当前状态

    public void SetState(IState state)
    {
        mState = state;
    }

    public void Handle()
    {
        mState.Handle();//当前状态下需要执行方法
    }
}

public class EatMeals : IState
{
    private Context mContext;

    public EatMeals(Context context)
    {
        mContext = context;
    }

    public void Handle()
    {
        Debug.Log("在吃饭");
    }
}

public class Work : IState
{
    private Context mContext;

    public Work(Context context)
    {
        mContext = context;
    }

    public void Handle()
    {
        Debug.Log("在工作");
    }
}

public class Sleep : IState
{
    private Context mContext;

    public Sleep(Context context)
    {
        mContext = context;
    }

    public void Handle()
    {
        Debug.Log("在吃饭");
    }
}

public class DPState : MonoBehaviour
{
    void Start()
    {
        Context context = new Context();
        context.SetState(new EatMeals(context));
        context.Handle();
    }

}
