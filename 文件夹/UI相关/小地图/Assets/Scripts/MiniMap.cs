using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMap: MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //引入模块

    //需自己拖拽赋值
    public Camera MiniMapCamera;
    public Canvas canvas;

    private Button initButton;

    //属性值
    private bool isInMap;//是否在小地图内
    private float ZoomFilled = 1; //缩放布满值,想要投放的物体,(总长与总宽中的最大值)/2，即可布满全Camera
    private float ZoomMax;// 缩放最大值,布满Size*1.25，得到一个比较好的视野
    private float ZoomMin;//缩放最小值,缩放最大值的1/5,较为合适(可以自己改变)
    private int zoomSpeed = 200;//缩放速度
    private Vector2 dragBeginMousePosition;//开始拖拽位置
    private Vector2 dragEndMousePosition;//结束拖拽位置
    private Vector3 startPosition;//Camera开始位置
    private Vector3 currentPosition;//Camera现位置

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
        initButton = transform.Find("initButton").GetComponent<Button>();//重置按钮初始化
        ZoomMax = ZoomFilled * 1.25f;// 缩放最大值赋值
        ZoomMin = ZoomMax / 5;//缩放最小值赋值
        startPosition = currentPosition = MiniMapCamera.transform.position;
        initButton.onClick.AddListener(() =>
        {
            MiniMapCamera.transform.position = startPosition;
            MiniMapCamera.orthographicSize = ZoomMax;
        });//重置按钮方法
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
        if (MiniMapCamera.orthographicSize >= ZoomMax)//安全校验防止超过阈值
        {
            MiniMapCamera.orthographicSize = ZoomMax;
        }
        if (MiniMapCamera.orthographicSize <= ZoomMin)//安全校验防止超过阈值
        {
            MiniMapCamera.orthographicSize = ZoomMin;
        }
        if (scrollWheelValue != 0 )//安全校验
        {
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
        if (Input.GetMouseButtonDown(0))//获得拖拽起始点
        {
            dragBeginMousePosition = ScreenPointToWorldPoint();
        }
        if (Input.GetMouseButton(0))//实时更新拖拽点并移动
        {
            dragEndMousePosition = ScreenPointToWorldPoint();//获得拖拽终止点
            float xProportion = (dragEndMousePosition.x - dragBeginMousePosition.x) / //获得X方向相对移动比例
                                ((RectTransform)transform).rect.width;
            float yProportion = (dragEndMousePosition.y - dragBeginMousePosition.y) / //获得Y方向相对移动比例
                                ((RectTransform)transform).rect.height;
            float xChangeValue = xProportion * (MiniMapCamera.orthographicSize / ZoomFilled) * 2; //实际X位移量 = X方向相对移动比例 * 单位长度 * Camera与屏幕比例
            float yChangeValue = yProportion * (MiniMapCamera.orthographicSize / ZoomFilled) * 2; //实际Y位移量 = Y方向相对移动比例 * 单位长度 * Camera与屏幕比例
            MiniMapCamera.transform.position = currentPosition + new Vector3(-xChangeValue, 0, -yChangeValue);//移动
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentPosition = MiniMapCamera.transform.position;//更新Camera开始位置
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
