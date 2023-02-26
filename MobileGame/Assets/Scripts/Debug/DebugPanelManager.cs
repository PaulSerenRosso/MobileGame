using HelperPSR.Debugs;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using TMPro;
using UnityEngine;

namespace UIDebugs
{
    public class DebugPanelManager : MonoBehaviour, IUpdatable
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private GameObject _console;
        [SerializeField] private TextMeshProUGUI _frameRateText;

        private float _frameRate;

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
            _root.SetActive(!_root.activeSelf);
        }

        public void OpenOrCloseConsole()
        {
            _console.SetActive(!_console.activeSelf);
        }

        public void OnUpdate()
        {
            _frameRate = DebugHelper.GetFrameRate();
            _frameRateText.text = _frameRate.ToString("F2");
        }
    }
}