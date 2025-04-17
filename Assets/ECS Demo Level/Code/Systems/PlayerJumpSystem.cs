
using Tags;
using Events;
using Requests;
using Components;
using UnityEngine;
using Leopotam.Ecs;

namespace Systems
{
    sealed class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly 
            EcsFilter<PlayerTag, GroundCheckSphereComponent, JumpComponent, JumpEvent>.
            Exclude<BlockJumpDuration> _jumpFilter = null;
        
        public void Run()
        {
            foreach (var i in _jumpFilter)
            {
                ref var entity = ref _jumpFilter.GetEntity(i);
                ref var groundCheck = ref _jumpFilter.Get2(i);
                ref var jumpComponent = ref _jumpFilter.Get3(i);
                ref var movable = ref entity.Get<MovableComponent>();
                ref var velocity = ref movable.Velocity;
                
                if (groundCheck.IsGrounded == false) 
                    continue;

                velocity.y = Mathf.Sqrt(jumpComponent.Force * -2f * movable.Gravity);
                entity.Get<BlockJumpDuration>().Timer = 3f;
            }
        }
    }
}