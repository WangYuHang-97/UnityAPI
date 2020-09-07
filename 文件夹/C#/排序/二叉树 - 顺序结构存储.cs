using System;

namespace 二叉树___顺序结构存储
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] data = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            BiTree<char> tree = new BiTree<char>(10);
            for (int i = 0; i < data.Length; i++)
            {
                tree.Add(data[i]);
            }
            tree.FirstTravers();
            Console.WriteLine();
            tree.MiddleTravers();
            Console.WriteLine();
            tree.LastTravers();
            Console.WriteLine();
            tree.LayerTravers();
            Console.WriteLine();
            Console.ReadKey();
        }
    }


    class BiTree<T>
    {
        private T[] data;
        private int count = 0;//数据索引
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="capacity">存储数据个数</param>
        public BiTree(int capacity)
        {
            data = new T[capacity];
        }

        public bool Add(T item)
        {
            if (count >= data.Length)
            {
                return false;
            }
            data[count] = item;
            count++;
            return true;
        }

        /// <summary>
        /// 前序遍历（先输出当前结点的数据，再依次遍历输出左结点和右结点）
        /// </summary>
        public void FirstTravers()
        {
            FirstTravers(0);
        }

        /// <summary>
        /// 中序遍历（先遍历输出左结点，再输出当前结点的数据，再遍历输出右结点）
        /// </summary>
        public void MiddleTravers()
        {
            MiddleTravers(0);
        }

        /// <summary>
        /// 后序遍历（先遍历输出左结点，再遍历输出右结点，最后输出当前结点的数据）
        /// </summary>
        public void LastTravers()
        {
            LastTravers(0);
        }

        /// <summary>
        /// 前序遍历封装方法（先输出当前结点的数据，再依次遍历输出左结点和右结点）
        /// </summary>
        /// <param name="index">索引(编号-1)</param>
        private void FirstTravers(int index)
        {
            if (index>=count)
            {
                return;
            }
            int number = index + 1;//当前节点编号
            if (data[index].Equals(-1)) //-1时为空结点
            {
                return;
            }
            Console.Write(data[index] + " ");
            int leftNumber = number * 2;//左子结点编号
            int rightNumber = number * 2 + 1;//右子节点编号
            FirstTravers(leftNumber - 1);
            FirstTravers(rightNumber - 1);
        }

        /// <summary>
        /// 中序遍历封装方法（先遍历输出左结点，再输出当前结点的数据，再遍历输出右结点）
        /// </summary>
        /// <param name="index">索引(编号-1)</param>
        private void MiddleTravers(int index)
        {
            if (index >= count)
            {
                return;
            }
            int number = index + 1;//当前节点编号
            if (data[index].Equals(-1)) //-1时为空结点
            {
                return;
            }
            int leftNumber = number * 2;//左子结点编号
            int rightNumber = number * 2 + 1;//右子节点编号
            MiddleTravers(leftNumber - 1);
            Console.Write(data[index] + " ");
            MiddleTravers(rightNumber - 1);
        }

        /// <summary>
        /// 后序遍历封装方法（先遍历输出左结点，再遍历输出右结点，最后输出当前结点的数据）
        /// </summary>
        /// <param name="index">索引(编号-1)</param>
        private void LastTravers(int index)
        {
            if (index >= count)
            {
                return;
            }
            int number = index + 1;//当前节点编号
            if (data[index].Equals(-1)) //-1时为空结点
            {
                return;
            }
            int leftNumber = number * 2;//左子结点编号
            int rightNumber = number * 2 + 1;//右子节点编号
            LastTravers(leftNumber - 1);
            LastTravers(rightNumber - 1);
            Console.Write(data[index] + " ");
        }

        /// <summary>
        /// 中序遍历（从树的第一层开始，从上到下逐层遍历，在同一层中，从左到右对结点 逐个访问输出）
        /// </summary>
        public void LayerTravers()
        {
            for (int i = 0; i < count; i++)
            {
                if (data[i].Equals(-1))
                {
                    continue;
                }
                Console.Write(data[i] +" ");
            }
            Console.WriteLine();
        }
    }
}
