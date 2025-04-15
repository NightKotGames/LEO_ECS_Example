using System;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    [Serializable]
    public struct MovableComponent
    {
        public CharacterController characterController;
        public Vector3 velocity;
        public float speed;
        public float gravity;
    }
}