using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using BehaviorTree.Trees;
using Object = System.Object;

namespace BehaviorTree.Nodes.Actions
{
    public abstract class ActionNode : Node
    {
        public NodeValuesSharer Sharer;

        public (BehaviourTreeEnums.TreeEnemyValues[] enemyValues, BehaviourTreeEnums.TreeExternValues[] externValues)
            GetDependencyValues()
        {
            var so = (ActionNodeSO)GetNodeSO();
            return (so.Data.EnemyValues, so.Data.ExternValues);
        }

        public virtual void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, Object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, Object> enemyDependencyValues)
        {
            
        }

        public abstract ActionNodeDataSO GetDataSO();
    }
}