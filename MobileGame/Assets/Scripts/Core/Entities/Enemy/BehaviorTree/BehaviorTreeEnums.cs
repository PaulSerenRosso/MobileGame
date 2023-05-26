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
        ShieldFX,
        StunFX,
        PrevShoot,
        MuzzleShoot,
        PotionFX,
        MovementFX,
    }

    public enum TreeExternValues
    {
        PlayerTransform,
        GridManager,
        TickManager,
        PlayerMovementHandler,
        PoolService,
        HypeService,
        PlayerRenderer,
        PlayerController,
        UICanvasService,
        PlayerTauntHandler
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
        STRING,
        VECTOR3LIST
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

    public enum PopupValue
    {
        MOVE,
        TAUNT,
        ULTIMATE
    }
}