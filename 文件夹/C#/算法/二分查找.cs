using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 二分查找 : MonoBehaviour
{
    int GetIndex(int[] a, int n) //迭代算法(若不存在返回-1)
    {
        int left = 0;//左索引
        int right = a.Length;//右索引
        while (right - left != 1)
        {
            int mid = left + (right - left) / 2;//中位索引
            if (n < a[mid]) right = mid;//改变右索引
            else if (n > a[mid]) left = mid;//改变左索引
            else return mid;//找到正确值
        }
        return -1;//不存在该值
    }

    int GetIndex(int[] a, int n, int left, int right) //递归算法(若不存在返回-1)
    {
        if (right - left == 1) return -1;//不存在该值
        int mid = left + (right - left) / 2;//中位索引
        if (n < a[mid]) return GetIndex(a, n, left, mid);//改变右索引
        else if (n > a[mid]) return GetIndex(a, n, mid, right); //改变右索引
        else return mid;//找到正确值
    }
}
