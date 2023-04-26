using BehaviorTree.SO.Decorator;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BehaviorTree.Nodes.Decorator
{
    [CreateAssetMenu(menuName = "BehaviorTree/Decorator/DelayReturnerSO",
        fileName = "new Tree_D_DelayReturner_Spe")]
    public class DecoratorDelayReturnerNode : DecoratorNode 
    {
        private DecoratorDelayReturnerNodeSO _so;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorDelayReturnerNodeSO)nodeSO;
        }

        public override async void Evaluate()
        {
            State = _so.ReturnState;
            ReturnedEvent?.Invoke();
            await UniTask.Delay(_so.TimeBeforeToReadChild);
            Child.Evaluate();
        }

        public override void EvaluateChild()
        {
          
        }
    }
    
}