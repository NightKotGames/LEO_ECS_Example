using System;
using UnityEngine;

namespace NTC.Source.Code.Ecs
{
    [Serializable]
    public struct MouseLookDirectionComponent
    {
        public Transform cameraTransform;
        [HideInInspector] public Vector2 direction;
        [Range(0, 2)] public float mouseSensitivity;
    }
}