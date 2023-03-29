using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Actions/TauntActionSO", fileName = "new TauntAction")]
    public class TauntActionSO : ScriptableObject
    {
        public float EndTime;
        public float StartTime;
        public float HypeAmount; 
    }
}