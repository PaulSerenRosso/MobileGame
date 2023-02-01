using Attributes;
using Service.AudioService;

namespace Service
{
    public class GameService : IGameService
    {
        [DependsOnService] private IAudioService _audioService;

        [DependsOnService] private ISceneService _sceneService;

        [DependsOnService] private IUICanvasSwitchableService _canvasService;

        [ServiceInit]
        private void Initialize()
        {
            _canvasService.LoadMainMenu();
        }
    }
}