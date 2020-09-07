/// <summary>
/// 数据
/// </summary>
/// <typeparam name="T">泛型</typeparam>
class Node<T>
{
    private T data;//数据载体
    private Node<T> next;//下一个数据
    private Node<T> last;//上一个数据

    #region 构造函数
    public Node()
    {
        data = default(T);
        next = null;
    }

    public Node(T value)
    {
        data = value;
        next = null;
    }

    public Node(T value, Node<T> next)
    {
        this.data = value;
        this.next = next;
    }

    public Node(Node<T> next)
    {
        this.next = next;
    }
    #endregion

    #region 属性
    public T Data
    {
        get { return data; }
        set { data = value; }
    }

    public Node<T> Next
    {
        get { return next; }
        set { next = value; }

    }

    public Node<T> Last { get => last; set => last = value; }
    #endregion
}
