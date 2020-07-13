using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class 翻书效果实现ScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    //该方法需要Content的AnchorPresets设置为双stret

    #region 字段
    //引入模块
    private Canvas canvas;
    private ScrollRect scrollRect;
    //字段

    private float cellLength;//单个单元格长度
    private float Spacing;//单元格间隙
    private float firstItemLength;//移动第一个单元格距离
    private float contentLength;//content长度
    private float upperLimit;//上限
    private float lowerLimit;//下限
    private float oneItemLength;//滑动一个单元格需要距离,即两两单元格中心距离
    private float oneItemProportion;//滑动一个单元格需要比例
    private float beginMousePositionX;//鼠标开始坐标
    private float endMousePositionX;//鼠标停止坐标
    private float lastProportion;//上一个位置比例
    private int leftOffset;//左偏移量
    private int totalItemNum;//单元格数量
    private int currentIndex;//当前单元格索引

    #endregion

    #region 方法
    #endregion

    #region Unity回调
    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        scrollRect = GetComponent<ScrollRect>();
        cellLength = scrollRect.content.GetComponent<GridLayoutGroup>().cellSize.x;
        Spacing = scrollRect.content.GetComponent<GridLayoutGroup>().spacing.x;
        leftOffset = scrollRect.content.GetComponent<GridLayoutGroup>().padding.left;
        contentLength = scrollRect.gameObject.GetComponent<RectTransform>().rect.width + scrollRect.content.rect.width - 2 * leftOffset - cellLength;
        firstItemLength = cellLength / 2 + leftOffset;
        oneItemLength = cellLength + Spacing;
        oneItemProportion = oneItemLength / contentLength;
        upperLimit = 1 - firstItemLength / contentLength;
        lowerLimit = firstItemLength / contentLength;
        currentIndex = 1;
        scrollRect.horizontalNormalizedPosition = 0;
        totalItemNum = scrollRect.content.transform.childCount;
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
        if (Mathf.Abs(offsetX) > firstItemLength)//执行滑动条件前必须大于第一个单元格距离
        {
            if (offsetX > 0)//右滑
            {
                if (currentIndex >= totalItemNum) return;//到达右顶端
                int moveCount = (int)((offsetX - firstItemLength) / oneItemLength) + 1;//单次移动单元格数目
                currentIndex += moveCount;//更新索引
                if (currentIndex >= totalItemNum) currentIndex = totalItemNum; //大于最大单元格个数
                lastProportion += oneItemProportion * moveCount;//移动比例更新
                if (lastProportion >= upperLimit) lastProportion = 1;//设置为1时到达右顶端
            }
            else//左滑
            {
                if (currentIndex <= 1) return;//到达左顶端
                int moveCount = (int)((offsetX + firstItemLength) / oneItemLength) - 1;//单次移动单元格数目
                currentIndex += moveCount;//更新索引
                if (currentIndex <= 1) currentIndex = 1; //大于最大单元格个数
                lastProportion += oneItemProportion * moveCount;//移动比例更新
                if (lastProportion <= lowerLimit) lastProportion = 0;//设置为0时到达左顶端
            }
        }
        DOTween.To(() => scrollRect.horizontalNormalizedPosition,//参数1:需要改变的值   参数2:浮动值,并赋值给参数1   参数3:目标值   参数4:持续时间    参数5.设置移动加速度曲线
            lerpValue => scrollRect.horizontalNormalizedPosition = lerpValue, lastProportion, 0.5f).SetEase(Ease.OutQuint);
    }
    #endregion
}
