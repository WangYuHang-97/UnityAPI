using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EasyUI
{
    public class PageTurn : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {

        #region 字段
        //引入模块
        private Canvas canvas;
        private ScrollRect scrollRect;
        //字段

        public bool IsMemoryModel = true;//若为true，保持的参数不初始化，用于返回面板保持上次操作位置
        public float oneItemLengthCoefficient = 0.8f;//滑动一个单元格需要距离偏移系数，当没到oneItemLength时就滑动，优化手感

        private float cellLength;//单个单元格长度
        private float Spacing;//单元格间隙
        private float contentLength;//content长度
        private float oneItemLength;//滑动一个单元格需要距离,即两两单元格中心距离
        private float oneItemProportion;//滑动一个单元格需要比例
        private float beginMousePositionX;//鼠标开始坐标
        private float endMousePositionX;//鼠标停止坐标
        private float lastProportion;//上一个位置比例
        private int leftOffset;//左偏移量
        private int totalItemNum;//单元格数量
        private int currentIndex;//当前单元格索引

        #endregion

        #region Unity回调
        void Awake()
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();//此处为方便，未使用框架
            scrollRect = GetComponent<ScrollRect>();
            cellLength = scrollRect.content.GetComponent<GridLayoutGroup>().cellSize.x;
            Spacing = scrollRect.content.GetComponent<GridLayoutGroup>().spacing.x;
            leftOffset = scrollRect.content.GetComponent<GridLayoutGroup>().padding.right = scrollRect.content.GetComponent<GridLayoutGroup>().padding.left = (int)(GetComponent<RectTransform>().rect.width / 2 - cellLength/2);//左偏移
            totalItemNum = scrollRect.content.transform.childCount;
            contentLength = leftOffset * 2 + totalItemNum * cellLength + (totalItemNum - 1) * Spacing;
            scrollRect.content.sizeDelta = new Vector2(contentLength, scrollRect.content.rect.height);
            oneItemLength = cellLength + Spacing;
            oneItemProportion = 1f / (totalItemNum - 1);
            Init();
        }

        void OnEnable()
        {
            if (!IsMemoryModel)Init();
        }
        #endregion

        #region 方法
        void Init()
        {
            currentIndex = 1;
            scrollRect.horizontalNormalizedPosition = 0;
        }
        #endregion

        #region 事件回调
        /// <summary>
        /// 将视口坐标转为本地坐标,并返回x值
        /// </summary>
        /// <returns></returns>
        private float MousePosX2LocalPosX()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            return position.x;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            beginMousePositionX = MousePosX2LocalPosX();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            float offsetX = 0;//鼠标坐标差值初始化
            endMousePositionX = MousePosX2LocalPosX();
            offsetX = beginMousePositionX - endMousePositionX;//鼠标坐标差值
            Debug.Log(offsetX);
            if (Mathf.Abs(offsetX) > oneItemLength * oneItemLengthCoefficient)//执行滑动条件前必须大于第一个单元格距离
            {
                if (offsetX > 0)//右滑
                {
                    if (currentIndex >= totalItemNum) return;//到达右顶端
                    int moveCount = (int)((offsetX) / oneItemLength / oneItemLengthCoefficient);//单次移动单元格数目
                    currentIndex += moveCount;//更新索引
                    if (currentIndex >= totalItemNum) currentIndex = totalItemNum; //大于最大单元格个数
                    lastProportion += oneItemProportion * moveCount;//移动比例更新
                    if (lastProportion >= 1) lastProportion = 1;//设置为1时到达右顶端
                }
                else//左滑
                {
                    if (currentIndex <= 1) return;//到达左顶端
                    int moveCount = (int)((offsetX) / oneItemLength / oneItemLengthCoefficient);//单次移动单元格数目
                    currentIndex += moveCount;//更新索引
                    if (currentIndex <= 1) currentIndex = 1; //大于最大单元格个数
                    lastProportion += oneItemProportion * moveCount;//移动比例更新
                    if (lastProportion <= 0) lastProportion = 0;//设置为0时到达左顶端
                }
            }
            DOTween.To(() => scrollRect.horizontalNormalizedPosition,//参数1:需要改变的值   参数2:浮动值,并赋值给参数1   参数3:目标值   参数4:持续时间    参数5.设置移动加速度曲线
                lerpValue => scrollRect.horizontalNormalizedPosition = lerpValue, lastProportion, 0.5f).SetEase(Ease.OutQuint);
        }
        #endregion
    }
}

