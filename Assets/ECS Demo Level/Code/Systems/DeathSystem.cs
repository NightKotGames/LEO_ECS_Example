using Leopotam.Ecs;
using NTC.Global.Pool;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    sealed class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, PerformDeathEvent>.Exclude<IsDeathTag> deathFilter = null;
        
        public void Run()
        {
            foreach (var i in deathFilter)
            {
                ref var entity = ref deathFilter.GetEntity(i);
                ref var model = ref deathFilter.Get1(i);
                
                //For default destroy:
                Object.Destroy(model.modelTransform.gameObject);
                entity.Destroy();
                
                //For despawn from pool:
                NightPool.Despawn(model.modelTransform);
                entity.Get<IsDeathTag>();
            }
        }
    }
}