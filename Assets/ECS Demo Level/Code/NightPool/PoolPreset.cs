using System.Collections.Generic;
using UnityEngine;

namespace NTC.Global.Pool
{
    [CreateAssetMenu(menuName = "Source/Pool/PoolPreset", fileName = "PoolPreset", order = 0)]
    public class PoolPreset : ScriptableObject
    {
        [SerializeField] private string poolName;
        public List<PoolItem> poolItems = new List<PoolItem>(256);

        public string GetName()
        {
            return poolName;
        }
    }
}