using Leopotam.Ecs;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    sealed class DebugSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DebugMessageRequest> messageFilter = null;
        
        public void Run()
        {
            foreach (var i in messageFilter)
            {
                ref var entity = ref messageFilter.GetEntity(i);
                ref var request = ref messageFilter.Get1(i);
                ref var message = ref request.Message;
                
                Debug.Log(message);
                entity.Del<DebugMessageRequest>();
            }
        }
    }
}