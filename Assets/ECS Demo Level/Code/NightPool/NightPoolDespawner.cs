using UnityEngine;

namespace NTC.Global.Pool
{
    public class NightPoolDespawner : MonoBehaviour
    {
        [SerializeField] private float timeToDespawn = 3f;
        [SerializeField] private bool checkForEvents;

        private bool processed;
        private float timer;

        private void OnDisable()
        {
            processed = false;
        }

        private void Update()
        {
            if (processed) return;
            
            timer += Time.deltaTime;
            
            if (timer < timeToDespawn) return;
            
            timer = 0;
            processed = true;
            NightPool.Despawn(gameObject, checkForEvents);
        }
    }
}