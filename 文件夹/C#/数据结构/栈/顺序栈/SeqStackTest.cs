using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeqStackTest : MonoBehaviour
{
    void Start()
    {
        SeqStack<int> seqStack = new SeqStack<int>();
        for (int i = 0; i < 3; i++)
        {
            seqStack.Push(i);
        }
        print("展示栈顶元素:"+seqStack.Peek());
        print("栈数量:"+seqStack.GetLength());
        for (int i = 0; i < 3; i++)
        {
            print(seqStack.Pop());
        }
    }

}
