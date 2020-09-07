using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LinkStack<T> : IStackDS<T>
{
    private Node<T> top;//栈顶元素节点
    private int count = 0;//栈中元素个数

    public int Count//取得栈中元素个数
    {
        get { return count; }
    }
    public int GetLength()//取得栈中元素个数
    {
        return count;
    }

    public bool IsEmpty()//判断栈中是否有数据
    {
        return count == 0;
    }

    public void Clear()//清空栈中所有数据
    {
        count = 0;
        top = null;
    }

    public void Push(T item)//入栈
    {
        //把新元素作为栈顶
        Node<T> newNode = new Node<T>(item);
        newNode.Next = top;
        top = newNode;
        count++;
    }

    public T Pop()//取得栈顶元素后删除
    {
        T tempNode = top.Data;
        top = top.Next;
        count--;
        return tempNode;
    }

    public T Peek()//取得栈顶元不删除
    {
        return top.Data;
    }
}
