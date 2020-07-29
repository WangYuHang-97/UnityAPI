using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 最大子数组问题___分治法
{
    class Program
    {
        struct  SubArray
        {
            public int startIndex;
            public int endIndex;
            public int total;
        }
        static void Main(string[] args)
        {
            int[] priceArray = {100, 113, 110, 85, 105, 102, 86, 63, 81, 101, 94, 106, 101, 79, 94, 90, 97};
            int[] pf = new int[priceArray.Length - 1];
            for (int i = 1; i < priceArray.Length; i++)
            {
                pf[i - 1] = priceArray[i] - priceArray[i - 1];
            }
            SubArray subArray = GetMaxArray(0, pf.Length - 1, pf);
            Console.WriteLine("在"+subArray.startIndex+"买入,"+(subArray.endIndex+1)+"卖出");
            Console.ReadKey();
        }

        static SubArray GetMaxArray(int low,int high,int[] array)
        {
            if (low == high)
            {
                return new SubArray
                {
                    endIndex = low,
                    startIndex = low,
                    total = array[low]
                };
            }
            int mid = (low + high) / 2;//中位数
            SubArray sunArray1 = GetMaxArray(low, mid, array);//情况1,startIndex和endIndex都在中位数左边
            SubArray sunArray2 = GetMaxArray(mid + 1, high, array);//情况2,startIndex和endIndex都在中位数右边

            ////情况3,startIndex和endIndex分别在中位数左边和右边,则startIndex一定是low和mid间最大子数组,endIndex一定是mid和high间最大子数组
            int total1 = array[mid];
            int startIndex = mid;
            int totalTemp = 0;
            for (int i = mid; i >=low; i--)
            {
                totalTemp += array[i];
                if (totalTemp>total1)
                {
                    total1 = totalTemp;
                    startIndex = i;
                }
            }

            int total2 = array[mid + 1];
            int endIndex = mid + 1;
            totalTemp = 0;
            for (int j = mid+1; j <=high; j++)
            {
                totalTemp += array[j];
                if (totalTemp > total2)
                {
                    total2 = totalTemp;
                    endIndex= j;
                }
            }
            SubArray subArray3 = new SubArray
            {
                endIndex = endIndex,
                startIndex = startIndex,
                total = total1+total2
            };
            if (sunArray1.total>= sunArray2.total&&sunArray1.total>=subArray3.total)
            {
                return sunArray1;
            }
            else if(sunArray2.total >= sunArray1.total && sunArray2.total >= subArray3.total)
            {
                return sunArray2;
            }
            else
            {
                return subArray3;
            }

        }
    }
}
