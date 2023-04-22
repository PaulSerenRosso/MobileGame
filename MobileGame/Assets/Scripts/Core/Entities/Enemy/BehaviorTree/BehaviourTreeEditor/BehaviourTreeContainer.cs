using System.Collections.Generic;
using BehaviorTree;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeEditor
{
    public class BehaviourTreeContainer
    {
        public int Index;
        public InnerNodeRender SelectedNodeRender;
        public BehaviourTreeContainer PreviousContainer;
        
        private Vector2 _scrollView;
        private BehaviourTreeViewerWindow _behaviourTreeViewerWindow;
        private GUIStyle nodeContainerStyle;
        private bool _isClose;
        private List<NodeRender> _nodeRenders;

        public BehaviourTreeContainer()
        {
            _nodeRenders = new List<NodeRender>();
        }

        public int GetCurrentNodeRenderCount() => _nodeRenders.Count;

        public void Init(BehaviourTreeViewerWindow behaviourTreeViewerWindow, List<NodeRender> nodesRenders,
            BehaviourTreeContainer previousContainer, int index)
        {
            _behaviourTreeViewerWindow = behaviourTreeViewerWindow;
            _nodeRenders = nodesRenders;
            _isClose = false;
            if (previousContainer != null) PreviousContainer = previousContainer;
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
            EditorGUILayout.BeginHorizontal(GUI.skin.box);

            for (int i = 0; i < _nodeRenders.Count; i++)
            {
                GUILayout.FlexibleSpace();
                _nodeRenders[i].RenderNode();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
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