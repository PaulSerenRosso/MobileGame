using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attributes
{ [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class TickServiceFunction : Attribute
    {
        
    }
}