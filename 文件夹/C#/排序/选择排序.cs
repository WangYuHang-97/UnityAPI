using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 选择排序 : MonoBehaviour
{
    void SelectSort(int[] dataArray)
    {
        for (int i = 0; i < dataArray.Length - 1; i++)
        {
            int min = dataArray[i];
            int minIndex = i;
            for (int j = i + 1; j < dataArray.Length; j++)
            {
                if (dataArray[j] < min)
                {
                    min = dataArray[j];
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                int temp = dataArray[i];
                dataArray[i] = dataArray[minIndex];
                dataArray[minIndex] = temp;
            }
        }
    }
}
