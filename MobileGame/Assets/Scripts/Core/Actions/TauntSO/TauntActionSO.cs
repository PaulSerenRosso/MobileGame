using UnityEngine;

namespace Action
{
    [CreateAssetMenu(menuName = "Actions/TauntActionSO", fileName = "new TauntAction")]
    public class TauntActionSO : ScriptableObject
    {
        public float EndTime;
    }
}