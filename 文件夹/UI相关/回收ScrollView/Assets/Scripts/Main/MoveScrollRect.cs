using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class MoveScrollRect : MonoBehaviour
{
    private float _beforeY = 1;
    private ScrollRect _scrollRect;
    private GridLayoutGroup _layout;
    private int _totalColumn;//总行
    private int _totalRow;//总列
    private int _makeRow;
    private int ShowRow => _makeRow - 1;
    private object[]_datas;

    private Action _refresh;

    public void RefreshData(List<object> datas)
    {
        _datas = new object[]{datas};
        _refresh?.Invoke();
    }


    public void RefreshData(object[] datas)
    {
        _datas = datas;
        _refresh?.Invoke();
    }

    public void Init<T>(List<object> datas) where T : MonoBehaviour
    {
        Init<T>(new object[] {datas});
    }

    public void Init<T>(object[] datas) where T : MonoBehaviour 
    {
        _scrollRect = GetComponent<ScrollRect>();
        _scrollRect.content.GetComponent<RectTransform>().pivot = new Vector2(0.5f,1);
        _scrollRect.normalizedPosition = new Vector2(0, 1);

        _layout = _scrollRect.content.GetComponent<GridLayoutGroup>();
        var search = new Dictionary<int, List<T>>();
        _datas = datas;
        GetNum(_scrollRect);
        _totalRow = ScrollRectAdapt(_scrollRect.content, ((List<object>)datas[0]).Count, _totalColumn);
        for (int i = 1; i <= _makeRow; i++)
        {
            MakeItem(i);
        }

        _refresh = () =>
        {
            foreach (var kv in search)
            {
                var makeRow = kv.Key;
                for (int i = 0; i < kv.Value.Count; i++)
                {
                    int index = (makeRow - 1) * _totalColumn + i;
                    kv.Value[i].InvokeByReflect("Init", GetData(_datas, index));
                }
            }
        };

        _scrollRect.onValueChanged.AddListener(position =>
        {
            bool up = !(_beforeY > position.y);
            _beforeY = position.y;
            var nowRow = GetLine(up);
            var needMakeRow = GetNeedRow(nowRow, up);
            var needRecyleRow = GetNeedRow(nowRow, !up);
            MakeItem(needMakeRow, needRecyleRow);
        });

        void MakeItem(int makeRow, int recycleRow = -1)
        {
            if (search.ContainsKey(makeRow))
            {
                return;
            }
            if (recycleRow == -1 || !search.ContainsKey(recycleRow))
            {
                for (int column = 1; column <= _totalColumn; column++)
                {
                    int index = (makeRow - 1) * _totalColumn + (column - 1);
                    var item = Instantiate(Resources.Load<GameObject>(typeof(T).Name)).GetComponent<T>();
                    item.transform.SetParent(_scrollRect.content);
                    //var item = FactoryManager.GetPrefabSync(typeof(T).Name, Vector3.zero,//这是我框架自己写的方法
                    //    _scrollRect.content).GetComponent<T>();
                    item.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                    item.transform.localPosition = GetPosition(makeRow, column);
                    item.InvokeByReflect("Init", GetData(_datas, index));

                    if (search.ContainsKey(makeRow))
                    {
                        search[makeRow].Add(item);
                    }
                    else
                    {
                        search.Add(makeRow, new List<T>() { item });
                    }
                }
            }
            else
            {
                var recycleItems = search[recycleRow];
                search.Remove(recycleRow);
                for (int column = 1; column <= _totalColumn; column++)
                {
                    int index = (makeRow - 1) * _totalColumn + (column - 1);
                    var item = recycleItems[column - 1];
                    item.transform.localPosition = GetPosition(makeRow, column);
                    item.InvokeByReflect("Init", GetData(_datas,index));

                    if (search.ContainsKey(makeRow))
                    {
                        search[makeRow].Add(item);
                    }
                    else
                    {
                        search.Add(makeRow, new List<T>() { item });
                    }
                }
            }
        }
    }

    object[] GetData(object[] originDatas,int index)
    {
        var datas = new object[originDatas.Length];
        for (int i = 0; i < originDatas.Length; i++)
        {
            var data = (List<object>)originDatas[i];
            if (data == null || index >= data.Count || data[index] == null)
            {
                datas[i] = null;
            }
            else
            {
                datas[i] = data[index];
            }
        }
        return datas;
    }

    int ScrollRectAdapt(RectTransform content, int childCount, int numPerLine)
    {
        var num = childCount % numPerLine > 0 ? childCount / numPerLine + 1 : childCount / numPerLine;
        if (_layout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
        {
            content.sizeDelta = new Vector2(num * _layout.cellSize.x
                                            + (num - 1) * _layout.spacing.x + _layout.padding.left, 0);
        }
        else
        {
            content.sizeDelta = new Vector2(0, num * _layout.cellSize.y
                                               + (num - 1) * _layout.spacing.y + _layout.padding.top);
        }
        return num;
    }

    void GetNum(ScrollRect scrollRect)
    {
        _totalColumn = 0;
        _layout.enabled = false;
        switch (_layout.constraint)
        {
            case GridLayoutGroup.Constraint.Flexible:
                Debug.LogError("没有设置Constraint");
                break;
            case GridLayoutGroup.Constraint.FixedColumnCount:
            case GridLayoutGroup.Constraint.FixedRowCount:
                _totalColumn = _layout.constraintCount;
                break;
        }
        var num = (int)(scrollRect.GetComponent<RectTransform>().rect.height /
                         (_layout.cellSize.y + _layout.spacing.y)) + 2;
        var dataNum = ((List<object>)_datas[0]).Count;
        _makeRow = dataNum < num ? dataNum : num;
    }



    Vector3 GetPosition(int row, int column)
    {
        var yStart = _layout.padding.top;
        var y = row * _layout.cellSize.y + (row - 1) * _layout.spacing.y;
        var midX = (_totalColumn + 1) / 2f;
        var xStart = _layout.padding.left;
        float x = 0;
        if (_totalColumn!=1)
        {
            var offset = Mathf.Abs(column - midX) * (_layout.cellSize.x + _layout.spacing.x);
            if (column < midX)
            {
                x = -offset;
            }
            else
            {
                x = offset;
            }
        }
        return new Vector3(xStart + x, -(yStart + y));
    }

    int GetLine(bool up)
    {
        if (up)
        {
            var contentDY = _scrollRect.content.anchoredPosition.y +
                            _scrollRect.viewport.GetComponent<RectTransform>().rect.height;
            int row = (int)(contentDY / (_layout.cellSize.y + _layout.spacing.y)) + 1;
            return row - (ShowRow - 1);
        }
        else
        {
            var contentUY = _scrollRect.content.anchoredPosition.y - _layout.padding.top;
            int row = (int)(contentUY / (_layout.cellSize.y + _layout.spacing.y)) + 1;
            return row;
        }
    }

    int GetNeedRow(int row, bool up)
    {
        if (up)
        {
            return row - 1 < 1 ? 1 : row - 1;
        }
        else
        {
            return row + ShowRow > _totalRow ? 1 : row + ShowRow;
        }
    }
}
