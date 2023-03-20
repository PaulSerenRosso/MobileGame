using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTree
{
    [Serializable]
    public class InternValue : StringWithHashCode
    {
        public BehaviourTreeEnums.InternValueType Type;
        public BehaviourTreeEnums.InternValuePropertyType PropertyType;
        [TextArea] public string Comment;

        public void SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType type,
            BehaviourTreeEnums.InternValuePropertyType propertyType, string commentary)
        {
            Type = type;
            PropertyType = propertyType;
            Comment = commentary;
        }
    }
}