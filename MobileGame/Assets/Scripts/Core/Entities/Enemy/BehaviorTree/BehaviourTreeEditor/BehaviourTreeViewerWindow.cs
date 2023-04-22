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
    public class BehaviourTreeViewerWindow : EditorWindow
    {
        public Color BaseColor;
        public List<BehaviourTreeContainer> _childContainers = new();

        private InnerNodeRender _rootRender;
        private Vector2 _testscrollPos;
        private Vector2 _scrollPos;
        private List<BehaviourTreeContainer> _containersToRender = new();
        private InnerNodeSO _root;
        private Color _colorCheck = new(0f, 1f, 0f);

        [MenuItem("Window/BehaviourTree/Viewer")]
        static void Init()
        {
            BehaviourTreeViewerWindow viewerWindow = (BehaviourTreeViewerWindow)GetWindow(typeof(BehaviourTreeViewerWindow));
            viewerWindow.Show();
        }

        private void OnEnable()
        {
            BaseColor = GUI.color;
        }

        public void AddContainer(NodeSO so)
        {
            var newContainer = new BehaviourTreeContainer();
            List<NodeRender> nodeRenders = new List<NodeRender>();
            nodeRenders.Add(CreateNodeRender(so, newContainer, nodeRenders.Count + 1));
            CreateContainer(newContainer, nodeRenders);
        }

        public void AddChildContainer(InnerNodeSO innerNode)
        {
            var newContainer = new BehaviourTreeContainer();
            List<NodeRender> nodeRenders = new List<NodeRender>();
            if (innerNode is CompositeSO compositeParentSo)
            {
                for (int i = 0; i < compositeParentSo.Children.Count; i++)
                {
                    nodeRenders.Add(CreateNodeRender(compositeParentSo.Children[i], newContainer, nodeRenders.Count + 1));
                }
            }
            else if (innerNode is DecoratorSO decoratorParentSo)
            {
                nodeRenders.Add(CreateNodeRender(decoratorParentSo.Child, newContainer, nodeRenders.Count + 1));
            }

            CreateContainer(newContainer, nodeRenders);
            _childContainers.Add(newContainer);
        }

        private void CreateContainer(BehaviourTreeContainer newContainer, List<NodeRender> nodeRenders)
        {
            if (_containersToRender.Count == 0)
            {
                newContainer.Init(this, nodeRenders, null, _containersToRender.Count);
            }
            else
            {
                newContainer.Init(this, nodeRenders, _containersToRender[^1], _containersToRender.Count);
            }

            _containersToRender.Add(newContainer);
        }

        public void RemoveContainers(int startContainerIndex)
        {
            for (int i = _containersToRender.Count - 1; i >= startContainerIndex; i--)
            {
                _childContainers.Remove(_containersToRender[i]);
                _containersToRender.RemoveAt(i);
            }
        }

        public NodeRender CreateNodeRender(NodeSO so, BehaviourTreeContainer container, int index)
        {
           var treePrefix = so.name.Split("_")[0]+"_";
            return so switch
            {
                DecoratorSO decoratorSo => new InnerNodeRender(this, container, Color.yellow, decoratorSo,
                    index + " - " +treePrefix+ decoratorSo.name.Split("D_")[1]),
                CompositeSO compositeSo => new InnerNodeRender(this, container, Color.blue, compositeSo,
                    index + " - " + treePrefix+compositeSo.name.Split("CO_")[1]),
                TaskNodeSO taskNodeSo => new ActionNodeRender(this, Color.magenta,
                    taskNodeSo, index + " - " +treePrefix+ taskNodeSo.name.Split("T_")[1]),
                CheckNodeSO checkNodeSo => new ActionNodeRender(this, _colorCheck,
                    checkNodeSo, index + " - " +treePrefix+ checkNodeSo.name.Split("CH_")[1]),
                _ => null
            };
        }

        private void OnGUI()
        {
            GUI.color = BaseColor;
            _root = EditorGUILayout.ObjectField("Root", _root, typeof(InnerNodeSO), false) as InnerNodeSO;
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, true, true, GUI.skin.horizontalScrollbar,
                GUI.skin.verticalScrollbar,
                GUIStyle.none);
            if (_root)
            {
                if (_containersToRender.Count == 0)
                {
                    AddContainer(_root);
                }
                else if (!_containersToRender[0].ContainerNode(_root))
                {
                    _containersToRender.Clear();
                    _childContainers.Clear();
                    AddContainer(_root);
                }
                else
                {
                    UpdateChildrenContainers();
                }

                for (int i = 0; i < _containersToRender.Count; i++)
                {
                    _containersToRender[i].RenderContainer();
                    GUILayout.Space(10f);
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void UpdateChildrenContainers()
        {
            for (int i = 0; i < _childContainers.Count; i++)
            {
                var childContainer = _childContainers[i];

                var selectedSOOfPreviousContainer =
                    childContainer.PreviousContainer.SelectedNodeRender.GetSO();
                if (selectedSOOfPreviousContainer is DecoratorSO decoratorSo)
                {
                    if (decoratorSo.Child != childContainer.GetNodeSO(0))
                    {
                        UpdateNodeRenderer(childContainer, 0, decoratorSo.Child);
                    }
                }
                else if (selectedSOOfPreviousContainer is CompositeSO compositeSo)
                {
                    if (compositeSo.Children.Count > childContainer.GetCurrentNodeRenderCount())
                    {
                        for (int j = childContainer.GetCurrentNodeRenderCount();
                             j < compositeSo.Children.Count;
                             j++)
                        {
                            var newNode = CreateNodeRender(compositeSo.Children[j], childContainer, childContainer.GetCurrentNodeRenderCount() + 1);
                            childContainer.AddNode(newNode);
                        }
                    }
                    else if (compositeSo.Children.Count < childContainer.GetCurrentNodeRenderCount())
                    {
                        for (int j = childContainer.GetCurrentNodeRenderCount() - 1;
                             j >= compositeSo.Children.Count;
                             j--)
                        {
                            CancelNodeIfEqual(childContainer, j);
                            childContainer.RemoveAtNode(j);
                        }
                    }

                    for (int j = 0; j < compositeSo.Children.Count; j++)
                    {
                        if (compositeSo.Children[j] != childContainer.GetNodeSO(j))
                        {
                            UpdateNodeRenderer(childContainer, j, compositeSo.Children[j]);
                        }
                    }
                }
            }
        }

        private void CancelNodeIfEqual(BehaviourTreeContainer behaviourTreeContainer, int j)
        {
            if (behaviourTreeContainer.GetNodeRender(j) == behaviourTreeContainer.SelectedNodeRender)
            {
                behaviourTreeContainer.SelectedNodeRender.CancelSelection();
            }
        }

        private void UpdateNodeRenderer(BehaviourTreeContainer behaviourTreeContainer, int j, NodeSO newNodeSo)
        {
            CancelNodeIfEqual(behaviourTreeContainer, j);
            behaviourTreeContainer.SetNodeRenderer(CreateNodeRender(newNodeSo, behaviourTreeContainer, j + 1), j);
        }
    }
}
#endif