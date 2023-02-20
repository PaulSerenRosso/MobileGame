using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "TauntActionSO", fileName = "new TauntAction")]
    public class TauntActionSO : ScriptableObject
    {
        public float endTime;
    }
}