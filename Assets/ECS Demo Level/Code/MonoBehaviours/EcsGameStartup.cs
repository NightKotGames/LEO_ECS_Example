
using Events;
using Systems;
using Requests;
using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace MonoBehaviors 
{
    public sealed class EcsGameStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
            _systems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();
        }

        private void Update() => _systems.Run();

        private void AddInjections() { }
        
        private void AddSystems()
        {
            _systems.
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
            _systems.
                OneFrame<JumpEvent>().
                OneFrame<InitializeEntityRequest>()
                ;
        }

        private void OnDestroy()
        {
            if (_systems == null) return;
            
            _systems.Destroy();
            _systems = null;
            _world.Destroy();
            _world = null;
        }
    }
}