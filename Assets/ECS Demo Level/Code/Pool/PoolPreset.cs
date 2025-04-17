
using UnityEngine;
using System.Collections.Generic;

namespace Pool
{
    [CreateAssetMenu(menuName = "Source/Pool/PoolPreset", fileName = "PoolPreset", order = 0)]

    public class PoolPreset : ScriptableObject
    {
        [SerializeField] private string _poolName;
        public List<PoolItem> PoolItems = new List<PoolItem>(256);

        public string GetName() => _poolName;
    }
}