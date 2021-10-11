using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Excel
{
    [System.Serializable]
    public class PropertyProperty : Item
    {
        [SerializeField] private string _name;
        [SerializeField] private string _id;
        [SerializeField] private string _description;

        public string ID
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => DataEncryptManager.StringDecoder(_name);
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}