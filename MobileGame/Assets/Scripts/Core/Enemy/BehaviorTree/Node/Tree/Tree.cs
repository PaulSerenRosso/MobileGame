using System;
using System.Collections.Generic;
using BehaviorTree.Nodes;
using BehaviorTree.Struct;
using Environnement.MoveGrid;
using UnityEngine;
using Object = System.Object;

namespace BehaviorTree.Trees
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private InnerNodeStructSO _rootSO;

        private Node _root;

        [SerializeField] private NodeValuesInitializer _nodeValuesInitializer;
        private NodeValuesSharer _nodeValuesSharer = new NodeValuesSharer();
        public void Setup(Transform playerTransform, ITickeableService tickeableService, EnvironmentGridManager environmentGridManager)
        {
            _nodeValuesInitializer = new NodeValuesInitializer(playerTransform, tickeableService, environmentGridManager);
            _root = Node.CreateNodeSO(_rootSO);
            LoopSetUpChild(_root, _rootSO.Childs);
        }

        void LoopSetUpChild(Node parent, StructNodeSO[] childsSO)
        {
            foreach (var childSO in childsSO)
            {
                var child = Node.CreateNodeSO(childSO);
                parent.Attach(child);
                if (childSO is InnerNodeStructSO innerNodeStructSO)
                {
                    if (innerNodeStructSO.Childs.Length != 0)
                    {
                        LoopSetUpChild(child, innerNodeStructSO.Childs);
                    }
                }
                else if (childSO is ActionNodeStructSO actionNodeStructSo)
                {
                    SetActionNode(child, actionNodeStructSo);
                }
            
            }
        }
        private void SetActionNode(Node child, ActionNodeStructSO actionNodeStructSo)
        {
            var actionChild = (ActionNode)child;
            actionChild.SetDataSO(actionNodeStructSo.data);
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