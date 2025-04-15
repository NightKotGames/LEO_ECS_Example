using Leopotam.Ecs;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    sealed class PlayerMouseLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag> playerFilter = null;
        private readonly EcsFilter<PlayerTag, ModelComponent, MouseLookDirectionComponent> mouseLookFilter = null;

        private Quaternion startTransformRotation;

        public void Init()
        {
            foreach (var i in playerFilter)
            {
                ref var entity = ref playerFilter.GetEntity(i);
                ref var model = ref entity.Get<ModelComponent>();

                startTransformRotation = model.modelTransform.rotation;
            }
        }
        
        public void Run()
        {
            foreach (var i in mouseLookFilter)
            {
                ref var model = ref mouseLookFilter.Get2(i);
                ref var lookComponent = ref mouseLookFilter.Get3(i);

                var axisX = lookComponent.direction.x;
                var axisY = lookComponent.direction.y;
                
                var rotateX = 
                    Quaternion.AngleAxis(axisX, Vector3.up * Time.deltaTime * lookComponent.mouseSensitivity);
                var rotateY = 
                    Quaternion.AngleAxis(axisY, Vector3.right * Time.deltaTime * lookComponent.mouseSensitivity);
                
                model.modelTransform.rotation = startTransformRotation * rotateX;
                lookComponent.cameraTransform.rotation = model.modelTransform.rotation * rotateY;
            }
        }
    }
}