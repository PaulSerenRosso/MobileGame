using System.Collections.Generic;
using BehaviorTree;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeEditor
{
    public class ActionNodeRender : NodeRender
    {
        protected GUIStyle internValueBox = new(GUI.skin.box);
        
        private ActionNodeSO _so;
        private List<InternValue> _getValues = new();
        private List<InternValue> _setValues = new();
        private List<StringWithHashCode> _removedValues = new();
        private Color _orange = new(1f, 0.5f, 0.2f, 1);
        private Color _violet = new(0.6f, 0, 1f);

        public override NodeSO GetSO()
        {
            return _so;
        }
        
        protected override void Render()
        {
            base.Render();
            EditorGUILayout.ObjectField("DATA", _so.Data, _so.Data.GetType(), false);
            if (_getValues.Count != 0)
            {
                UpdateInterValuesBlock(Color.cyan, "Get Values", _getValues);
            }

            if (_setValues.Count != 0)
            {
                UpdateInterValuesBlock(_orange, "Set Values", _setValues);
            }

            if (_removedValues.Count != 0)
            {
                UpdateInterValuesBlock(_violet, "Removed Values", _removedValues);
            }
            
            EditorGUILayout.EndVertical();
        }
        
        private void UpdateInterValuesBlock(Color color, string title, List<InternValue> internValuesBlock)
        {
            EditorGUILayout.BeginVertical();
            GUI.backgroundColor = color;
            GUILayout.BeginHorizontal(_titleStyle);
            GUILayout.Label(title);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = BehaviourTreeViewerWindow.BaseColor;
            foreach (var internValue in internValuesBlock)
            {
                if (internValue.Key == "")
                {
                    GUI.contentColor = Color.red;
                    GUILayout.Label(internValue.Type + " Key is missing");
                    GUI.contentColor = Color.white;
                }
                else
                {
                    GUILayout.Label(internValue.Type + " " + internValue.Key);
                }
            }

            EditorGUILayout.EndVertical();
        }

        private void UpdateInterValuesBlock(Color color, string title, List<StringWithHashCode> internValuesBlock)
        {
            EditorGUILayout.BeginVertical();
            GUI.backgroundColor = color;
            GUILayout.BeginHorizontal(_titleStyle);
            GUILayout.Label(title);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = BehaviourTreeViewerWindow.BaseColor;
            foreach (var internValue in internValuesBlock)
            {
                if (internValue.Key == "")
                {
                    GUI.contentColor = Color.red;
                    GUILayout.Label("Key is missing");
                    GUI.contentColor = Color.white;
                }
                else
                {
                    GUILayout.Label(internValue.Key);
                }
            }

            EditorGUILayout.EndVertical();
        }

        public ActionNodeRender(BehaviourTreeViewerWindow behaviourTreeViewerWindow, Color backgroundColor, ActionNodeSO so,
            string titleName) : base(
            behaviourTreeViewerWindow, backgroundColor, titleName)
        {
            _so = so;
            foreach (var internValue in _so.InternValues)
            {
                switch (internValue.PropertyType)
                {
                    case BehaviorTreeEnums.InternValuePropertyType.GET:
                    {
                        _getValues.Add(internValue);
                        break;
                    }
                    case BehaviorTreeEnums.InternValuePropertyType.SET:
                    {
                        _setValues.Add(internValue);
                        break;
                    }
                    case BehaviorTreeEnums.InternValuePropertyType.GETANDSET:
                    {
                        _setValues.Add(internValue);
                        _getValues.Add(internValue);
                        break;
                    }
                    case BehaviorTreeEnums.InternValuePropertyType.REMOVE:
                    {
                        _removedValues.Add(internValue);
                        break;
                    }
                }
            }
        }
    }
}