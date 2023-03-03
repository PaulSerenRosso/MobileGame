using System.Collections.Generic;
using BehaviorTree;
using BehaviorTree.SO.Composite;
using BehaviorTree.SO.Decorator;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using InnerNodeSO = BehaviorTree.SO.InnerNodeSO;

#if UNITY_EDITOR
namespace BehaviorTreeEditor
{
    public class BehaviourTreeWindow : EditorWindow
    {
        private InnerNodeRender _rootRender;
        private Vector2 _testscrollPos;
        private Vector2 _scrollPos;
        private List<BehaviourTreeChildContainer> _containersToRender = new();
        private InnerNodeSO _root;
        public Color BaseColor;

        [MenuItem("Window/BehaviourTree")]
        static void Init()
        {
            BehaviourTreeWindow window = (BehaviourTreeWindow)GetWindow(typeof(BehaviourTreeWindow));
            window.Show();
        }

        private void OnEnable()
        {
            BaseColor = GUI.color;
        }

        public void RemoveContainer(BehaviourTreeChildContainer container)
        {
            _containersToRender.Remove(container);
        }

        public void AddContainer(NodeSO so)
        {
            var newContainer = new BehaviourTreeChildContainer();
            List<NodeRender> nodeRenders = new List<NodeRender>();
            nodeRenders.Add(CreateNodeRender(so, newContainer));
            CreateContainer(newContainer, nodeRenders);
        }

        public void AddChildContainer(InnerNodeSO innerNode)
        {
            var newContainer = new BehaviourTreeChildContainer();
            List<NodeRender> nodeRenders = new List<NodeRender>();
            if (innerNode is CompositeSO compositeParentSo)
            {
                for (int i = 0; i < compositeParentSo.Childs.Count; i++)
                {
                    nodeRenders.Add(CreateNodeRender(compositeParentSo.Childs[i], newContainer));
                }
            }
            else if (innerNode is DecoratorSO decoratorParentSo)
            {
                nodeRenders.Add(CreateNodeRender(decoratorParentSo.Child, newContainer));
            }

            CreateContainer(newContainer, nodeRenders);
        }

        private void CreateContainer(BehaviourTreeChildContainer newContainer, List<NodeRender> nodeRenders)
        {
            if (_containersToRender.Count < 2)
            {
                newContainer.Init(this, nodeRenders, null, _containersToRender.Count);
                _containersToRender.Add(newContainer);
            }
            else
            {
                newContainer.Init(this, nodeRenders, _containersToRender[^1], _containersToRender.Count);
                _containersToRender.Add(newContainer);
            }
        }

        public void RemoveContainers(int startContainerIndex)
        {
            for (int i = _containersToRender.Count - 1; i >= startContainerIndex; i--)
            {
                _containersToRender.RemoveAt(i);
            }
        }

        public NodeRender CreateNodeRender(NodeSO so, BehaviourTreeChildContainer container)
        {
            return so switch
            {
                DecoratorSO decoratorSo => new InnerNodeRender(this, container, Color.yellow, decoratorSo),
                CompositeSO compositeSo => new InnerNodeRender(this, container, Color.blue, compositeSo),
                ActionNodeSO actionNodeSo => new ActionNodeRender(this, Color.magenta,
                    actionNodeSo),
                _ => null
            };
        }

        private void OnGUI()
        {
            GUI.color = BaseColor;

            EditorGUILayout.BeginVertical();
            _root = EditorGUILayout.ObjectField("Root", _root, typeof(InnerNodeSO), false) as InnerNodeSO;
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, true, GUIStyle.none,
                GUI.skin.verticalScrollbar,
                GUIStyle.none);
            if (_root)
            {
                if (_containersToRender.Count == 0)
                {
                    AddContainer(_root);
                }

                for (int i = 0; i < _containersToRender.Count; i++)
                {
                    _containersToRender[i].RenderContainer();
                    GUILayout.Space(10f);
                }
            }
            
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
    }
}
#endif