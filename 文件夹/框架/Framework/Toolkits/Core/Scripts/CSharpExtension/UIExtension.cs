using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyNamespace
{
    public static class UIExtension
    {
        /// <summary>
        /// 图片自动调整，用于图片尺寸不一样时
        /// </summary>
        /// <param name="img"></param>
        /// <param name="target"></param>
        /// <param name="spriteName"></param>
        public static void SpriteAdjust(this Image img, float target)
        {
            img.SetNativeSize();
            float max = Mathf.Max(img.GetComponent<RectTransform>().rect.width,
                img.GetComponent<RectTransform>().rect.height);
            float scale = target / max;
            img.transform.localScale = new Vector3(scale, scale, 1);
        }

        /// <summary>
        /// 调整滑动面板目录大小
        /// </summary>
        /// <param name="content"></param>
        /// <param name="numPerLine"></param>
        /// <param name="offset">偏移量</param>
        /// <param name="scrollRect"></param>
        public static void ScrollRectAdapt(this ScrollRect scrollRect ,int numPerLine = 1, float offset = 0)
        {
            var content = scrollRect.content;
            var num = content.childCount % numPerLine > 0 ? content.childCount / numPerLine + 1 : content.childCount / numPerLine;
            ScrollRectAdaptBase(scrollRect, num, offset);
        }

        public static void ScrollRectAsynAdapt(this ScrollRect scrollRect, int count, int numPerLine = 1, float offset = 0)
        {
            var num = count % numPerLine > 0 ? count / numPerLine + 1 : count / numPerLine;
            ScrollRectAdaptBase(scrollRect, num, offset);
        }

        static void ScrollRectAdaptBase(ScrollRect scrollRect, int num, float offset = 0)
        {
            var content = scrollRect.content;
            GridLayoutGroup gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
            if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedRowCount)
            {
                content.sizeDelta = new Vector2(num * gridLayoutGroup.cellSize.x
                                                + (num - 1) * gridLayoutGroup.spacing.x + gridLayoutGroup.padding.left + offset, 0);
            }
            else
            {
                content.sizeDelta = new Vector2(0, num * gridLayoutGroup.cellSize.y
                                                   + (num - 1) * gridLayoutGroup.spacing.y + gridLayoutGroup.padding.top + offset);
            }
        }
    }
}