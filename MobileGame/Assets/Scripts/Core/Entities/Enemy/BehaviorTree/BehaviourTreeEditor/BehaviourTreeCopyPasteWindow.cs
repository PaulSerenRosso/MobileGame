using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BehaviorTree;
using BehaviorTree.Nodes.Actions;
using BehaviorTree.Nodes.Composite;
using BehaviorTree.Nodes.Decorator;
using BehaviorTree.SO;
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
        private const string _actionNodeDirectoryName = "ActionNodes";
        private const string _taskNodeDirectoryName = "Tasks";
        private const string _checkNodeDirectoryName = "Checks";
        private const string _actionNodeDataDirectoryName = "ActionNodesData";
        private const string _innerNodeDirectoryName = "InnerNodes";
        private const string _compositeNodeFolderDirectoryame = "Composites";
        private const string _decoratorNodeDirectoryName = "Decorators";
        private const string separatorDirectory = "/";
        [MenuItem("Window/BehaviourTree/CopyPaste")]
        static void Init()
        {
            BehaviourTreeCopyPasteWindow viewerWindow = (BehaviourTreeCopyPasteWindow)GetWindow(typeof(BehaviourTreeCopyPasteWindow));
            viewerWindow.Show();
        }


        private void OnGUI()
        {
            _nodeToDuplicate = EditorGUILayout.ObjectField("Node To Duplicate", _nodeToDuplicate, typeof(NodeSO), false) as NodeSO;
            _pathOfRoot =
                EditorGUILayout.TextField("Path To Create Duplicate Node", _pathOfRoot);
            GUI.enabled = _pathOfRoot != "" && _nodeToDuplicate != null;
            if (GUILayout.Button("Duplicate Node"))
            {
                TryCreateFoldersNeeded();
                
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
            TryCreateFolder(_innerNodeDirectoryName, _compositeNodeFolderDirectoryame);
            TryCreateFolder(_innerNodeDirectoryName, _decoratorNodeDirectoryName);
            CreateNodeSOAssetWithTypeNode(_nodeToDuplicate);
            AssetDatabase.Refresh();
        }

        private bool TryCreateFolder(params string[] subFolderName)
        {
            string currentPath = _pathOfRoot;
            for (int i = 0; i < subFolderName.Length; i++)
            {
                currentPath += (separatorDirectory + subFolderName[i]);
            }
            Debug.Log(currentPath);
            if(!AssetDatabase.IsValidFolder(currentPath))
            {
                Directory.CreateDirectory(currentPath);
                return true;
            }

            return false; 
        }

        private void CreateNodeSOAssetWithTypeNode(NodeSO nodeSo)
        {
            switch (nodeSo)
            {
                case CompositeSO composite:
                {
                        CreateNodeSOAsset(nodeSo, _pathOfRoot+separatorDirectory+_innerNodeDirectoryName+separatorDirectory+_compositeNodeFolderDirectoryame);
                   // for (int i = 0; i < composite.Children.Count; i++)
                    //{
                     //   CreateNodeSOAssetWithTypeNode(composite.Children[i]);
                    //}
                    break;
                }
                
                case DecoratorSO decorator:
                {
                    break;
                }
                
                case CheckNodeSO check:
                {
                    break;
                }

                case TaskNodeSO task:
                {
                    break;
                }
            }
        }

        private void CreateNodeSOAsset(NodeSO nodeSo, string path)
        {
            AssetDatabase.CreateAsset(nodeSo, _pathOfRoot);
        }
  
    }
}
#endif