using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace EasyUI
{
	public partial class CountDown1 : MonoBehaviour
	{
	    private int Time = 120;
	    private int time;//时间
	    private float moveDis;//移动距离

        /// <summary>
        /// 开始调用
        /// </summary>
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
	                TextMove(Text_Time1.transform.localPosition.y > Text_Time2.transform.localPosition.y//最上方那个Text移动到下方
	                    ? Text_Time1
	                    : Text_Time2);
	            });
                this.Text_Time2.transform.DOLocalMoveY(this.Text_Time2.transform.localPosition.y + moveDis, 1f).SetEase(Ease.InOutElastic);
            }
            Invoke("GameOver",2f);//此处建议使用自制计时器
	    }

        /// <summary>
        /// 倒计数结束调用
        /// </summary>
	    void GameOver()
	    {
	        Debug.Log("游戏结束");
	        Application.Quit();
        }

        /// <summary>
        /// 文本移动方法
        /// </summary>
        /// <param name="text"></param>
	    void TextMove(Text text)
	    {
	        text.transform.localPosition -= new Vector3(0, 2 * moveDis);
	        text.text = time - 1 < 0 ? "" : (time - 1).ToString();
        }
	}
}
