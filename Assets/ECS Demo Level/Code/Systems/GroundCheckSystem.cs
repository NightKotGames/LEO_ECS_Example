
using Components;
using UnityEngine;
using Leopotam.Ecs;

namespace Systems
{
    sealed class GroundCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GroundCheckSphereComponent> _groundFilter = null;
        
        public void Run()
        {
            foreach (var i in _groundFilter)
            {
                ref var groundCheck = ref _groundFilter.Get1(i);

                groundCheck.IsGrounded =
                    Physics.CheckSphere(groundCheck.GroundCheckSphere.position, groundCheck.GroundDistance, 
                        groundCheck.GroundMask);
            }
        }
    }
}