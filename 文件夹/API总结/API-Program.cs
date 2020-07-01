using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAPI
{
    /// <summary>
    /// <para>数学模块</para>
    /// <para>包含算法处理方法</para>
    /// </summary>
    class MathfAPI
    {
        /// <summary>
        /// 当num处于(num,num+1)时返回num+1
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        int CeilToInt(float num)
        {
            return Mathf.CeilToInt(num);
        }

        /// <summary>
        /// 当num处于(num,num+1)时返回num
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        int FloorToInt(float num)
        {
            return Mathf.FloorToInt(num);
        }

    }

    /// <summary>
    /// <para>坐标模块</para>
    /// <para>包含坐标转换方法</para>
    /// </summary>
    class Coordinate
    {
        private Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        /// <summary>
        /// <para>将屏幕鼠标坐标导出Local坐标</para>
        /// <para>Canvas模式-ScreenSpace-Overlay</para>
        /// <para>其中Local原点坐标坐标为Canvas中心点,左下为（-Width/2,-Height/2）,右上为(Width/2,Height/2)</para>
        /// </summary>
        /// <returns></returns>
        Vector2 ScreenPointToLocalPoint()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            return position;
        }

        /// <summary>
        /// <para>将屏幕鼠标坐标导出World坐标</para>
        /// <para>Canvas模式-ScreenSpace-Overlay</para>
        /// <para>与WorldToScreenPoint相对应</para>
        /// <para>其中World原点坐标坐标为Canvas左下角,右上为(Width,Height)</para>
        /// </summary>
        /// <returns></returns>
        Vector3 ScreenPointToWorldPoint()
        {
            Vector3 position;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            return position;
        }

        /// <summary>
        /// 将屏幕坐标传递为视口坐标,左下(0,0),右上(1，1)
        /// </summary>
        /// <returns></returns>
        Vector3 ScreenToViewportPoint()
        {
            return Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        /// <summary>
        /// <para>将物体的世界坐标转化为屏幕坐标,中心(Width/2,Height/2)</para>
        /// <para>与ScreenPointToWorldPoint相对应</para>
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        Vector3 WorldToScreenPoint(GameObject go)
        {
            return Camera.main.WorldToScreenPoint(go.transform.position);
        }

        /// <summary>
        /// <para>将视口坐标传递为屏幕坐标,左下(0，0)右上(Width,Height)</para>
        /// </summary>
        /// <returns></returns>
        Vector3 ViewportToScreenPoint()
        {
            return Camera.main.ViewportToScreenPoint(new Vector3(1, 1));
        }
    }

    /// <summary>
    /// <para>事件模块</para>
    /// <para>包含各种检测方法</para>
    /// </summary>
    class EventSystem
    {
        /// <summary>
        /// 用于检测鼠标下面是否有UI(需点击)
        /// </summary>
        /// <returns></returns>
         bool IsPointerOverGameObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            }
            return false;
        }
    }

    /// <summary>
    /// <para>Tranfrom模块</para>
    /// <para>包含移动方法</para>
    /// </summary>
    class Tranfrom : UnityEngine.Transform
    {
        /// <summary>
        /// 若localRotation变化，则旋转一倍Rotation变化值进行移动
        /// </summary>
        void TransformForwardWorld()
        {
            transform.Translate(transform.forward * Time.deltaTime, Space.World);
        }
        /// <summary>
        /// 若localRotation变化，则旋转二倍Rotation变化值进行移动(在world基础上+1)
        /// </summary>
        void TransformForwardSelf()
        {
            transform.Translate(transform.forward * Time.deltaTime, Space.Self);
        }
        /// <summary>
        /// 朝world-forward移动
        /// </summary>
        void Vector3ForwardWorld()
        {
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);
        }
        /// <summary>
        /// 朝local-forward移动
        /// </summary>
        void Vector3ForwardSelf()
        {
            transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
        }
    }

    class Mesh
    {
        /// <summary>
        /// <para>用于合并多个gameObject的mesh成一个mesh(未赋material)</para>
        /// <para>可以减少Batche优化性能</para>
        /// </summary>
        void MeshCombine()
        {
            GameObject go = GameObject.Find("挂载物体");//空gameobject,用于挂载新生成Mesh
            MeshFilter goMeshFIlter = go.AddComponent<MeshFilter>();//添加MeshFilter组件
            go.AddComponent<MeshRenderer>();//添加MeshFilter组件MeshRenderer
            MeshFilter[] filters = go.GetComponentsInChildren<MeshFilter>();//获取子物体MeshFilter组件集合
            CombineInstance[] combiners = new CombineInstance[filters.Length];//创建新合成Mesh集合
            for (int i = 0; i < filters.Length; i++)//将子物体MeshFilter全部导入至新合成Mesh集合
            {
                combiners[i].mesh = filters[i].sharedMesh;
                combiners[i].transform = filters[i].transform.localToWorldMatrix;
            }
            UnityEngine.Mesh finalMesh = new UnityEngine.Mesh();//最终Mesh初始化
            finalMesh.CombineMeshes(combiners);//最终Mesh生成
            goMeshFIlter.sharedMesh = finalMesh;//将最终Mesh赋与挂载物体的MeshFilter组件
        }
    }
}