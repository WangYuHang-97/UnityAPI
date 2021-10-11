using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Excel
{
    public class PropertyConstManager : IProperty
    {
        public PropertyConst[] propertyConst;

        public PropertyConst[] PropertyConst { get => propertyConst; set => propertyConst = value; }

        public override string ToString()
        {
            return "PropertyConstManager";
        }

        public override void Split(Item[] obj)
        {
            base.Split(obj);
            PropertyConst = (PropertyConst[]) obj;
        }

        public override string TableDir()
        {
            return "E://HePingWuGuan//策划//2_《和平武馆》配置表//常量表.xlsx";
        }
    }
}