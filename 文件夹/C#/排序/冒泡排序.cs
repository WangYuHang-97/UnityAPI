using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 冒泡排序 : MonoBehaviour
{
    void BubbleSort(int[] dataArray)
    {
        for (int i = 0; i < dataArray.Length-1; i++)
        {
            for (int j = i+1; j < dataArray.Length; j++)
            {
                if (dataArray[i]>dataArray[j])
                {
                    int temp = dataArray[i];
                    dataArray[i] = dataArray[j];
                    dataArray[j] = temp;
                }
            }
        }
    }

}
