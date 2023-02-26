using System.Collections.Generic;
using Object = System.Object;

namespace BehaviorTree.Trees
{
    public class NodeValuesSharer
    {
        public Dictionary<int, Object> InternValues;

        public NodeValuesSharer()
        {
            InternValues = new Dictionary<int, Object>();
        }
    }
}