
using Requests;
using Extenstions;
using UnityEngine;
using Voody.UniLeo;

namespace MonoBehaviors
{
    public class EcsTriggerChecker : MonoBehaviour
    {
        [SerializeField] private readonly string _targetTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_targetTag) == false) 
                return;
            
            WorldHandler.GetWorld().SendMessage(new DebugMessageRequest()
            {
                Message = "Player entered"
            });
        }
    }
}