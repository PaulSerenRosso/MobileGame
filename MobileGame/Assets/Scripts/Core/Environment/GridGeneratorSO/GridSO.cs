using UnityEngine;

[CreateAssetMenu(menuName = "Grid", fileName = "new Grid")]
public class GridSO : ScriptableObject
{
    [Tooltip("Number of circles on the environment")] public float[] CircleRadius;

    [Tooltip("Number of Checkpoints by circle")] public int MovePoints;
}