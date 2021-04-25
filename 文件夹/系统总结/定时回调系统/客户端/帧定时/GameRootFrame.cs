using UnityEngine;

public class GameRootFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        FrameSystem frameSystem = GetComponent<FrameSystem>();
        frameSystem.InitFrameSys();
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
        id = FrameSystem.Intance.AddFrameTask(FuncA, 1, 3);//创造计时任务
        FrameSystem.Intance.DeleteFrameTask(id);//删除某个任务
        FrameSystem.Intance.ReplaceFrameTask(id, FuncB, 2, 0);//代替某个任务
    }

}
