using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace EasyUI
{
	public partial class CountDown2 : MonoBehaviour
	{
	    private int Time = 120;
	    private int time;//时间
	    private float zoomValue;//移动距离

        void Start()
		{
		    time = Time;
		    this.Text_Time1.text = time.ToString();
		    zoomValue = 0.8f;
            StartCoroutine(CountDownFunction());
		}

	    IEnumerator CountDownFunction()
        {
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                Tween move = this.Text_Time1.transform.DOScale(Vector3.one * zoomValue, 1f).SetEase(Ease.OutQuad);
                move.OnKill(() =>
                {
                    Text_Time1.text = time.ToString();
                    this.Text_Time1.transform.localScale = Vector3.one;
                });
            }
            Invoke("GameOver",2f);//此处建议使用自制计时器
	    }

	    void GameOver()
	    {
	        Debug.Log("游戏结束");
	        Application.Quit();
        }
	}
}
