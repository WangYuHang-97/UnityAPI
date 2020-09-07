using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 插入排序 : MonoBehaviour
{
    void InsertSort(int[] dataArray)
    {
        for (int i = 1; i < dataArray.Length; i++)
        {
            int iValue = dataArray[i];
            bool isInsert = false;
            for (int j = i - 1; j >= 0; j--)//向前遍历，若j大于i，者j向后移动一位，直到j小于i，i填入j+1位置
            {
                if (dataArray[j] > iValue)
                {
                    dataArray[j + 1] = dataArray[j];
                }
                else
                {
                    dataArray[j + 1] = iValue;
                    isInsert = true;
                    break;
                }
            }
            if (isInsert == false)
            {
                dataArray[0] = iValue;
            }
        }
    }
}
