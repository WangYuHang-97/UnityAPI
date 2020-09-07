using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqListTest : MonoBehaviour
{
    void Start()
    {
        SeqList<int> seqList= new SeqList<int>();
        for (int i = 0; i < 5; i++)
        {
            seqList.Add(i);
        }
        print("顺序表长度："+seqList.GetLength());
        print("顺序表是否为空：" + seqList.IsEmpty());
        seqList.Insert(5,5);
        seqList.Delete(0);
        for (int i = 0; i < seqList.GetLength(); i++)
        {
            print(seqList[i]);
        }
        print("5的位置:"+seqList.Locate(5));
    }
}
