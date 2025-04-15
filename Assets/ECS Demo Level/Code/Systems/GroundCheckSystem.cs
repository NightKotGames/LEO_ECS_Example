using Leopotam.Ecs;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    sealed class GroundCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GroundCheckSphereComponent> groundFilter = null;
        
        public void Run()
        {
            foreach (var i in groundFilter)
            {
                ref var groundCheck = ref groundFilter.Get1(i);

                groundCheck.isGrounded =
                    Physics.CheckSphere(groundCheck.groundCheckSphere.position, groundCheck.groundDistance, 
                        groundCheck.groundMask);
            }
        }
    }
}