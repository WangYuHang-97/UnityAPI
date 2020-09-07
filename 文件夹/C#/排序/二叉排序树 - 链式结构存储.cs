using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 若它的左子树不为空，则左子树上所有的结点的值均小于根结构的值；若它的右子树不为空，则右字数上所有结点的值均大于它的根结点的值；
/// </summary>
namespace 二叉排序树___链式结构存储
{
    class Program
    {
        static void Main(string[] args)
        {
            BSTree tree = new BSTree();
            int[] data = { 32, 45, 71, 34, 61, 35, 89, 52, 87, 12, 45 };
            for (int i = 0; i < data.Length; i++)
            {
                tree.Add(data[i]);
            }
            tree.MiddleTravers();
            Console.WriteLine();
            Console.WriteLine(tree.Find(89));
            Console.WriteLine(tree.Find(100));
            tree.Delete(34);
            tree.MiddleTravers();
            Console.WriteLine();
            Console.ReadKey();
        }
    }

    class BSTree
    {
        private BSNode root = null;

        /// <summary>
        /// 添加数据(左子节点小于该节点,右子节点大于该节点)
        /// </summary>
        /// <param name="item">数据值</param>
        public void Add(int item)
        {
            BSNode newNode = new BSNode(item);
            if (root == null) root = newNode;
            else
            {
                BSNode temp = root;
                while (true)
                {
                    if (item >= temp.Data)//放在右子节点
                    {
                        if (temp.RightChild == null)
                        {
                            temp.RightChild = newNode;
                            newNode.Parent = temp;
                            break;
                        }
                        else
                        {
                            temp = temp.RightChild;
                        }
                    }
                    else//放在左子节点
                    {
                        if (temp.LeftChild == null)
                        {
                            temp.LeftChild = newNode;
                            newNode.Parent = temp;
                            break;
                        }
                        else
                        {
                            temp = temp.LeftChild;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 中序遍历（先遍历输出左结点，再输出当前结点的数据，再遍历输出右结点）
        /// </summary>
        public void MiddleTravers()
        {
            MiddleTravers(root);
        }

        /// <summary>
        /// 中序遍历封装方法（先遍历输出左结点，再输出当前结点的数据，再遍历输出右结点）
        /// </summary>
        /// <param name="index">索引(编号-1)</param>
        private void MiddleTravers(BSNode node)
        {
            if (node == null) return;
            MiddleTravers(node.LeftChild);
            Console.Write(node.Data + " ");
            MiddleTravers(node.RightChild);
        }

        /// <summary>
        /// 查找方法1
        /// </summary>
        /// <param name="item">想要查找对象的数值</param>
        /// <returns></returns>
        public bool Find(int item)
        {
            //return Find(item, root);
            BSNode temp = root;
            while (true)
            {
                if (temp == null) return false;
                if (temp.Data == item) return true;
                if (item > temp.Data) temp = temp.RightChild;
                else temp = temp.LeftChild;
            }
        }

        /// <summary>
        /// 查找方法2
        /// </summary>
        /// <param name="item">想要查找对象的数值</param>
        /// <param name="node">开始节点</param>
        /// <returns></returns>
        private bool Find(int item, BSNode node)
        {
            if (node == null) return false;
            if (node.Data == item) return true;
            else
            {
                if (item > node.Data) return Find(item, node.RightChild);
                else return Find(item, node.LeftChild);
            }
        }

        public bool Delete(int item)
        {
            BSNode temp = root;
            while (true)
            {
                if (temp == null) return false;
                if (temp.Data == item)
                {
                    Delete(temp);
                    return true;
                }
                if (item > temp.Data) temp = temp.RightChild;
                else temp = temp.LeftChild;
            }
        }

        private void Delete(BSNode node)
        {
            if (node.LeftChild == null && node.RightChild == null)//叶子节点（不包含子节点）
            {
                if (node.Parent == null) root = null;
                if (node.Parent.LeftChild == node) node.Parent.LeftChild = null;
                else node.Parent.RightChild = null;
                return;
            }
            else if (node.LeftChild == null)//有右字节点
            {
                node.Data = node.RightChild.Data;
                node.RightChild = null;
                return;
            }
            else if (node.RightChild == null)//有左子节点
            {
                node.Data = node.LeftChild.Data;
                node.LeftChild = null;
                return;
            }
            else//左右都有子节点（从右节点找到最小节点）
            {
                BSNode temp = node.RightChild;
                while (true)//获得最小节点
                {
                    if (temp.LeftChild!=null) temp = temp.LeftChild;
                    else break;
                }
                node.Data = temp.Data;
                Delete(temp);//可能还会有右子节点
            }
        }
    }
}

class BSNode
    {
        private BSNode leftChild = null;
        private BSNode rightChild = null;
        private BSNode parent = null;
        private int data;

        public int Data { get => data; set => data = value; }
        public BSNode LeftChild { get => leftChild; set => leftChild = value; }
        public BSNode RightChild { get => rightChild; set => rightChild = value; }
        public BSNode Parent { get => parent; set => parent = value; }

        public BSNode(int item)
        {
            Data = item;
        }
    }


