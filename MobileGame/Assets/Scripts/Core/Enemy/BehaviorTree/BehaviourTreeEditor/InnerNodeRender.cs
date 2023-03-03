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
                    _currentContainer.SelectedNodeRender = null;
                    _behaviourTreeWindow.RemoveContainers(_currentContainer.Index + 1);
                    _isSelected = false;
                }
                else
                {
                    if (_currentContainer.SelectedNodeRender != null)
                    {
                        _currentContainer.SelectedNodeRender._isSelected = false;
                        _behaviourTreeWindow.RemoveContainers(_currentContainer.Index + 1);
                    }

                    _currentContainer.SelectedNodeRender = this;
                    _isSelected = true;
                    _behaviourTreeWindow.AddChildContainer(_so);
                }
            }
            GUI.backgroundColor = _behaviourTreeWindow.BaseColor;
            base.Render();
            EditorGUILayout.EndVertical();
        }


        public InnerNodeRender(BehaviourTreeWindow behaviourTreeWindow, BehaviourTreeContainer currentContainer,
            Color backgroundColor, InnerNodeSO so) : base(behaviourTreeWindow, backgroundColor)
        {
            _so = so;
            _currentContainer = currentContainer;
        }
    }
}