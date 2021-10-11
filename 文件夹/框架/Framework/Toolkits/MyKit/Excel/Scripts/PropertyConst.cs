using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Excel
{
    [System.Serializable]
    public class PropertyConst : Item
    {
        [SerializeField] private string _constKey;
        [SerializeField] private string _constValue;

        public string ConstKey
        {
            get => DataEncryptManager.StringDecoder(_constKey);
            set => _constKey = value;
        }

        public string ConstValue
        {
            get => DataEncryptManager.StringDecoder(_constValue);
            set => _constValue = value;
        }
    }
}