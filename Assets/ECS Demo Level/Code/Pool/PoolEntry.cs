
using UnityEngine;

namespace Pool
{
    public class PoolEntry : MonoBehaviour
    {
        [SerializeField] private PoolPreset _poolPreset;

        private void Awake() => PoolInstance.InstallPoolItems(_poolPreset);

        private void OnDestroy() => PoolInstance.Reset();
    }
}