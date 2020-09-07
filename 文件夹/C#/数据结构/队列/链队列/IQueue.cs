using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IQueue<T>
{
    void Clear();
    void Enqueue(T item);
    int Count { get; }
    bool IsEmpty();
    T Dequeue();
    T Peek();

}
