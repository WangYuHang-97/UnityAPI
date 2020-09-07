using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqQueueTest : MonoBehaviour
{
    void Start()
    {
        SeqQueue<int> seqQueue = new SeqQueue<int>();
        for (int i = 0; i < 3; i++)
        {
            seqQueue.Enqueue(i);
        }
        print("展示队列顶元素:" + seqQueue.Peek());
        print("队列数量:" + seqQueue.Length());
        for (int i = 0; i < 3; i++)
        {
            print(seqQueue.Dequeue());
        }
    }
}
