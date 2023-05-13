using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using Service.UI;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskOpenPopupPanelNode : ActionNode
    {
        private TaskOpenPopupPanelNodeSO _so;
        private TaskOpenPopupPanelNodeDataSO _data;
        private IUICanvasSwitchableService _canvasService;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskOpenPopupPanelNodeSO)nodeSO;
            _data = (TaskOpenPopupPanelNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            switch (_data.PopupValue)
            {
                case BehaviorTreeEnums.PopupValue.MOVE:
                    _canvasService.OpenMoveTutoPanel();
                    break;
                case BehaviorTreeEnums.PopupValue.TAUNT:
                    _canvasService.OpenTauntTutoPanel();
                    break;
                case BehaviorTreeEnums.PopupValue.ULTIMATE:
                    _canvasService.OpenUltimateTutoPanel();
                    break;
            }
            
            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _canvasService =
                (IUICanvasSwitchableService)externDependencyValues[BehaviorTreeEnums.TreeExternValues.UICanvasService];
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}