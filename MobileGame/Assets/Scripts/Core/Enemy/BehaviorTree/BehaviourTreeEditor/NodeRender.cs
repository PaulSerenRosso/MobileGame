using BehaviorTree;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeEditor
{
    public abstract class NodeRender
    {
        protected BehaviourTreeWindow _behaviourTreeWindow;
        protected BehaviourTreeContainer _currentContainer;
        
        private Color _backgroundColor;
        private string _titleName;
        
        protected GUIStyle _titleStyle = new("HelpBox")
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };

        protected GUIStyle _nodeRenderStyle = new("HelpBox");

        public abstract NodeSO GetSO();

        public NodeRender(BehaviourTreeWindow behaviourTreeWindow, Color backgroundColor, string titleName)
        {
            _behaviourTreeWindow = behaviourTreeWindow;
            _backgroundColor = backgroundColor;
            _titleName = titleName;
        }

        public void RenderNode()
        {
            BeginRender();
            Render();
        }

        protected virtual void BeginRender()
        {
            EditorGUIUtility.labelWidth = 50;
            EditorGUILayout.BeginVertical(_nodeRenderStyle, GUILayout.Width(200));
            GUI.backgroundColor = _backgroundColor;
            EditorGUILayout.BeginHorizontal(_titleStyle);
            GUILayout.FlexibleSpace();
            GUILayout.Label(_titleName);
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

/*
  * "ToolbarButton"
"CN Box"
"Button"
"CN EntryBackEven"
"Toolbar"
"CN EntryInfo"
"CN EntryBackodd"
"CN StatusWarn"
"CN StatusError"
"Box"
"CN StatusInfo"
"CN CountBadge"
"MiniButtonMiddle"
"MiniButton"
"MiniButtonLeft"
"ErrorStyle"
"MiniButtonRight"
"LogStyle"
"CN Message"
"WarningStyle"
"MessageStyle"
"EvenBackground"
"OddBackground"
"StatusError"
"ObjectPickerResultsGrid"
"PR Label"
"ProjectBrowserGridLabel"
"ProjectBrowserHeaderBgMiddle"
"ProjectBrowserSubAssetExpandBtn"
"toolbarDropDown"
"ToolbarSeachTextField"
"SearchCancelButtonEmpty"
"ToolbarSeachCancelButton"
"SearchTextField"
"ObjectPickerBackground"
"ObjectPickerPreviewBackground"
"Foldout"
"ProjectBrowserHeaderBgTop"
"NotificationBackground"
"ObjectPickerToolbar"
"PR Ping"
"PR TextField"
"ProjectBrowserIconDropShadow"
"ProjectBrowserPreviewBg"
"CN EntryError"
"ProjectBrowserTextureIconDropShadow"
"ProjectBrowserSubAssetBg"
"ProjectBrowserSubAssetBgOpenEnded"
"WhiteLabel"
"CountBadge"
"StatusWarn"
"ProjectBrowserSubAssetBgDivider"
"In BigTitle"
"StatusLog"
"WhiteMiniLabel"
"LargeLabel"
"BoldLabel"
"ColorPickerBox"
"ProjectBrowserSubAssetBgCloseEnded"
"ProjectBrowserSubAssetBgMiddle"
"WordWrappedLabel"
"CN EntryWarn"
"MiniBoldLabel"
"MiniTextField"
"WhiteLargeLabel"
"WhiteBoldLabel"
"miniButton"
"miniButtonLeft"
"WordWrappedMiniLabel"
"miniButtonRight"
"Radio"
"toolbarPopup"
"toolbarTextField"
"miniButtonMid"
"ToolbarSeachCancelButtonEmpty"
"ToolbarSeachTextFieldPopup"
"SearchCancelButton"
"HelpBox"
"AssetLabel Partial"
"MinMaxHorizontalSliderThumb"
"AssetLabel Icon"
"ProjectBrowserIconAreaBg"
"selectionRect"
"AssetLabel"
"Label"
"BoldLabel"
"DropDownButton"
"MiniLabel"
"FoldoutPreDrop"
"ProgressBarBack"
"ProgressBarBar"
"ProgressBarText"
"Tooltip"
"IN Title"
"MiniBoldLabel"
"BoldToggle"
"textField"
"IN TitleText"
"Foldout"
"NotificationText"
"ControlLabel"
"ToggleMixed"
"ObjectField"
"ObjectFieldThumb"
"MiniPopup"
"ObjectFieldMiniThumb"
"TextFieldDropDownText"
"Toggle"
"ColorField"
"TextFieldDropDown"
  */