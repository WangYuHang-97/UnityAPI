using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //引入模块
    public Camera MiniMapCamera;
    public Canvas canvas;
    //属性值
    private bool isInMap;//是否在小地图内
    private float ZoomMax = 10;//缩放最大值
    private float ZoomMin = 2;//缩放最小值
    private int zoomSpeed = 200;//缩放速度
    private Vector2 dragBeginMousePosition;//开始拖拽位置
    private Vector2 dragEndMousePosition;//结束拖拽位置
    private Vector3 startPosition;//Camera开始位置

    public void OnPointerEnter(PointerEventData eventData)
    {
        isInMap = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isInMap = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = MiniMapCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInMap)
        {
            return;
        }
        Zoom();
        Move();
    }

    /// <summary>
    /// 缩放方法
    /// </summary>
    void Zoom()
    {
        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");//获得鼠标滑轮数值

        if (scrollWheelValue != 0 )//安全校验
        {
            if (MiniMapCamera.orthographicSize > ZoomMax)//安全校验防止超过阈值
            {
                MiniMapCamera.orthographicSize = ZoomMax;
            }
            if (MiniMapCamera.orthographicSize < ZoomMin)//安全校验防止超过阈值
            {
                MiniMapCamera.orthographicSize = ZoomMin;
            }
            //DOTWEEN方法
            //DOTween.To(() => MiniMapCamera.orthographicSize, newSize => MiniMapCamera.orthographicSize = newSize, MiniMapCamera.orthographicSize + scrollWheelValue * zoomSpeed, 1f).SetEase(Ease.Linear);
            MiniMapCamera.orthographicSize -= scrollWheelValue * Time.deltaTime * zoomSpeed;//改变Camera数值
        }
    }

    /// <summary>
    /// 移动方法
    /// </summary>
    void Move()
    {
        if (Input.GetMouseButtonDown(0))//获得起始点
        {
            dragBeginMousePosition = ScreenPointToWorldPoint();
        }
        if (Input.GetMouseButton(0))//实时更新拖拽点并移动
        {
            dragEndMousePosition = ScreenPointToWorldPoint();
            float xChangeValue = (dragEndMousePosition.x - dragBeginMousePosition.x) / 100;//该值不准确,应该与缩放值相关
            float yChangeValue = (dragEndMousePosition.y - dragBeginMousePosition.y) / 100;//该值不准确,应该与缩放值相关
            MiniMapCamera.transform.position = startPosition + new Vector3(-xChangeValue, 0, -yChangeValue);//移动
        }
        if (Input.GetMouseButtonUp(0))
        {
            startPosition = MiniMapCamera.transform.position;//更新Camera开始位置
        }
    }

    /// <summary>
    /// 将屏幕坐标转换为世界坐标
    /// </summary>
    /// <returns></returns>
    Vector3 ScreenPointToWorldPoint()
    {
        Vector3 position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        return position;
    }
}
