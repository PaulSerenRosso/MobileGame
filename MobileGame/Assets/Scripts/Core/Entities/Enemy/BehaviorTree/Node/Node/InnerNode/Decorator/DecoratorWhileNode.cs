using BehaviorTree.SO.Decorator;
using Cysharp.Threading.Tasks;

namespace BehaviorTree.Nodes.Decorator
{
    public class DecoratorWhileNode : DecoratorNode
    {
        private DecoratorWhileSO _so;

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (DecoratorWhileSO)nodeSO;
        }

        public override void EvaluateChild()
        {
            if (Child.State != _so.WhileStateCondition)
            {
                State = Child.State;
                ReturnedEvent?.Invoke();
            }
            else
            {
                WaitEndOfFrame();
            }
        }

        public override void Evaluate()
        {
            Child.Evaluate();
        }

        private async void WaitEndOfFrame()
        {
            await UniTask.DelayFrame(0);
            Child.Evaluate();
        }
    }
}