using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;


[Serializable]
public class ExternValueObject
{
    public Enums.TreeExternValues Type;
    [HideInInspector]
    public Object Obj;
}
