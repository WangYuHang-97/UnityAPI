using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Excel
{
    public class ExcelConfig
    {
        /// <summary>
        /// 存放excel表文件夹的的路径，本例xecel表放在了"Assets/Excels/"当中
        /// </summary>

        /// <summary>
        /// 存放Excel转化CS文件的文件夹路径
        /// </summary>
        public static readonly string assetPath = "Assets/Editor Default Resources/Data/NumericalInformation/";
        public static readonly string assetPathSCN = "Assets/Editor Default Resources/Data/NumericalInformation/SCN/";
        public static readonly string assetPathTCN = "Assets/Editor Default Resources/Data/NumericalInformation/TCN/";

        public static readonly string PropertyConst = $"E://HePingWuGuan//策划//2_《和平武馆》配置表//常量表.xlsx";
        public static readonly string PropertyCH2= $"E://HePingWuGuan//策划//3_《和平武馆》各系统策划案//";

        public static readonly string PropertyFight = "和平武馆配置总表.xlsx";
        public static readonly string PropertyName = "随机姓名表.xlsx";
    }
}

