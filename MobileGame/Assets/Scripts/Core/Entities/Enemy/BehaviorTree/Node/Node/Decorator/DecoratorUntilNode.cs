using BehaviorTree.SO.Decorator;
using Cysharp.Threading.Tasks;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorUntilNode : DecoratorNode
    {
        private DecoratorUntilSO _so;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorUntilSO)nodeSO;
        }

        public override BehaviorTreeEnums.NodeState Evaluate()
        {
            ChildEvaluateAsync();
            return _so.BreakStateCondition;
        }

        private async void ChildEvaluateAsync()
        {
            while (Child.Evaluate() != _so.BreakStateCondition)
            {
                await UniTask.DelayFrame(0);
            }
        }
    }
}