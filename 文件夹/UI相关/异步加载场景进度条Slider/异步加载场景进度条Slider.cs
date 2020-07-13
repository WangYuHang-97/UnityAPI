using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// <para>脚本位置：Main Camera</para>
/// <para>脚本功能：场景异步加载的进度条显示</para>
/// </summary>
public class 异步加载场景进度条Slider : MonoBehaviour
{
    #region 字段
    //引入模块
    public Slider processBar;//滑动条gameobject导入
    public AsyncOperation AsyncOperation;//异步加载程序导入

    //变量
    private int nowProcess = 0;//场景缓冲加载进度
    private int targetProcess;//场景实际加载进度
    #endregion

    #region 方法
    /// <summary>
    /// <para>将需要加载场景进行协程方法判断</para>
    /// SceneNum:场景编号
    /// </summary>
    /// <param name="SceneNum"></param>
    /// <returns></returns>
    IEnumerator LoadScene(int SceneNum)
    {
        AsyncOperation = SceneManager.LoadSceneAsync(SceneNum);//指定需要加载场景赋予AsyncOperation
        AsyncOperation.allowSceneActivation = false;//设置加载完成后不能自动跳转场景
        yield return AsyncOperation;//下载完成后返回async
    }

    #endregion

    #region Unity回调

    void Start()
    {
        StartCoroutine(LoadScene(2));//开启协程
    }


    void Update()
    {
        if (AsyncOperation == null)//如果加载完成后，不再进行之后判断
        {
            return;
        }
        if (AsyncOperation.progress < 0.9f)//若当前进度小于90％,则将实际加载进度赋值
        {
            targetProcess = (int)(AsyncOperation.progress * 100);
        }
        else
        {
            targetProcess = 100;//大于90％,则直接达到满值
        }
        if (nowProcess < targetProcess)//若缓冲加载进度小于实际加载进度,将缓冲加载进度自增（为达到缓慢增加效果）
        {
            nowProcess++;
        }
        processBar.value = nowProcess / 100f;//UI显示
        if (nowProcess == 100)//满载,自动跳转场景bool设置为true
        {
            AsyncOperation.allowSceneActivation = true;
        }
    }
    #endregion
}
