using System.Collections.Generic;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Actions;
using BehaviorTree.Nodes.Composite;
using BehaviorTree.Nodes.Decorator;
using BehaviorTree.SO.Composite;
using BehaviorTree.SO.Decorator;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using UnityEngine;
using Object = System.Object;

namespace BehaviorTree.Trees
{
    public class Tree : MonoBehaviour, IUpdatable
    {
        [SerializeField] private NodeSO _rootSO;
        [SerializeField] private NodeValuesInitializer _nodeValuesInitializer;

        private Node _root;
        private NodeValuesSharer _nodeValuesSharer = new();

        public void OnUpdate()
        {
            _root.Evaluate();
        }

        public void Setup(Transform playerTransform, ITickeableService tickeableService,
            EnvironmentGridManager environmentGridManager)
        {
            _nodeValuesInitializer.Setup(playerTransform, tickeableService, environmentGridManager);
            _root = Node.CreateNodeSO(_rootSO);
            switch (_rootSO)
            {
                case CompositeSO compositeSO:
                    LoopSetupChild((CompositeNode)_root, compositeSO.Childs);
                    break;
                case DecoratorSO decoratorSO:
                    LoopSetupChild((DecoratorNode)_root, decoratorSO.Child);
                    break;
            }
            UpdateManager.Register(this);
        }

        private void LoopSetupChild(CompositeNode parent, NodeSO[] childsSO)
        {
            foreach (var childSO in childsSO)
            {
                var child = CreateChild(childSO);
                parent.Attach(child);
                SetupChild(childSO, child);
            }
        }

        private void LoopSetupChild(DecoratorNode parent, NodeSO childSO)
        {
            var child = CreateChild(childSO);
            parent.Attach(child);
            SetupChild(childSO, child);
        }

        private Node CreateChild(NodeSO childSO)
        {
            return Node.CreateNodeSO(childSO);
        }

        private void SetupChild(NodeSO childSO, Node child)
        {
            child.SetNodeSO(childSO);
            if (childSO is CompositeSO compositeSO)
            {
                if (compositeSO.Childs.Length != 0)
                {
                    LoopSetupChild((CompositeNode)child, compositeSO.Childs);
                }
            }
            else if (childSO is DecoratorSO decoratorSO)
            {
                LoopSetupChild((DecoratorNode)child, decoratorSO.Child);
            }
            else if (childSO is ActionNodeSO actionNodeSO)
            {
                SetActionNode(child, actionNodeSO);
            }
        }

        private void SetActionNode(Node child, ActionNodeSO actionNodeSo)
        {
            var actionChild = (ActionNode)child;
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