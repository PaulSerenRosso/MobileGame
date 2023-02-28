using System;

namespace BehaviorTree
{
    [Serializable]
    public class StringWithHashCode
    {
        public string Key;
        public int HashCode;

        public void UpdateKeyHashCode()
        {
            HashCode = Key.GetHashCode();
        }
    }
}