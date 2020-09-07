using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LinkQueue<T> : IQueue<T>
{
    private Node<T> top;//栈顶元素节点
    private int count = 0;//栈中元素个数

    public void Clear()
    {
        count = 0;
        top = null;
    }

    public int Length()
    {
        return count;
    }

    public void Enqueue(T item)
    {
        Node<T> newNode = new Node<T>(item);
        if (top == null)
        {
            top = newNode;
            count++;
        }
        else
        {
            Node<T> temp = top;
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            temp.Next = newNode;
            count++;
        }
    }

    public int Count
    {
        get { return count; }
    }
    public bool IsEmpty()
    {
        return count == 0;
    }

    public T Dequeue()
    {
        if (top.Next == null)
        {
            count--;
            return top.Data;
        }
        else
        {
            Node<T> tempNode = top;
            top = top.Next;
            count--;
            return tempNode.Data;
        }
    }

    public T Peek()
    {
        return top.Data;
    }
}
