
using Tags;
using Components;
using UnityEngine;
using Leopotam.Ecs;

namespace Systems
{
    sealed class PlayerMouseLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag> _playerFilter = null;
        private readonly EcsFilter<PlayerTag, ModelComponent, MouseLookDirectionComponent> _mouseLookFilter = null;

        private Quaternion _startTransformRotation;

        public void Init()
        {
            foreach (var i in _playerFilter)
            {
                ref var entity = ref _playerFilter.GetEntity(i);
                ref var model = ref entity.Get<ModelComponent>();

                _startTransformRotation = model.ModelTransform.rotation;
            }
        }
        
        public void Run()
        {
            foreach (var i in _mouseLookFilter)
            {
                ref var model = ref _mouseLookFilter.Get2(i);
                ref var lookComponent = ref _mouseLookFilter.Get3(i);

                var axisX = lookComponent.Direction.x;
                var axisY = lookComponent.Direction.y;
                
                var rotateX = 
                    Quaternion.AngleAxis(axisX, Vector3.up * Time.deltaTime * lookComponent.MouseSensitivity);
                var rotateY = 
                    Quaternion.AngleAxis(axisY, Vector3.right * Time.deltaTime * lookComponent.MouseSensitivity);
                
                model.ModelTransform.rotation = _startTransformRotation * rotateX;
                lookComponent.CameraTransform.rotation = model.ModelTransform.rotation * rotateY;
            }
        }
    }
}