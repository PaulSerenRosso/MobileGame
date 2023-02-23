using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum Direction
    {
        Left, Right, Top, Down
    }
    public enum TreeEnemyValues
    {
        Rigidbody, Animator, Transform
    }
    public enum TreeExternValues
    {
        PlayerTransform, EnvironmentGridManager,ITickService 
    }
}
