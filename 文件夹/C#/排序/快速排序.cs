using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 快速排序 : MonoBehaviour
{
    void QuickSort(int[] dataArray, int left, int right)//left开始索引 right结束索引
    {
        if (left < right)
        {
            int x = dataArray[left];//基准数
            int i = left;
            int j = right;

            while (true && i < j)
            {
                //从前往后，找到一个比基准数小的数字
                while (true && i < j)
                {
                    if (dataArray[j] <= x)//找到一个比基准数小数字
                    {
                        dataArray[i] = dataArray[j];
                        break; ;
                    }
                    else
                    {
                        j--;
                    }
                }
                //从后往前，找到一个比基准数大的数字
                while (true && i < j)
                {
                    if (dataArray[i] > x)
                    {
                        dataArray[j] = dataArray[i];
                        break; ;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            dataArray[i] = x;//left - i - right
            QuickSort(dataArray, left, i - 1);
            QuickSort(dataArray, i + 1, right);
        }
    }
}
