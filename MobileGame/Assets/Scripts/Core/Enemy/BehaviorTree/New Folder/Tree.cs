using System;
using Environnement.MoveGrid;
using UnityEngine;

namespace BehaviorTree
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private InnerNodeStructSO _rootSO;

        private Node root;

        [SerializeField] private Blackboard _blackboard;
        
        public void Setup(Transform playerTransform, ITickeableService tickeableService, EnvironmentGridManager environmentGridManager)
        {
            _blackboard = new Blackboard(playerTransform, tickeableService, environmentGridManager);
            root = Node.CreateNodeSO(_rootSO);
            LoopSetUpChild(root, _rootSO.Childs);
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
                    var actionChild = (ActionNode)child; 
                    actionChild.SetDataSO(actionNodeStructSo.data);
                    // faire la logique de set les values ici
                }
            
            }
        }
    }
}