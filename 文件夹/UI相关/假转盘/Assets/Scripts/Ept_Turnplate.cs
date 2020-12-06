using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ShootResources
{
    /// <summary>
    /// 轮盘系统
    /// </summary>
	public partial class Ept_Turnplate : MonoBehaviour
	{
	    private float rotateSpeed = 360f;//轮盘旋转速度（角度/s）

	    private float topValue;//角度上限
	    private float downValue;//角度下限
	    private TurnplateItem turnplateItem;//轮盘资源
	    private Tween firstRotate;
	    private Tween secondRotate;
	    private Tween thirdRotate;
	    private Tween turnplateItemShake;

        private List<int> turnplateItemProbabilityList;//物品概率列表，用来存储各物品概率
	    private List<TurnplateItem> turnplateItemList;//物品列表，有哪些物品参与随机抽取
	    private int turnplateItemTotalProbability;//总概率

        /// <summary>
        /// 唤醒调用
        /// </summary>
	    void Awake()
	    {
	        ParseFakeData();
	    }

	    /// <summary>
	    /// 假数据导入
	    /// </summary>
	    void ParseFakeData()
	    {
	        turnplateItemProbabilityList = new List<int>();
	        turnplateItemList = new List<TurnplateItem>();
	        turnplateItemTotalProbability = 0;
	        TurnplateItem basicsalRope = new TurnplateItem("Reward_Bullet", 60,30,-30,0);
	        turnplateItemList.Add(basicsalRope);
            basicsalRope = new TurnplateItem("Reward_Meat", 2,-30,-90,1);
            turnplateItemList.Add(basicsalRope);
            basicsalRope = new TurnplateItem("Reward_Fish", 20,-90,-150,2); 
            turnplateItemList.Add(basicsalRope);
	        basicsalRope = new TurnplateItem("Reward_Fruit", 20, -150, -210,3);
	        turnplateItemList.Add(basicsalRope);
	        basicsalRope = new TurnplateItem("Reward_Vegetable", 20, -210, -270,4);
	        turnplateItemList.Add(basicsalRope);
	        basicsalRope = new TurnplateItem("Reward_Treasure", 20, -270, -330,5);
	        turnplateItemList.Add(basicsalRope);

            foreach (var item in turnplateItemList)
	        {
	            turnplateItemProbabilityList.Add(item.probability);
	            turnplateItemTotalProbability += item.probability;
	        }
	    }

        /// <summary>
        /// 获得一个轮盘资源
        /// </summary>
        /// <returns></returns>
	    private TurnplateItem GetTurnplateItem()
	    {
	        int probability = 0;//概率增量，达到增量及返回物品
	        int randomNum = Random.Range(0, turnplateItemTotalProbability + 1);//随机获得概率
	        for (int i = 0; i < turnplateItemList.Count; i++)
	        {
	            probability += turnplateItemProbabilityList[i];
	            if (probability >= randomNum)//达到增量
	            {
	                return turnplateItemList[i];
	            }
	        }
	        return null;
	    }

        /// <summary>
        /// 激活调用
        /// </summary>
        void OnEnable()
        {
            turnplateItem = GetTurnplateItem();//获得轮盘资源
            this.Text_Item.text = turnplateItem.name;
            Tween open = this.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);//打开轮盘tween
            open.OnKill(StartReallyRotate);//打开后开启旋转
        }

        /// <summary>
        /// 关闭调用
        /// </summary>
	    void OnDisable()
	    {
            //安全校验
	        firstRotate?.Kill();
	        secondRotate?.Kill();
            thirdRotate?.Kill();
            turnplateItemShake?.Kill();
            this.Turnplate.localRotation = new Quaternion(0,0,0,0);//恢复状态
        }

        /// <summary>
        /// 开启旋转
        /// </summary>
        void StartReallyRotate()
        {
            float angle = Random.Range(turnplateItem.downValue, turnplateItem.topValue);
            firstRotate = this.Turnplate.DORotate(new Vector3(0, 0, this.Turnplate.localEulerAngles.z - 360), 360 / rotateSpeed
                , RotateMode.FastBeyond360).SetEase(Ease.Linear).SetAutoKill();//第一次原地旋转无效果
            firstRotate.OnKill(() =>
            {
                angle -= 360;//防止反转
                secondRotate = this.Turnplate.DORotate(new Vector3(0, 0, angle), Mathf.Abs(angle) / rotateSpeed
                    , RotateMode.FastBeyond360).SetEase(Ease.Linear).SetAutoKill();//第二次转到指定位置
                secondRotate.OnKill(() =>
                {
                    thirdRotate = this.Turnplate.DORotate(new Vector3(0, 0, this.Turnplate.localEulerAngles.z - 360), 360*2 / rotateSpeed
                        , RotateMode.FastBeyond360).SetEase(Ease.OutQuad).SetAutoKill();//第三次原地减速旋转
                    thirdRotate.OnKill(() =>
                    {
                        turnplateItemShake = this.Turnplate.GetChild(turnplateItem.sibling).DOShakeScale(1f);//获得指定资源物抖动
                        turnplateItemShake.OnKill(Close);//关闭轮盘
                    });
                });
            });
        }

        /// <summary>
        /// 关闭方法
        /// </summary>
	    void Close()
	    {
	        Tween close = this.transform.DOLocalMoveY(-1300, 0.5f);
	        close.OnKill(() =>
	        {
                this.gameObject.SetActive(false);
	        });
        }
	}

    /// <summary>
    /// 轮盘资源
    /// </summary>
    class TurnplateItem
    {
        #region 字段
        //public int id; //id
        public string name; //物品名字
        public int probability; //物品生成概率
        public float topValue;
        public float downValue;
        public int sibling;
        #endregion

        #region 方法
        public TurnplateItem(string name, int probability, float topValue, float downValue,int sibling)
        {
            this.name = name;
            this.probability = probability;
            this.topValue = topValue;
            this.downValue = downValue;
            this.sibling = sibling;
        }
        #endregion
    }
}
