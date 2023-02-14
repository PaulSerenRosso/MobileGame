namespace Service
{
    public interface IUICanvasService : IService
    {
        void LoadMainMenu();
        void LoadInGameMenu();
        void LoadPopUpCanvas();
    }
}