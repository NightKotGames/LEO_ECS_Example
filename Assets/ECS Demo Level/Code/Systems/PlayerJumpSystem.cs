using Leopotam.Ecs;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    sealed class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly 
            EcsFilter<PlayerTag, GroundCheckSphereComponent, JumpComponent, JumpEvent>.
            Exclude<BlockJumpDuration> jumpFilter = null;
        
        public void Run()
        {
            foreach (var i in jumpFilter)
            {
                ref var entity = ref jumpFilter.GetEntity(i);
                ref var groundCheck = ref jumpFilter.Get2(i);
                ref var jumpComponent = ref jumpFilter.Get3(i);
                ref var movable = ref entity.Get<MovableComponent>();
                ref var velocity = ref movable.velocity;
                
                if (!groundCheck.isGrounded) continue;

                velocity.y = Mathf.Sqrt(jumpComponent.force * -2f * movable.gravity);
                entity.Get<BlockJumpDuration>().Timer = 3f;
            }
        }
    }
}