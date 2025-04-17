
using Requests;
using UnityEngine;
using Leopotam.Ecs;

namespace Systems
{
    sealed class JumpBlockSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BlockJumpDuration> _blockFilter = null;
        
        public void Run()
        {
            foreach (var i in _blockFilter)
            {
                ref var entity = ref _blockFilter.GetEntity(i);
                ref var block = ref _blockFilter.Get1(i);
                
                block.Timer -= Time.deltaTime;
                if (block.Timer <= 0)
                {
                    entity.Del<BlockJumpDuration>();
                }
            }
        }
    }
}