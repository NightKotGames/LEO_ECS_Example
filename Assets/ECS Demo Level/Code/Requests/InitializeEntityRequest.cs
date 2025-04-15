using System;

namespace NTC.Source.Code.Ecs
{
    [Serializable]
    public struct InitializeEntityRequest
    {
        public EntityReference entityReference;
    }
}