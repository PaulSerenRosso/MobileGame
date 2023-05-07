using System;
using System.Collections.Generic;
using BehaviorTree.Nodes;
using BehaviorTree.Nodes.Actions;
using BehaviorTree.Nodes.Composite;
using BehaviorTree.Nodes.Decorator;
using BehaviorTree.SO.Composite;
using BehaviorTree.SO.Decorator;
using Cysharp.Threading.Tasks;
using Environment.MoveGrid;
using Player;
using Service;
using Service.Hype;
using UnityEngine;
using Object = System.Object;

namespace BehaviorTree.Trees
{
    public class Tree : MonoBehaviour
    {
        public List<Node> ResetNodeList = new();

        [SerializeField] private NodeSO _rootSO;
        [SerializeField] private NodeValuesInitializer _nodeValuesInitializer;

        private Node _root;
        private NodeValuesSharer _nodeValuesSharer = new();

        public void Setup(Transform playerTransform, ITickeableService tickeableService,
            GridManager gridManager, IPoolService poolService, IHypeService hypeService)
        {
            _nodeValuesInitializer.Setup(playerTransform, tickeableService, gridManager, poolService, hypeService);
            _root = Node.CreateNodeSO(_rootSO);
            _root.Tree = this;
            switch (_rootSO)
            {
                case CompositeSO compositeSO:
                    LoopSetupChild((CompositeNode)_root, compositeSO.Children);
                    break;
                case DecoratorSO decoratorSO:
                    LoopSetupChild((DecoratorNode)_root, decoratorSO.Child);
                    break;
            }

            _root.ReturnedEvent = WaitForNextFrame;
        }

        private void LoopSetupChild(CompositeNode parent, List<NodeSO> childsSO)
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
            var node = Node.CreateNodeSO(childSO);
            node.Tree = this;
            return node;
        }

        private void SetupChild(NodeSO childSO, Node child)
        {
            child.SetNodeSO(childSO);
            if (childSO is CompositeSO compositeSO)
            {
                if (compositeSO.Children.Count != 0)
                {
                    LoopSetupChild((CompositeNode)child, compositeSO.Children);
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
            Dictionary<BehaviorTreeEnums.TreeExternValues, Object> dependencyExternValuesObjects =
                new Dictionary<BehaviorTreeEnums.TreeExternValues, Object>();
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, Object> dependencyEnemyValuesObjects =
                new Dictionary<BehaviorTreeEnums.TreeEnemyValues, Object>();
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

        private async void WaitForNextFrame()
        {
            await UniTask.DelayFrame(0);
            _root.Evaluate();
        }

        public void StopTree()
        {
            foreach (var node in ResetNodeList)
            {
                node.Stop();
            }
        }

        public async void ResetTree(Action callback)
        {
            await UniTask.DelayFrame(0);
            _nodeValuesSharer.InternValues.Clear();
            foreach (var node in ResetNodeList)
            {
                node.Reset();
            }
            callback?.Invoke();
        }

        public void ReplayTree()
        {
            foreach (var node in ResetNodeList)
            {
                node.Replay();
            }
            _root.Evaluate();
        }
    }
}