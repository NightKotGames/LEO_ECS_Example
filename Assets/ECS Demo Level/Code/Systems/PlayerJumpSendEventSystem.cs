using Leopotam.Ecs;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    sealed class PlayerJumpSendEventSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTag, JumpComponent> playerFilter = null;
        
        public void Run()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            foreach (var i in playerFilter)
            {
                ref var entity = ref playerFilter.GetEntity(i);
                entity.Get<JumpEvent>();
            }
        }
    }
}