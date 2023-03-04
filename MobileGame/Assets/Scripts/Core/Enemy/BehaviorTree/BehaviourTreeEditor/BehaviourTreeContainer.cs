using System.Collections.Generic;
using BehaviorTree;
using BehaviorTree.SO.Composite;
using BehaviorTree.SO.Decorator;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeEditor
{
    public class BehaviourTreeContainer
    {
        private Vector2 _scrollView;
        private BehaviourTreeWindow _behaviourTreeWindow;
        public InnerNodeRender SelectedNodeRender;
        public BehaviourTreeContainer PreviousContainer;
        public int Index;
        private GUIStyle nodeContainerStyle;
        private bool _isClose;
        private List<NodeRender> _nodeRenders;
        
        public BehaviourTreeContainer()
        {
            _nodeRenders = new List<NodeRender>();
        }

        public int GetCurrentNodeRenderCount() => _nodeRenders.Count;
        public void Init(BehaviourTreeWindow behaviourTreeWindow, List<NodeRender> nodesRenders,
            BehaviourTreeContainer previousContainer, int index)
        {
            _behaviourTreeWindow = behaviourTreeWindow;
            _nodeRenders = nodesRenders;
            _isClose = false;
            if (previousContainer != null)
                PreviousContainer = previousContainer;
            Index = index;
        }
        public bool ContainerNode(NodeSO so)
        {
            foreach (var nodeRender in _nodeRenders)
            {
                if (nodeRender.GetSO() == so)
                    return true;
            }

            return false;
        }

        public NodeSO GetNodeSO(int index)
        {
            return _nodeRenders[index].GetSO();
        }

        public NodeRender GetNodeRender(int index)
        {
            return _nodeRenders[index];
        }

        public void SetNodeRenderer(NodeRender newNodeRender, int index)
        {
            _nodeRenders[index] = newNodeRender;
        }
        public void RenderContainer()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            _scrollView =
                EditorGUILayout.BeginScrollView(_scrollView, true, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar,
                    GUI.skin.box, GUILayout.Width(_behaviourTreeWindow.position.width * 0.8f),
                    GUILayout.Height(_behaviourTreeWindow.position.height / 5));
            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < _nodeRenders.Count; i++)
            {
                GUILayout.FlexibleSpace();
                _nodeRenders[i].RenderNode();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        public void AddNode(NodeRender nodeRender)
        {
            _nodeRenders.Add(nodeRender);
        }

        public void RemoveAtNode(int index) => _nodeRenders.RemoveAt(index);


    }
}