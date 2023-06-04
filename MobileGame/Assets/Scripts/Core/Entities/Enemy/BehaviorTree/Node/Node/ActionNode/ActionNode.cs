using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using BehaviorTree.Trees;
using Object = System.Object;

namespace BehaviorTree.Nodes.Actions
{
    public abstract class ActionNode : Node
    {
        public NodeValuesSharer Sharer;

        public (BehaviorTreeEnums.TreeEnemyValues[] enemyValues, BehaviorTreeEnums.TreeExternValues[] externValues)
            GetDependencyValues()
        {
            var so = (ActionNodeSO)GetNodeSO();
            return (so.Data.EnemyValues, so.Data.ExternValues);
        }

        public virtual void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, Object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, Object> enemyDependencyValues)
        {
            
        }

        public override void Evaluate()
        {
            
        }

        public abstract ActionNodeDataSO GetDataSO();
    }
}