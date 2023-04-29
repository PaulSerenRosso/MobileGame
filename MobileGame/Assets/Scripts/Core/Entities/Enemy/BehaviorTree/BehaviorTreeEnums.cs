public class BehaviorTreeEnums
{
    public enum TreeEnemyValues
    {
        Rigidbody,
        Animator,
        Transform,
        MeshRenderer,
        EnemyManager,
        TauntFX,
        StunFX,
        PrevShoot,
        MuzzleShoot
    }

    public enum TreeExternValues
    {
        PlayerTransform,
        GridManager,
        TickManager,
        PlayerHandlerMovement,
        PoolService,
        HypeService,
        PlayerRenderer
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
        BOOL,
        STRING
    }

    public enum InternValuePropertyType
    {
        GET,
        SET,
        GETANDSET,
        REMOVE
    }

    public enum InternValueCalculate
    {
        ADD,
        SUBTRACT,
        SET
    }
}