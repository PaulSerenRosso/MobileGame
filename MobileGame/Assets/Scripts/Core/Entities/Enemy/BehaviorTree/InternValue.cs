using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTree
{
    [Serializable]
    public class InternValue : StringWithHashCode
    {
        public BehaviorTreeEnums.InternValueType Type;
        public BehaviorTreeEnums.InternValuePropertyType PropertyType;
        [TextArea] public string Comment;

        public void SetInternValueWithoutKey(BehaviorTreeEnums.InternValueType type,
            BehaviorTreeEnums.InternValuePropertyType propertyType, string commentary)
        {
            Type = type;
            PropertyType = propertyType;
            Comment = commentary;
        }
    }
}