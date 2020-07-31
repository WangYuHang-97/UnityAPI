using UnityEngine;

public class GameRootTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        TimerSystem timerSystem = GetComponent<TimerSystem>();
        timerSystem.InitTimerSys();
    }

    ///示例

    private int id;

    void FuncA()
    {
        print("开始计时A");
    }

    void FuncB()
    {
        print("开始计时B");
    }

    void Start()
    {
        id = TimerSystem.Intance.AddTimeTask(FuncA, 1, PETimeUnit.Second, 3);//创造计时任务
        TimerSystem.Intance.DeleteTimeTask(id);//删除某个任务
        TimerSystem.Intance.ReplaceTimeTask(id, FuncB, 2f, PETimeUnit.Minute, 0);//代替某个任务
    }

}
