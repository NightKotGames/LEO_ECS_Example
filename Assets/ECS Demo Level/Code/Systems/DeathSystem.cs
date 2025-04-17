
using Pool;
using Tags;
using Events;
using Components;
using UnityEngine;
using Leopotam.Ecs;

namespace Systems
{
    sealed class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, PerformDeathEvent>.Exclude<IsDeathTag> _deathFilter = null;
        
        public void Run()
        {
            foreach (var i in _deathFilter)
            {
                ref var entity = ref _deathFilter.GetEntity(i);
                ref var model = ref _deathFilter.Get1(i);
                
                Object.Destroy(model.ModelTransform.gameObject);
                entity.Destroy();

                PoolInstance.Despawn(model.ModelTransform);
                entity.Get<IsDeathTag>();
            }
        }
    }
}