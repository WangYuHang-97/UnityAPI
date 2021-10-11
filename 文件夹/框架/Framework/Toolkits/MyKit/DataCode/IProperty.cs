using System.Collections;
using System.Collections.Generic;
using LitJson;
using QFramework;
using UnityEngine;

namespace Excel
{
    public class IProperty : ScriptableObject
    {
        public string Search;

        public string Json;

        public int GetNum(string id)
        {
            var search = JsonMapper.ToObject<Dictionary<string, int>>(Search);
            if (search.ContainsKey(id))
            {
                return search[id];
            }
            else if (search.ContainsKey(DataEncryptManager.StringEncoder(id)))
            { 
                return search[DataEncryptManager.StringEncoder(id)];
            }
            else
            {
                Debug.Log(id);
                return -1;
            }
        }

        public virtual string TableName()
        {
            return null;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public virtual void Split(Item[] obj)
        {
            
        }

        public virtual string TableDir()
        {
            return $"E://HePingWuGuan//策划//3_《和平武馆》各系统策划案//和平武馆配置总表.xlsx";
        }
    }
}