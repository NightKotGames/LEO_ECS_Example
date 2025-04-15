using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace NTC.Source.Code.Ecs
{
    public sealed class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld world;
        private EcsSystems systems;

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            
            systems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void AddInjections()
        {
            
        }
        
        private void AddSystems()
        {
            systems.
                Add(new EntityInitializeSystem()).
                Add(new JumpBlockSystem()).
                Add(new CursorLockSystem()).
                Add(new PlayerJumpSendEventSystem()).
                Add(new GroundCheckSystem()).
                Add(new PlayerMovableInputSystem()).
                Add(new PlayerMouseInputSystem()).
                Add(new PlayerMouseLookSystem()).
                Add(new PlayerJumpSystem()).
                Add(new MovementSystem()).
                Add(new DebugSystem()).
                Add(new VolumeEnableSystem())
                ;
        }

        private void AddOneFrames()
        {
            systems.
                OneFrame<JumpEvent>().
                OneFrame<InitializeEntityRequest>()
                ;
        }

        private void OnDestroy()
        {
            if (systems == null) return;
            
            systems.Destroy();
            systems = null;
            world.Destroy();
            world = null;
        }
    }
}