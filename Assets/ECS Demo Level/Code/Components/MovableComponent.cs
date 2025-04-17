
using System;
using UnityEngine;

namespace Components
{
    [Serializable]

    public struct MovableComponent
    {
        public CharacterController CharacterController;
        public Vector3 Velocity;
        public float Speed;
        public float Gravity;
    }
}