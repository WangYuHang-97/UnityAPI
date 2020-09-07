using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkListTest : MonoBehaviour
{
    void Start()
    {
        LinkList<int> linkList = new LinkList<int>();
        for (int i = 0; i < 5; i++)
        {
            linkList.Add(i);
        }
        print("链表长度：" + linkList.GetLength());
        print("链表是否为空：" + linkList.IsEmpty());
        linkList.AddFirst(100);
        linkList.AddLast(-100);
        linkList.AddAfter(5, linkList.GetNode(4));
        linkList.AddBefore(-1, linkList.GetNode(0));
        linkList.Delete(5);
        for (int i = 0; i < linkList.GetLength(); i++)
        {
            print(linkList[i]);
        }
        print("4的具体位置："+linkList.Locate(4));
    }
}
