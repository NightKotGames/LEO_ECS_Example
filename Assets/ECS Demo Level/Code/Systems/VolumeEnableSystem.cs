using Leopotam.Ecs;

namespace NTC.Source.Code.Ecs
{
    sealed class VolumeEnableSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnableVolumeEvent> enableFilter = null;
        
        public void Run()
        {
            foreach (var i in enableFilter)
            {
                ref var entity = ref enableFilter.GetEntity(i);
                entity.Del<EnableVolumeEvent>();
            }
        }
    }
}