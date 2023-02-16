using UnityEngine;

[CreateAssetMenu(menuName = "Environment", fileName = "new Environment")]
public class EnvironmentSO : ScriptableObject
{
    [Tooltip("Position of the player at the start of the environment")] public int Index;
    
    [Tooltip("Name in Addressable for Environment")] public string EnvironmentAddressableName;

    [Tooltip("Type of grid in the Environment")] public GridSO GridOfEnvironment;

    [Tooltip("Name in Addressable for MovePoint")] public string RendererMovePointAddressableName;

    // todo: reference boss
}