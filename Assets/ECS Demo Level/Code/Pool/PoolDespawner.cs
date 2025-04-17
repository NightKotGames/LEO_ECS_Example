
using UnityEngine;

namespace Pool
{
    public class PoolDespawner : MonoBehaviour
    {
        [SerializeField] private float _timeToDespawn = 3f;
        [SerializeField] private bool _checkForEvents;

        private float _timer;
        private bool _processed;

        private void OnDisable()
        {
            _processed = false;
        }

        private void Update()
        {
            if (_processed) return;
            
            _timer += Time.deltaTime;
            
            if (_timer < _timeToDespawn) return;
            
            _timer = 0;
            _processed = true;
            PoolInstance.Despawn(gameObject, _checkForEvents);
        }
    }
}