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
        GridManager,
        TickManager,
        PlayerHandlerMovement,
        PoolService,
        HypeService
    }

    public enum HypeFunctionMode
    {
        DecreasePlayer, 
        DecreaseEnemy, 
        IncreaseEnemy, 
        IncreasePlayer, 
        SetPlayer, 
        SetEnemy,
    }

    public enum NodeState
    {
        SUCCESS,
        FAILURE
    }

    public enum InternValueType
    {
        VECTOR3,
        INT,
        FLOAT,
        NONE,
        CALLBACK,
        BOOL
    }

    public enum InternValuePropertyType
    {
        GET,
        SET,
        GETANDSET,
        REMOVE
    }

    public enum InternValueIntCalculate
    {
        ADD,
        SUBTRACT,
        SET
    }
}