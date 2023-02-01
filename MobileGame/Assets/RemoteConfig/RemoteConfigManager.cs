using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace RemoteConfig
{
    public class RemoteConfigManager : MonoBehaviour
    {
        [SerializeField] private RemoteConfigVariables variables = null;
        [SerializeField] private bool updateAtStart = true;

        private struct userAttributes
        {
        }

        private struct appAttributes
        {
        }

        /// <summary>
        /// Method called before the start of the game
        /// </summary>
        private async void Awake()
        {
            if (Utilities.CheckForInternetConnection()) await InitializeRemoteConfigAsync();

            RemoteConfigService.Instance.FetchCompleted += ApplySettings;
            if (updateAtStart) CallFetch();
        }

        /// <summary>
        /// Async Method which allow to wait for connection before doing something else
        /// </summary>
        private async Task InitializeRemoteConfigAsync()
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        /// <summary>
        /// Method which call the appConfig to retrieve all the data and be able to update the value
        /// </summary>
        public void CallFetch() => RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());

        /// <summary>
        /// Method which is called when the fetching is done
        /// </summary>
        /// <param name="configResponse"></param>
        private void ApplySettings(ConfigResponse configResponse)
        {
            if (configResponse.requestOrigin != ConfigOrigin.Remote) return;

            /*
            variables.perseveranceSo.percentage = RemoteConfigService.Instance.appConfig.GetFloat("HEAL_Percentage");
            variables.perseveranceSo.timeBeforeHealAfterDamage = RemoteConfigService.Instance.appConfig.GetFloat("HEAL_TimeAfterDamage");
            
            SetChampion01Variables();
            SetChampion02Variables();
            SetGeneratorVariables();
            SetRelaiVariables();
            PhotonNetwork.ConnectUsingSettings();
            */
        }
    }
}
