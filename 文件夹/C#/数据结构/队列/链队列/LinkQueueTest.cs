using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkQueueTest : MonoBehaviour
{
    void Start()
    {
        LinkQueue<int> linkQueue = new LinkQueue<int>();
        for (int i = 0; i < 3; i++)
        {
            linkQueue.Enqueue(i);
        }
        print("展示队列顶元素:" + linkQueue.Peek());
        print("队列数量:" + linkQueue.Length());
        for (int i = 0; i < 3; i++)
        {
            print(linkQueue.Dequeue());
        }
    }
}
