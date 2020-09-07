interface IInterface<T>
{
    int GetLength();
    void Clear();
    bool IsEmpty();
    void Add(T item);
    void Insert(T item, int index);
    T Delete(int index);
    T this[int index] { get; }
    T GetItem(int index);
    int Locate(T value);
}
