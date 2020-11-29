using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyUI
{
    public class DragBox : MonoBehaviour
    {
        private Vector2 startPosition;//起始位置坐标
        private Vector2 endPosition;//终止位置坐标
        private bool mouseIsDown;//检测鼠标是否按下
        private bool rectMaking;//检测是否可以开始绘制矩形框

        void Update()
        {
            GetMousePosition();
        }

        /// <summary>
        /// 获取两个坐标
        /// </summary>
        void GetMousePosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition;//起始坐标获得
                mouseIsDown = true;//开始获取终止坐标
                rectMaking = true;//开始绘制矩形框
            }
            if (mouseIsDown)
            {
                endPosition = Input.mousePosition;
                if (Input.GetMouseButtonUp(0))
                {
                    mouseIsDown = false;//成功获取终止坐标
                    rectMaking = false;//绘制矩形框完成
                }
            }
        }

        /// <summary>
        /// 类似于Update,用于绘制图框
        /// </summary>
        void OnGUI()
        {
            if (rectMaking)
            {
                Vector2 origin = startPosition;
                //由于UI图形起点在左上方,而Screen起点在左下方
                origin.x = Mathf.Min(startPosition.x, endPosition.x);
                origin.y = Mathf.Max(startPosition.y, endPosition.y);
                origin.y = Screen.height - origin.y;
                Vector2 size = endPosition - startPosition;
                size.x = Mathf.Abs(size.x);
                size.y = Mathf.Abs(size.y);
                Rect rect = new Rect(origin, size);
                GUI.Box(rect, "");
                Vector3 pos = Camera.main.WorldToScreenPoint(new Vector2(2, 2));
                if (rect.Contains(pos))//用于检测某物体是否在框内(需要转化为屏幕坐标)
                {
                    print("包含该坐标");
                }
            }
        }
    }
}

