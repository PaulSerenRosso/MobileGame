using Environment.MoveGrid;
using Service;
using UnityEngine;
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Tree.Tree _tree;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    public void Setup(Transform playerTransform, ITickeableService tickeableService,
        EnvironmentGridManager environmentGridManager, IPoolService poolService)
    {
        _tree.Setup(playerTransform, tickeableService, environmentGridManager, poolService);
    }

    public void RenderContainer()
    {
        
    }
}