using System;
using UnityEngine;

namespace NTC.Global.Pool
{
    [Serializable]
    public sealed class PoolItem
    {
        [SerializeField] private string name;

        [Space] public GameObject prefab;
                public int size;
        
        public string Tag => prefab.name;

        public PoolItem(GameObject go)
        {
            prefab = go;
        }
    }
}