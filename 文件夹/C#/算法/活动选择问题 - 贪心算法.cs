using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 活动选择问题___贪心算法
{
    class Program
    {
        static void Main(string[] args)
        {
            //动态规划
            int[] s1 = {0, 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12, 24}; //活动开始时间
            int[] f1 = {0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 24}; //活动结束时间
            List<int>[,] result = new List<int>[13, 13];
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    result[i, j] = new List<int>();
                }
            }

            result = BottomUp(s1, f1, result);
            foreach (var temp in result[0, 12])
            {
                Console.WriteLine(temp);
            }

            //贪心算法
            int[] s2 = {0, 1, 3, 0, 5, 3, 5, 6, 8, 8, 2, 12}; //活动开始时间
            int[] f2 = {0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14}; //活动结束时间
            List<int> listRecursion = ActivitySelectionRecursion(1, 11, 0, 24, s2, f2);
            foreach (var temp in listRecursion)
            {
               Console .WriteLine(temp);
            }
            List<int> listIteration = ActivitySelectionRecursion(1, 11, 0, 24, s2, f2);
            foreach (var temp in listIteration)
            {
                Console.WriteLine(temp);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 动态规划（自底而上）
        /// </summary>
        /// <param name="s">活动开始时间数组</param>
        /// <param name="f">活动结束时间数组</param>
        /// <param name="result">结果数组</param>
        static List<int>[,] BottomUp(int[] s, int[] f, List<int>[,] result)
        {
            for (int j = 0; j < 13; j++)
            {
                for (int i = 0; i < j - 1; i++)
                {
                    List<int> sij = new List<int>(); //i结束后 j开始前活动集合
                    for (int number = 1; number < s.Length - 1; number++) //number活动编号
                    {
                        if (s[number] >= f[i] && f[number] <= s[j])
                        {
                            sij.Add(number);
                        }
                    }

                    if (sij.Count > 0)
                    {
                        int maxCount = 0;
                        List<int> tempList = new List<int>();
                        foreach (int number in sij)
                        {
                            int count = result[i, number].Count + result[number, j].Count + 1;
                            if (maxCount < count)
                            {
                                maxCount = count;
                                tempList = result[i, number].Union<int>(result[number, j]).ToList();
                                tempList.Add(number);
                            }
                        }

                        result[i, j] = tempList;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 递归寻找
        /// </summary>
        /// <param name="startActivityNumber">开始活动索引</param>
        /// <param name="endActivityNumber">结束活动索引</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">终止时间</param>
        /// <param name="s">活动开始时间数组</param>
        /// <param name="f">活动结束时间数组</param>
        /// <returns></returns>
        static List<int> ActivitySelectionRecursion(int startActivityNumber, int endActivityNumber, int startTime, int endTime,
            int[] s, int[] f)
        {
            if (startActivityNumber>endActivityNumber||startTime>=endTime)
            {
                return new List<int>();
            }
            int tempNumber = 0;
            for (int number = startActivityNumber; number <= endActivityNumber; number++)
            {
                if (s[number] >= startTime && f[number] <= endTime)
                {
                    tempNumber = number;
                    break;
                }
            }
            List<int> list = ActivitySelectionRecursion(tempNumber + 1, endActivityNumber, f[tempNumber], endTime, s, f);
            list.Add(tempNumber);
            return list;
        }

        /// <summary>
        /// 迭代寻找
        /// </summary>
        /// <param name="startActivityNumber">开始活动索引</param>
        /// <param name="endActivityNumber">结束活动索引</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">终止时间</param>
        /// <param name="s">活动开始时间数组</param>
        /// <param name="f">活动结束时间数组</param>
        /// <returns></returns>
        static List<int> ActivitySelectionIteration(int startActivityNumber, int endActivityNumber, int startTime, int endTime,
            int[] s, int[] f)
        {
            List<int> list = new List<int>();
            for (int number = startActivityNumber; number <= endActivityNumber; number++)
            {
                if (s[number] >= startTime && f[number] <= endTime)
                {
                    list.Add(number);
                    startTime = f[number];
                }
            }
            return list;
        }
    }
}
