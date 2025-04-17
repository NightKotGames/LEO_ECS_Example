
using Leopotam.Ecs;

namespace Extenstions
{
    public static class WorldExt
    {
        public static void SendMessage<T>(this EcsWorld world, in T message) where T : struct
        {
            world.NewEntity().Get<T>() = message;
        }
    }
}