
using System;
using MonoBehaviors;

namespace Requests
{
    [Serializable]
 
    public struct InitializeEntityRequest
    {
        public EntityReference EntityReference;
    }
}