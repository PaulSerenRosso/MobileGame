using UnityEngine;

[CreateAssetMenu(menuName = "Grid", fileName = "new Grid")]
public class GridSO : ScriptableObject
{
    [Tooltip("Number of circles on the environment")] public float[] CircleRadius;

    [Tooltip("Number of Checkpoints by circle")] public int MovePoints;
    
    [Tooltip("Position of the player at the start of the environment")] public int Index;

    [Tooltip("Name in Addressable for MovePoint")] public string RendererMovePointAdressableName;

    [SerializeField] float _circleEnvironnementRadius;

    [HideInInspector]
    public float CircleEnvironnementSqRadius;
    
    private void OnValidate()
    {
        CircleEnvironnementSqRadius = _circleEnvironnementRadius * _circleEnvironnementRadius;
    }
  
}