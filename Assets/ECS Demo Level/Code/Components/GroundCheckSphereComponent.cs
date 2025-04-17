
using System;
using UnityEngine;

namespace Components
{
    [Serializable]
 
    public struct GroundCheckSphereComponent
    {
        public LayerMask GroundMask;
        public Transform GroundCheckSphere;
        public float GroundDistance;
        public bool IsGrounded;
    }
}