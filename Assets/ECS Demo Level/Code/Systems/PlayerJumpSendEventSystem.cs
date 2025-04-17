
using Tags;
using Events;
using Components;
using UnityEngine;
using Leopotam.Ecs;

namespace Systems
{
    sealed class PlayerJumpSendEventSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTag, JumpComponent> _playerFilter = null;
        
        public void Run()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            foreach (var i in _playerFilter)
            {
                ref var entity = ref _playerFilter.GetEntity(i);
                entity.Get<JumpEvent>();
            }
        }
    }
}