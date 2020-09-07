using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 每个结点的值都大于或等于其左右孩子结点的值，称为大顶堆；或者每个结点的值都小于等于其左右孩子结点的值，称为小顶堆!
/// </summary>
namespace 堆排序___顺序结构存储
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = { 50, 10, 90, 30, 70, 40, 80, 60, 20 };
            HeapSort(data);
            foreach (int a in data)
            {
                Console.Write(a+" ");
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// 堆排序方法
        /// </summary>
        /// <param name="data">数组数据</param>
        public static void HeapSort(int[] data)
        {
            for (int i = data.Length / 2; i >= 1; i--)//遍历所有非叶节点(有子节点的节点)，把子树变成大顶堆
            {
                HeapAjust(i,data,data.Length);
            }
            for (int i = data.Length; i >1; i--)//分别提取大顶堆的1号位（最大值）,再将剩下的数据成树后进行大顶堆操作，便可得到排序数组
            {
                int newTemp = data[0];
                data[0] = data[i - 1];//将最大值放在依序放在后面
                data[i - 1] = newTemp;//将最小值放在1号位
                HeapAjust(1,data,i-1);//进行大顶堆排序
            }
        }

        /// <summary>
        /// 将树调整为大顶堆
        /// </summary>
        /// <param name="numberToAjust">开始遍历的树编号(索引+1)</param>
        /// <param name="data">数组数据</param>
        /// <param name="maxNumber">大顶堆作用数组长度</param>
        private static void HeapAjust(int numberToAjust,int[]data ,int maxNumber)
        {
            int maxNodeNumber = numberToAjust;//数字最大节点索引
            int tempI = numberToAjust;//当前位置索引
            while (true)
            {
                int leftChildNumber = tempI * 2;//左子节点编号
                int rightChildNumber = leftChildNumber + 1;//右子节点编号
                if (leftChildNumber <= maxNumber && data[leftChildNumber - 1] > data[maxNodeNumber - 1])//小于左子节点
                {
                    maxNodeNumber = leftChildNumber;
                }
                if (rightChildNumber <= maxNumber && data[rightChildNumber - 1] > data[maxNodeNumber - 1])//小于右子节点
                {
                    maxNodeNumber = rightChildNumber;
                }
                if (maxNodeNumber != tempI)//若上述条件即maxNodeNumber发生，交换tempI和maxNodeNumber的数据
                {
                    int temp = data[tempI - 1];
                    data[tempI - 1] = data[maxNodeNumber - 1];
                    data[maxNodeNumber - 1] = temp;
                    tempI = maxNodeNumber;//当前位置更新,若再有子节点大于该节点,while持续更新
                }
                else
                {
                    break;
                }
            }
        }
    }
}
