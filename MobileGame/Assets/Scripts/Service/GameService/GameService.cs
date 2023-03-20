using Addressables;
using Attributes;
using Service.Inputs;
using Service.UI;

namespace Service
{
    public class GameService : IGameService
    {
        [DependsOnService] private IUICanvasSwitchableService _canvasService;

        [DependsOnService] private IInputService _inputService;
        
        public GlobalSettingsGameSO GlobalSettingsSO
        {
            get => _globalSettingsSO;
        }

        private GlobalSettingsGameSO _globalSettingsSO;

        // todo: get so with all environment string addressable
        // todo: launch fight

        [ServiceInit]
        private void Initialize()
        {
            AddressableHelper.LoadAssetAsyncWithCompletionHandler<GlobalSettingsGameSO>("GlobalSettingsGame",
                LoadGlobalSettingsSO);
        }

        private void LoadGlobalSettingsSO(GlobalSettingsGameSO so)
        {
            _globalSettingsSO = so;
            _canvasService.LoadMainMenu();
        }
    }
}