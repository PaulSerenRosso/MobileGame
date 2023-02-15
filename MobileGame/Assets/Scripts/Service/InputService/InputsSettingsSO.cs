using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Service.Inputs
{
    [CreateAssetMenu(menuName = "Inputs/InputsSettings", fileName = "new InputsSettings")]
    public class InputsSettingsSO : ScriptableObject
    {
        public string[] AllSwipeSOAdressableName;
    }
}