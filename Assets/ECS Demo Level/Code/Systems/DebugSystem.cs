
using Requests;
using UnityEngine;
using Leopotam.Ecs;

namespace Systems
{
    sealed class DebugSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DebugMessageRequest> _messageFilter = null;
        
        public void Run()
        {
            foreach (var i in _messageFilter)
            {
                ref var entity = ref _messageFilter.GetEntity(i);
                ref var request = ref _messageFilter.Get1(i);
                ref var message = ref request.Message;
                
                Debug.Log(message);
                entity.Del<DebugMessageRequest>();
            }
        }
    }
}