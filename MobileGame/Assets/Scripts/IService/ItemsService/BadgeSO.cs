using UnityEngine;

namespace Service.Items
{
    [CreateAssetMenu(menuName = "BadgeSO", fileName = "new BadgeSO")]
    public class BadgeSO : ScriptableObject
    {
        public Sprite SpriteBadge;
        public string TitleBadge;
        public string DescriptionBadge;
    }
}