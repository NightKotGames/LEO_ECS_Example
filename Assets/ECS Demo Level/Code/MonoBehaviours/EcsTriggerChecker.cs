using UnityEngine;
using Voody.UniLeo;

namespace NTC.Source.Code.Ecs
{
    public class EcsTriggerChecker : MonoBehaviour
    {
        [SerializeField] private string targetTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(targetTag)) return;
            
            WorldHandler.GetWorld().SendMessage(new DebugMessageRequest()
            {
                Message = "Player entered"
            });
        }
    }
}