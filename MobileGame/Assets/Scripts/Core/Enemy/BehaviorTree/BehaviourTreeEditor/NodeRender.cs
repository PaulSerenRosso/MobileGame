using BehaviorTree;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeEditor
{
    public abstract class NodeRender
    {
        protected BehaviourTreeWindow _behaviourTreeWindow;
        protected BehaviourTreeChildContainer _currentContainer;
        private Color _backgroundColor;
        
        protected GUIStyle _titleStyle = new GUIStyle(GUI.skin.window)
        {
            
            alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold
        };
        
        public abstract NodeSO GetSO();

        public NodeRender(BehaviourTreeWindow behaviourTreeWindow, Color backgroundColor)
        {
            _behaviourTreeWindow = behaviourTreeWindow;
            _backgroundColor = backgroundColor;
        }
        

        public void RenderNode()
        {
            BeginRender();
            Render();
        }

        protected virtual void BeginRender()
        {
            EditorGUIUtility.labelWidth = 50;
            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(_behaviourTreeWindow.position.width / 3));
            GUI.backgroundColor = _backgroundColor;
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(GetSO().GetTypeNode().Name, _titleStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUI.backgroundColor = _behaviourTreeWindow.BaseColor;
            GUI.backgroundColor = Color.red;
            GUI.backgroundColor = _behaviourTreeWindow.BaseColor;
        }


        protected virtual void Render()
        {
            EditorGUILayout.ObjectField("SO", GetSO(), GetSO().GetType(), false);
        }
    }
}