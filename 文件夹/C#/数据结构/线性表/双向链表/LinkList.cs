/// <summary>
/// 链式线性表
/// </summary>
/// <typeparam name="T">泛型</typeparam>
class LinkList<T> : IInterface<T>
{
    private Node<T> head;

    private Node<T> tail;

    public LinkList()
    {
        head = null;
        tail = null;
    }

    /// <summary>
    /// 获得长度
    /// </summary>
    /// <returns></returns>
    public int GetLength()
    {
        if (head == null)
        {
            return 0;
        }
        Node<T> temp = head;
        int count = 1;
        while (true)
        {
            if (temp.Next != null)
            {
                count++;
                temp = temp.Next;
            }
            else
            {
                break;
            }
        }
        return count;
    }

    /// <summary>
    /// 清除数据
    /// </summary>
    public void Clear()
    {
        head = null;
        tail = null;
    }

    /// <summary>
    /// 判断是否为空
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return head == null;
    }

    /// <summary>
    /// 添加数据（时间复杂度O(1)）
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {
        Node<T> newNode = new Node<T>(item);//若头节点为空，则该节点为头节点
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Last = tail;
            tail = newNode;
        }
    }

    /// <summary>
    /// 插入数据（时间复杂度O(1)~O(n)）
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    public void Insert(T item, int index)
    {
        Node<T> newNode = new Node<T>(item);
        if (index == 0) //插入到头节点
        {
            newNode.Next = head;
            head.Last = newNode;
            head = newNode;
        }
        else
        {
            Node<T> temp = head;
            for (int i = 0; i <= index - 1; i++)
            {
                temp = temp.Next;
            }
            Node<T> preNode = temp;
            Node<T> currentNode = temp.Next;
            preNode.Next = newNode;
            newNode.Last = preNode;
            newNode.Next = currentNode;
            currentNode.Last = newNode;
        }
    }

    /// <summary>
    /// 设置链头
    /// </summary>
    /// <param name="item">数据</param>
    public void AddFirst(T item)
    {
        Insert(item,0);
    }

    /// <summary>
    /// 设置链尾
    /// </summary>
    /// <param name="item">数据</param>
    public void AddLast(T item)
    {
        Add(item);
    }

    /// <summary>
    /// 获得该Item的Node
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public Node<T> GetNode(T item)
    {
        Node<T> temp = head;
        while (true)
        {
            if (temp.Data.Equals(item)) return temp;
            else
            {
                if (temp.Next!=null) temp = temp.Next;
                else break;
            }
        }
        return null;
    }

    /// <summary>
    /// 在某项后添加
    /// </summary>
    /// <param name="item">欲添加项</param>
    /// <param name="node">该项后</param>
    public void AddAfter(T item,Node<T> node)
    {
        Node<T> newNode = new Node<T>(item);
        if (node == tail)
        {
            node.Next = newNode;
            newNode.Last = node;
            tail = newNode;
        }
        else
        {
            Node<T> nextNode = node.Next;
            node.Next = newNode;
            newNode.Last = node;
            newNode.Next = nextNode;
            nextNode.Last = newNode;
        }

    }

    /// <summary>
    /// 在某项前添加
    /// </summary>
    /// <param name="item">欲添加项</param>
    /// <param name="node">该项后</param>
    public void AddBefore(T item, Node<T> node)
    {
        Node<T> newNode = new Node<T>(item);
        if (node == head)
        {
            node.Last = newNode;
            newNode.Next = node;
            head = newNode;
        }
        else
        {
            Node<T> lastNode = node.Last;
            node.Last = newNode;
            newNode.Next = node;
            newNode.Last = lastNode;
            lastNode.Next = newNode;
        }
    }

    /// <summary>
    /// 删除指定位置
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T Delete(int index)
    {
        T data = default(T);
        if (index == 0)
        {
            data = head.Data;
            head = head.Next;
        }
        else
        {
            Node<T> temp = head;
            for (int i = 0; i < index - 1; i++)
            {
                temp = temp.Next;
            }
            Node<T> preNode = temp;
            Node<T> currentNode = temp.Next;
            data = currentNode.Data;
            preNode.Next = currentNode.Next;
            currentNode.Next.Last = preNode;
        }
        return data;
    }

    /// <summary>
    /// 获得指定位置数据
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T this[int index]
    {
        get
        {
            Node<T> temp = head;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            };
            return temp.Data;
        }
    }

    /// <summary>
    /// 获得指定位置数据
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T GetItem(int index)
    {
        return this[index];
    }

    /// <summary>
    /// 获得某个Item位置
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int Locate(T value)
    {
        Node<T> temp = head;
        if (temp == null)
        {
            return -1;
        }
        else
        {
            int index = 0;
            while (true)
            {
                if (temp.Data.Equals(value))
                {
                    return index;
                }
                else
                {
                    if (temp.Next != null)
                    {
                        temp = temp.Next;
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return -1;
        }
    }
}
