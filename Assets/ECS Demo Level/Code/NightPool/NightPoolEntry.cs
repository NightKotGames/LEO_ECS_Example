using UnityEngine;

namespace NTC.Global.Pool
{
    public class NightPoolEntry : MonoBehaviour
    {
        [SerializeField] private PoolPreset poolPreset;

        private void Awake()
        {
            NightPool.InstallPoolItems(poolPreset);
        }

        private void OnDestroy()
        {
            NightPool.Reset();
        }
    }
}