using Leopotam.Ecs;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    sealed class JumpBlockSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BlockJumpDuration> blockFilter = null;
        
        public void Run()
        {
            foreach (var i in blockFilter)
            {
                ref var entity = ref blockFilter.GetEntity(i);
                ref var block = ref blockFilter.Get1(i);
                
                block.Timer -= Time.deltaTime;
                if (block.Timer <= 0)
                {
                    entity.Del<BlockJumpDuration>();
                }
            }
        }
    }
}