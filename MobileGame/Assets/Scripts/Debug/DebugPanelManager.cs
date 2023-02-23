using System;
using System.Collections;
using System.Collections.Generic;
using HelperPSR.Debugs;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using TMPro;
using UnityEngine;

namespace UIDebugs
{
public class DebugPanelManager : MonoBehaviour, IUpdatable
{
    [SerializeField]
    private GameObject root;

    [SerializeField] private GameObject console;
    [SerializeField] private TextMeshProUGUI frameRateText;

    private float frameRate; 
    private void Awake()
    { 
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        UpdateManager.Register(this);
    }

    public void FetchRemote()
    {
        RemoteConfigManager.CallFetch();
    }

    public void OpenOrCloseRoot()
    {
        root.SetActive(!root.activeSelf);
    }
    public void OpenOrCloseConsole()
    {
        console.SetActive(!console.activeSelf);
    }


    public void OnUpdate()
    {
        frameRate = DebugHelper.GetFrameRate();
        frameRateText.text = frameRate.ToString("F2");
    }
}
    
}
