using UnityEngine;

[CreateAssetMenu(menuName = "Camera", fileName = "new Camera")]
public class CameraSettingsSO : ScriptableObject
{
    public Vector3 OffsetPosition;
    public float SpeedPosition;
    public float SpeedRotation;
    public Vector3 OffsetRotation;

}
