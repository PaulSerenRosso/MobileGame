public class BehaviorTreeEnums
{
    public enum TreeEnemyValues
    {
        Rigidbody,
        Animator,
        Transform,
        MeshRenderer, 
        EnemyManager
    }

    public enum TreeExternValues
    {
        PlayerTransform,
        EnvironmentGridManager,
        TickManager,
        PlayerHandlerMovement,
        PoolService,
        PlayerHealth
    }

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public enum InternValueType
    {
        VECTOR3, INT, FLOAT, NONE
    }

    public enum InternValuePropertyType
    {
        GET, SET, REMOVE
    }
}