using System;
using System.IO;
using BehaviorTree;
using BehaviorTree.SO.Actions;
using BehaviorTree.SO.Composite;
using BehaviorTree.SO.Decorator;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace BehaviorTreeEditor
{
    // faire le copy paste
    // faire la création des folders
    public class BehaviourTreeCopyPasteWindow : EditorWindow
    {
        private NodeSO _nodeToDuplicate;
        private string _pathOfRoot;
        private string _prefixOfNodeToDuplicate;
        
        private const string _actionNodeDirectoryName = "ActionNodes";
        private const string _taskNodeDirectoryName = "Tasks";
        private const string _checkNodeDirectoryName = "Checks";
        private const string _actionNodeDataDirectoryName = "ActionNodesData";
        private const string _innerNodeDirectoryName = "InnerNodes";
        private const string _compositeNodeFolderDirectoryName = "Composites";
        private const string _decoratorNodeDirectoryName = "Decorators";
        private const string separatorDirectory = "/";
        private const string assetExtension = ".asset";

        private string _taskPath;
        private string _checkPath;
        private string _checkDataPath;
        private string _taskDataPath;
        private string _decoratorNodePath;
        private string _compositeNodePath;


        [MenuItem("Window/BehaviourTree/CopyPaste")]
        static void Init()
        {
            BehaviourTreeCopyPasteWindow viewerWindow =
                (BehaviourTreeCopyPasteWindow)GetWindow(typeof(BehaviourTreeCopyPasteWindow));
            viewerWindow.Show();
        }


        private void OnGUI()
        {
            _nodeToDuplicate =
                EditorGUILayout.ObjectField("Node To Duplicate", _nodeToDuplicate, typeof(NodeSO), false) as NodeSO;
            _pathOfRoot =
                EditorGUILayout.TextField("Path To Create Duplicate Node", _pathOfRoot);
            _prefixOfNodeToDuplicate =
                EditorGUILayout.TextField("Prefix used To Create Duplicate Node", _prefixOfNodeToDuplicate);
            GUI.enabled = _pathOfRoot != "" && _nodeToDuplicate != null  && _prefixOfNodeToDuplicate != "";
            if (GUILayout.Button("Duplicate Node"))
            {
                TryCreateFoldersNeeded();
                SetupPaths();
                CreateNodeSOAssetWithTypeNode(_nodeToDuplicate);
                AssetDatabase.Refresh();
            }

            GUI.enabled = true;
        }

        private void TryCreateFoldersNeeded()
        {
            TryCreateFolder(_actionNodeDirectoryName);
            TryCreateFolder(_actionNodeDirectoryName, _checkNodeDirectoryName);
            TryCreateFolder(_actionNodeDirectoryName, _taskNodeDirectoryName);
            TryCreateFolder(_actionNodeDataDirectoryName, _taskNodeDirectoryName);
            TryCreateFolder(_actionNodeDataDirectoryName, _checkNodeDirectoryName);
            TryCreateFolder(_innerNodeDirectoryName, _compositeNodeFolderDirectoryName);
            TryCreateFolder(_innerNodeDirectoryName, _decoratorNodeDirectoryName);
        }


        private bool TryCreateFolder(params string[] subFolderName)
        {
            string currentPath = _pathOfRoot;
            for (int i = 0; i < subFolderName.Length; i++)
            {
                currentPath += (separatorDirectory + subFolderName[i]);
            }

            if (!AssetDatabase.IsValidFolder(currentPath))
            {
                Directory.CreateDirectory(currentPath);
                return true;
            }

            return false;
        }

        private void SetupPaths()
        {
            _taskPath = _pathOfRoot + separatorDirectory + _actionNodeDirectoryName +
                        separatorDirectory +
                        _taskNodeDirectoryName;
            _checkPath = _pathOfRoot + separatorDirectory + _actionNodeDirectoryName +
                         separatorDirectory +
                         _checkNodeDirectoryName;
            _checkDataPath = _pathOfRoot + separatorDirectory + _actionNodeDataDirectoryName + separatorDirectory +
                             _checkNodeDirectoryName;
            _taskDataPath = _pathOfRoot + separatorDirectory + _actionNodeDataDirectoryName + separatorDirectory +
                            _taskNodeDirectoryName;
            _decoratorNodePath = _pathOfRoot + separatorDirectory + _innerNodeDirectoryName +
                                 separatorDirectory +
                                 _decoratorNodeDirectoryName;
            _compositeNodePath = _pathOfRoot + separatorDirectory + _innerNodeDirectoryName +
                                 separatorDirectory +
                                 _compositeNodeFolderDirectoryName;
        }

        private BehaviourTreeSO CreateNodeSOAssetWithTypeNode(BehaviourTreeSO nodeSo)
        {
            switch (nodeSo)
            {
                case CompositeSO composite:
                {
                    return TryCreateNodeSOAsset(nodeSo, _compositeNodePath, SetUpCompositeNodeAsset);
                }

                case DecoratorSO decorator:
                {
                    return TryCreateNodeSOAsset(nodeSo, _decoratorNodePath, SetUpDecoratorNodeAsset);
                }

                case CheckNodeSO check:
                {
                    return TryCreateNodeSOAsset(nodeSo, _checkPath, SetUpCheckNodeAsset);
                }

                case TaskNodeSO task:
                {
                    return TryCreateNodeSOAsset(nodeSo, _taskPath, SetUpTaskNodeAsset);
                }
            }

            throw new Exception("Is not a Valid type node");
        }

        void SetUpCompositeNodeAsset(BehaviourTreeSO nodeSo)
        {
            var compositeDuplication = (CompositeSO)nodeSo;
            for (int i = 0; i < compositeDuplication.Children.Count; i++)
            {
                var childDuplication = CreateNodeSOAssetWithTypeNode(compositeDuplication.Children[i]);
                if (childDuplication)
                {
                    compositeDuplication.Children[i] = (NodeSO)childDuplication;
                }
            }
        }


        void SetUpDecoratorNodeAsset(BehaviourTreeSO nodeSo)
        {
            var decoratorDuplication = (DecoratorSO)nodeSo;

            var childDuplication = CreateNodeSOAssetWithTypeNode(decoratorDuplication.Child);
            if (childDuplication)
            {
                decoratorDuplication.Child = (NodeSO)childDuplication;
            }
        }

        void SetUpActionNodeAsset(BehaviourTreeSO nodeSo, string pathForData)
        {
            var actionNodeDuplication = (ActionNodeSO)nodeSo;

            var data = CreateNodeSOAsset(actionNodeDuplication.Data, pathForData);
            if (data)
            {
                actionNodeDuplication.Data = (ActionNodeDataSO)data;
            }
        }

        void SetUpCheckNodeAsset(BehaviourTreeSO nodeSo)
        {
            SetUpActionNodeAsset(nodeSo, _checkDataPath);
        }


        void SetUpTaskNodeAsset(BehaviourTreeSO nodeSo)
        {
            SetUpActionNodeAsset(nodeSo, _taskDataPath);
        }

        private BehaviourTreeSO TryCreateNodeSOAsset(BehaviourTreeSO nodeSo, string path,
            Action<BehaviourTreeSO> successEvent)
        {
            var duplicationNode = CreateNodeSOAsset(nodeSo,
                path);
            if (duplicationNode != null)
            {
                successEvent?.Invoke(duplicationNode);
                return duplicationNode;
            }

            return null;
        }


        // change le spé 
        // change le nom pour le perso
        private BehaviourTreeSO CreateNodeSOAsset(BehaviourTreeSO nodeSo, string path)
        {
            if (nodeSo.IsDuplicable)
            {
                string newPath = "";
                newPath = path + separatorDirectory + _prefixOfNodeToDuplicate +"_"+ nodeSo.name.Split("_", 2)[1] + assetExtension;
                var currentAssetAtPath = AssetDatabase.LoadAssetAtPath(newPath, nodeSo.GetType());
                if (currentAssetAtPath)
                {
                    return (BehaviourTreeSO)currentAssetAtPath;
                }
                AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(nodeSo), newPath);
                return (BehaviourTreeSO)AssetDatabase.LoadAssetAtPath(newPath, nodeSo.GetType());
            }
            return null;
        }
    }
}
#endif