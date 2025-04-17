
using Events;
using Leopotam.Ecs;

namespace Systems
{
    sealed class VolumeEnableSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnableVolumeEvent> _enableFilter = null;
        
        public void Run()
        {
            foreach (var i in _enableFilter)
            {
                ref var entity = ref _enableFilter.GetEntity(i);
                entity.Del<EnableVolumeEvent>();
            }
        }
    }
}