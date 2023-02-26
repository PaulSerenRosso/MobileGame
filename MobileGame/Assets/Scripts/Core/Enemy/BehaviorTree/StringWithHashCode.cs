namespace BehaviorTree
{
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