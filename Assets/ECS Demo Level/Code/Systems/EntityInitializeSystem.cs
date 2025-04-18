﻿
using Requests;
using Leopotam.Ecs;

namespace Systems
{
    sealed class EntityInitializeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeEntityRequest> _initFilter = null;
        
        public void Run()
        {
            foreach (var i in _initFilter)
            {
                ref var entity = ref _initFilter.GetEntity(i);
                ref var request = ref _initFilter.Get1(i);
                request.EntityReference.Entity = entity;
            }
        }
    }
}