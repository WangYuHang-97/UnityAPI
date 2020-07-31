using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 钢条切割问题___动态规划
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] p = {0,1,5,8,9,10,17,17,20,24,30};//索引长度,值为价格
            //纯迭代模式
            for (int i = 0; i < p.Length; i++)
            {
                Console.WriteLine(UpDown(i,p));
            }
            //动态规划迭代（用空间换时间）
            int[] resultUpDown = new int[p.Length+1];//保存结果
            for (int i = 0; i < p.Length; i++)
            {
                Console.WriteLine(UpDown(i, p, resultUpDown));
            }
            //动态规划(自底向上,性能最好)
            int[] resultBottomUp = new int[p.Length + 1];//保存结果
            for (int i = 0; i < p.Length; i++)
            {
                Console.WriteLine(BottomUp(i, p, resultBottomUp));
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 求得长度为n得最大收益(自顶向下)
        /// </summary>
        /// <param name="n">长度</param>
        /// <param name="p">价格数组</param>
        /// <returns></returns>
        public static int UpDown(int n,int[] p)
        {
            if (n == 0) return 0;
            int tempMaxPrice = 0;
            for (int i = 1; i < n+1; i++)
            {
                int maxPrice = p[i] + UpDown(n - i, p);//从1~n切割一次,剩下的再迭代切割获得每次的最大收益
                if (maxPrice>tempMaxPrice)
                {
                    tempMaxPrice = maxPrice;
                }
            }
            return tempMaxPrice;
        }

        /// <summary>
        /// 动态规划求得长度为n得最大收益(自顶向下,将遍历每中拆分结果)
        /// </summary>
        /// <param name="n">长度</param>
        /// <param name="p">价格数组</param>
        /// <param name="result">各长度最大收益数组</param>
        /// <returns></returns>
        public static int UpDown(int n, int[] p,int[] result)
        {
            if (n == 0) return 0;
            if (result[n] != 0) return result[n];
            int tempMaxPrice = 0;
            for (int i = 1; i < n + 1; i++)
            {
                int maxPrice = p[i] + UpDown(n - i, p);
                if (maxPrice > tempMaxPrice)
                {
                    tempMaxPrice = maxPrice;
                }
            }
            result[n] = tempMaxPrice;
            return tempMaxPrice;
        }

        /// <summary>
        /// 动态规划求得长度为n得最大收益(自底向上,只获取最优解,不需要迭代)
        /// </summary>
        /// <param name="n">长度</param>
        /// <param name="p">价格数组</param>
        /// <param name="result">各长度最大收益数组</param>
        public static int BottomUp(int n, int[] p, int[] result)
        {
            for (int i = 1; i < n + 1; i++)
            {
                int tempMaxPrice = 0;
                for (int j = 1; j <=i; j++)
                {
                    int maxPrice = p[j] + result[i - j];
                    if (maxPrice > tempMaxPrice)
                    {
                        tempMaxPrice = maxPrice;
                    }
                }
                result[i] = tempMaxPrice;
            }
            return result[n];
        }
    }
}
