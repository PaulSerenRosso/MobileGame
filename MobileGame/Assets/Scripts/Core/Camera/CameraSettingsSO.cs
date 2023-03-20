using UnityEngine;

[CreateAssetMenu(menuName = "Camera", fileName = "new Camera")]
public class CameraSettingsSO : ScriptableObject
{
    public Vector3 Offset;
    public float SpeedPosition;
    public float SpeedRotation;

}
