
using System;
using UnityEngine;

namespace Pool
{
    [Serializable]

    public sealed class PoolItem
    {
        [SerializeField] private string _name;

        public int SizePool;
        [Space(20)] 
        public GameObject Prefab;
        
        public string Tag => Prefab.name;

        public PoolItem(GameObject @object) => Prefab = @object;
    }
}