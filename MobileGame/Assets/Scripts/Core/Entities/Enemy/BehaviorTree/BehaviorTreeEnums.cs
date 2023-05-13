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
        MuzzleShoot,
        PotionFX
    }

    public enum TreeExternValues
    {
        PlayerTransform,
        GridManager,
        TickManager,
        PlayerHandlerMovement,
        PoolService,
        HypeService,
        PlayerRenderer,
        PlayerController,
        UICanvasService
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

    public enum PopupValue
    {
        MOVE,
        TAUNT,
        ULTIMATE
    }
}