using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SeqQueue<T> : IQueue<T>
{
    private T[] data;
    private int count;

    public SeqQueue(int size)
    {
        data = new T[size];
        count = 0;
    }

    public SeqQueue() : this(10)
    {

    }

    public int Length()
    {
        return count;
    }

    public void Clear()
    {
        count = 0;
        data[0] = default(T);
    }

    public void Enqueue(T item)
    {
        data[count] = item;
        count++;
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
        if (count == 1)
        {
            T temp = data[0];
            data[0] = default(T);
            count--;
            return temp;
        }
        else
        {
            T temp = data[0];
            for (int i = 0; i < count - 1; i++)
            {
                data[i] = data[i + 1];
            }
            count--;
            return temp;
        }
    }

    public T Peek()
    {
        return data[0];
    }
}
