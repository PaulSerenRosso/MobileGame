using Environment.MoveGrid;
using UnityEngine;
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Tree.Tree _tree;

    private void Start()
    {
        transform.position = new Vector3(0, 2, 0);
    }

    public void SetUp(Transform playerTransform, ITickeableService tickeableService,
        EnvironmentGridManager environmentGridManager)
    {
        _tree.Setup(playerTransform, tickeableService, environmentGridManager);
    }
}