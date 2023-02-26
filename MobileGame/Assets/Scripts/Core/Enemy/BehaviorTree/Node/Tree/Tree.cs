using System.Collections.Generic;
using BehaviorTree.Actions;
using BehaviorTree.Nodes;
using BehaviorTree.InnerNode;
using Environment.MoveGrid;
using UnityEngine;
using Object = System.Object;

namespace BehaviorTree.Trees
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private InnerNodeSO _rootSO;
        [SerializeField] private NodeValuesInitializer _nodeValuesInitializer;
        
        private Node _root;
        private NodeValuesSharer _nodeValuesSharer = new();

        public void Setup(Transform playerTransform, ITickeableService tickeableService,
            EnvironmentGridManager environmentGridManager)
        {
            _nodeValuesInitializer =
                new NodeValuesInitializer(playerTransform, tickeableService, environmentGridManager);
            _root = Node.CreateNodeSO(_rootSO);
            LoopSetUpChild(_root, _rootSO.Childs);
        }

        void LoopSetUpChild(Node parent, NodeSO[] childsSO)
        {
            foreach (var childSO in childsSO)
            {
                var child = Node.CreateNodeSO(childSO);
                parent.Attach(child);
                if (childSO is InnerNodeSO innerNodeStructSO)
                {
                    if (innerNodeStructSO.Childs.Length != 0)
                    {
                        LoopSetUpChild(child, innerNodeStructSO.Childs);
                    }
                }
                else if (childSO is ActionNodeSO actionNodeSo)
                {
                    SetActionNode(child, actionNodeSo);
                }
            }
        }

        private void SetActionNode(Node child, ActionNodeSO actionNodeSo)
        {
            var actionChild = (ActionNode)child;
            actionChild.SetDataSO(actionNodeSo);
            var dependencyValuesType = actionChild.GetDependencyValues();
            Dictionary<BehaviourTreeEnums.TreeExternValues, Object> dependencyExternValuesObjects =
                new Dictionary<BehaviourTreeEnums.TreeExternValues, Object>();
            Dictionary<BehaviourTreeEnums.TreeEnemyValues, Object> dependencyEnemyValuesObjects =
                new Dictionary<BehaviourTreeEnums.TreeEnemyValues, Object>();
            for (int i = 0; i < dependencyValuesType.enemyValues.Length; i++)
            {
                dependencyEnemyValuesObjects.Add(dependencyValuesType.enemyValues[i],
                    _nodeValuesInitializer.GetEnemyValueObject(dependencyValuesType.enemyValues[i]));
            }

            for (int i = 0; i < dependencyValuesType.externValues.Length; i++)
            {
                dependencyExternValuesObjects.Add(dependencyValuesType.externValues[i],
                    _nodeValuesInitializer.GetExternValueObject(dependencyValuesType.externValues[i]));
            }

            actionChild.Sharer = _nodeValuesSharer;
            actionChild.SetDependencyValues(dependencyExternValuesObjects, dependencyEnemyValuesObjects);
        }
    }
}