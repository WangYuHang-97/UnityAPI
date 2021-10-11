using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Excel
{
    public class PropertyPropertyManager : IProperty
    {
        public string CNSearch;

        public PropertyProperty[] propertyProperty;

        public PropertyProperty[] PropertyProperty { get => propertyProperty; set => propertyProperty = value; }

        public PropertyProperty GetPropertyProperty(string id)
        {
            var cnsearch = JsonMapper.ToObject<Dictionary<string, int>>(CNSearch);
            var num = cnsearch[id];
            return propertyProperty[num];
        }

        public override void Split(Item[] propertyEnemy)
        {
            base.Split(propertyEnemy);
            PropertyProperty = (PropertyProperty[])propertyEnemy;
        }

        public override string TableName()
        {
            return "property";
        }
    }
}