using BehaviorTree;
using BehaviorTree.SO;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeEditor
{
    public class InnerNodeRender : NodeRender
    {
        private bool _isSelected;
        private string _selectedButtonName;
        private InnerNodeSO _so;

        public override NodeSO GetSO()
        {
            return _so;
        }
        

        protected override void Render()
        {
            if (_isSelected)
            {
                GUI.backgroundColor = Color.red;
                _selectedButtonName = "Hide Children";
            }
            else
            {
                GUI.backgroundColor = Color.green;
                _selectedButtonName = "Show Children";
            }

          
            if (GUILayout.Button(_selectedButtonName))
            {
                if (_isSelected)
                {
                    CancelSelection();
                }
                else
                {
                    Select();
                }
            }
            GUI.backgroundColor = BehaviourTreeViewerWindow.BaseColor;
            base.Render();
            EditorGUILayout.EndVertical();
        }

        private void Select()
        {
            if (_currentContainer.SelectedNodeRender != null)
            {
                _currentContainer.SelectedNodeRender._isSelected = false;
                BehaviourTreeViewerWindow.RemoveContainers(_currentContainer.Index + 1);
            }

            _currentContainer.SelectedNodeRender = this;
            _isSelected = true;
            BehaviourTreeViewerWindow.AddChildContainer(_so);
        }

        public  void CancelSelection()
        {
            _currentContainer.SelectedNodeRender = null;
            BehaviourTreeViewerWindow.RemoveContainers(_currentContainer.Index + 1);
            _isSelected = false;
        }
        
        public InnerNodeRender(BehaviourTreeViewerWindow behaviourTreeViewerWindow, BehaviourTreeContainer currentContainer,
            Color backgroundColor, InnerNodeSO so, string titleName) : base(behaviourTreeViewerWindow, backgroundColor, titleName)
        {
            _so = so;
            _currentContainer = currentContainer;
        }
    }
}