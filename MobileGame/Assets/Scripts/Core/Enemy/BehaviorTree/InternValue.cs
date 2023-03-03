using System;

using UnityEngine;

namespace BehaviorTree
{
  
[Serializable]
public class InternValue : StringWithHashCode
{
  public BehaviourTreeEnums.InternValueType Type;
  public BehaviourTreeEnums.InternValuePropertyType PropertyType;
  [TextArea] public string Commentary;

  public void SetInternValueWithoutKey(BehaviourTreeEnums.InternValueType type, BehaviourTreeEnums.InternValuePropertyType propertyType, string commentary)
  {
    Type = type;
    PropertyType = propertyType;
    Commentary = commentary;
  }
}
}
