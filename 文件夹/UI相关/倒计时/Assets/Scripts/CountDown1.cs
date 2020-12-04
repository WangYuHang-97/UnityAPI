using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace EasyUI
{
	public partial class CountDown1 : MonoBehaviour
	{
	    private int Time = 120;
	    private int time;//时间
	    private float moveDis;//移动距离

        void Start()
		{
		    time = Time;
		    this.Text_Time1.text = time.ToString();
		    this.Text_Time2.text = (time - 1).ToString();
		    moveDis = GetComponent<RectTransform>().rect.height;
            StartCoroutine(CountDownFunction());
		}

	    IEnumerator CountDownFunction()
	    {
	        while (time > 0)
	        {
	            yield return new WaitForSeconds(1);
	            time--;
	            Tween move = this.Text_Time1.transform.DOLocalMoveY(this.Text_Time1.transform.localPosition.y + moveDis, 1f).SetEase(Ease.InOutElastic);
	            move.OnKill(() =>
	            {
	                TextMove(Text_Time1.transform.localPosition.y > Text_Time2.transform.localPosition.y
	                    ? Text_Time1
	                    : Text_Time2);
	            });
                this.Text_Time2.transform.DOLocalMoveY(this.Text_Time2.transform.localPosition.y + moveDis, 1f).SetEase(Ease.InOutElastic);
            }
            Invoke("GameOver",2f);//此处建议使用自制计时器
	    }

	    void test()
	    {

	    }

	    void GameOver()
	    {
	        Debug.Log("游戏结束");
	        Application.Quit();
        }

	    void TextMove(Text text)
	    {
	        text.transform.localPosition -= new Vector3(0, 2 * moveDis);
	        text.text = time - 1 < 0 ? "" : (time - 1).ToString();
        }
	}
}
