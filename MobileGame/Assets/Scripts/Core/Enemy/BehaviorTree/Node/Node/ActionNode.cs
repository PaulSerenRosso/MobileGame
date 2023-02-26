using System.Collections.Generic;
using BehaviorTree.Actions;
using BehaviorTree.Trees;
using Object = System.Object;

namespace BehaviorTree.Nodes
{
    public abstract class ActionNode : Node
    {
        public NodeValuesSharer Sharer;
        
        public abstract ActionNodeSO GetDataSO();
        
        public abstract void SetDataSO(ActionNodeSO so);

        public (BehaviourTreeEnums.TreeEnemyValues[] enemyValues, BehaviourTreeEnums.TreeExternValues[] externValues)
            GetDependencyValues()
        {
            var so = GetDataSO();
            return (so.EnemyValues, so.ExternValues);
        }

        public abstract void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, Object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, Object> enemyDependencyValues);
    }
}