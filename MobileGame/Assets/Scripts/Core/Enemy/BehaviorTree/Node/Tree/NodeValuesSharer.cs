using System.Collections.Generic;
using Object = System.Object;

namespace BehaviorTree.Trees
{
    public class NodeValuesSharer
    {
        public Dictionary<BehaviourTreeEnums.TreeInternValues, Object> InternValues;

        public NodeValuesSharer()
        {
            InternValues = new Dictionary<BehaviourTreeEnums.TreeInternValues, Object>();
        }
    }
}