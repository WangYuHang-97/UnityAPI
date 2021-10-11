using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Excel
{
    public class Item 
    {
        public static int GetDuring(string timeStr)
        {
            int time = 0;
            if (timeStr.Contains("h"))
            {
                time += Int32.Parse(timeStr[timeStr.IndexOf("h") - 1].ToString()) * 60 * 60;
            }
            if (timeStr.Contains("m"))
            {
                time += Int32.Parse(timeStr[timeStr.IndexOf("m") - 1].ToString()) * 60;
            }
            if (timeStr.Contains("s"))
            {
                time += Int32.Parse(timeStr[timeStr.IndexOf("s") - 1].ToString());
            }
            return time / 400;
        }
    }
}
