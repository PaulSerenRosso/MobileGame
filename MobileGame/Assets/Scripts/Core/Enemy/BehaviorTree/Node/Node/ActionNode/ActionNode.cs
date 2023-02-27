using System.Collections.Generic;
using BehaviorTree.ActionsSO;
using BehaviorTree.Trees;
using Object = System.Object;

namespace BehaviorTree.Nodes
{
    public abstract class ActionNode : Node
    {
        public NodeValuesSharer Sharer;
        


        public (BehaviourTreeEnums.TreeEnemyValues[] enemyValues, BehaviourTreeEnums.TreeExternValues[] externValues)
            GetDependencyValues()
        {
            var so = (ActionNodeSO) GetNodeSO();
            return (so.Data.EnemyValues, so.Data.ExternValues);
        }

        public abstract void SetDependencyValues(
            Dictionary<BehaviourTreeEnums.TreeExternValues, Object> externDependencyValues,
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, Object> enemyDependencyValues);

        public abstract ActionNodeDataSO GetDataSO();
        public abstract void SetHashCodeKeyOfInternValues(int[] hashCodeKey);
    }
}