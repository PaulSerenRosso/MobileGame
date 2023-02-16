using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMovementSO", fileName = "new PlayerMovement")]
public class PlayerMovementSO : ScriptableObject
{
    public float MaxTime;
    public AnimationCurve CurvePosition;
}