using System.Collections.Generic;
using BehaviorTree.Data;
using BehaviorTree.Trees;
using Object = System.Object;

namespace BehaviorTree.Nodes
{
    public abstract class ActionNode : Node
    {
        public abstract ActionNodeDataSO GetDataSO();
        public abstract void SetDataSO(ActionNodeDataSO so);

        public NodeValuesSharer Sharer;

        public (BehaviourTreeEnums.TreeEnemyValues[] enemyValues, BehaviourTreeEnums.TreeExternValues[] externValues) GetDependencyValues()
        {
            var so = GetDataSO();
            return (so.enemyValues, so.externValues);
        }
        public abstract void SetDependencyValues(Dictionary<BehaviourTreeEnums.TreeExternValues, Object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, Object> enemyDependencyValues);

    }
}