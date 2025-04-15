using Leopotam.Ecs;

namespace NTC.Source.Code.Ecs
{
    sealed class EntityInitializeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeEntityRequest> initFilter = null;
        
        public void Run()
        {
            foreach (var i in initFilter)
            {
                ref var entity = ref initFilter.GetEntity(i);
                ref var request = ref initFilter.Get1(i);
                request.entityReference.Entity = entity;
            }
        }
    }
}