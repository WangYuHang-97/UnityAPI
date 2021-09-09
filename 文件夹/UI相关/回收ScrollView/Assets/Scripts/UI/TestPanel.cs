using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    public MoveScrollRect Ept_DailyTask;
    public Button Btn_ChangeData;

    private int _dataIndex = 2;


    // Start is called before the first frame update
    void Start()
    {
        Ept_DailyTask.Init<Ept_Task>(GetData());//初始化调用
        Btn_ChangeData.onClick.AddListener(() =>
        {
            Ept_DailyTask.RefreshData(GetData());//更新数据调用
        });
    }

    /// <summary>
    /// 二个参数
    /// </summary>
    /// <returns></returns>
    object[] GetData()
    {
        _dataIndex = _dataIndex == 1 ? 2 : 1;

        var datas = new object[2];
        List<string> contents = new List<string>();
        List<int> indexs = new List<int>();

        for (int i = 1 + (_dataIndex - 1)*100 ; i <= 100 + (_dataIndex - 1) * 100; i++)
        {
            contents.Add($"这是一个任务{i}");
            indexs.Add(i);
        }
        datas[0] = contents.ToListObj();
        datas[1] = indexs.ToListObj();
        return datas;
    }

    //一个参数可这样使用
    //List<object> GetData()
    //{
    //    List<string> contents = new List<string>()
    //    {
    //        "这是一个任务1",
    //        "这是一个任务2",
    //        "这是一个任务3",
    //        "这是一个任务4",
    //        "这是一个任务5",
    //        "这是一个任务6",
    //        "这是一个任务7",
    //        "这是一个任务8",
    //        "这是一个任务9",
    //        "这是一个任务10",
    //        "这是一个任务11",
    //        "这是一个任务12",
    //        "这是一个任务13",
    //    };
    //    return contents.ToListObj();
    //}
}
