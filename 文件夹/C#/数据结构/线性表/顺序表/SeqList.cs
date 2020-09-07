using System;

class SeqList<T> : IInterface<T>
{
    private T[] data;//用来存储数据
    private int count = 0;//表示存储了多少数据

    public SeqList(int size)//size为最大容量
    {
        data = new T[size];
        count = 0;
    }

    public SeqList() : this(10)
    {
    }

    public int GetLength()//获得数据个数
    {
        return count;
    }

    public void Clear()
    {
        count = 0;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public void Add(T item)
    {
        if (count == data.Length)//说明当前数组满
        {
            Console.WriteLine("当前数组存满，不允许存储");
        }
        else
        {
            data[count] = item;
            count++;
        }
    }

    public void Insert(T item, int index)
    {
        for (int i = count - 1; i >= count; i--)
        {
            data[i + 1] = data[i];
        }
        data[index] = item;
        count++;
    }

    public T Delete(int index)
    {
        T temp = data[index];
        for (int i = index + 1; i < count; i++)
        {
            data[i - 1] = data[i];
        }
        count--;
        return temp;
    }

    public T this[int index]
    {
        get { return GetItem(index); }
    }

    public T GetItem(int index)
    {
        if (index >= 0 && index <= count - 1)
        {
            return data[index];
        }
        else
        {
            Console.WriteLine("索引不存在");
            return default(T);
        }
    }

    public int Locate(T value)
    {
        for (int i = 0; i < count; i++)
        {
            if (data[i].Equals(value))
            {
                return i;
            }
        }
        return -1;
    }
}
