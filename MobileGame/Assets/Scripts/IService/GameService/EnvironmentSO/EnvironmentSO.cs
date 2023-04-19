using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Environment", fileName = "new Environment")]
public class EnvironmentSO : ScriptableObject
{
    [Tooltip("Name in Addressable for Environment")] public string EnvironmentAddressableName;
    public Sprite EnvironmentSprite;
}