using Interfaces;

namespace Service.UI
{
    public interface IUICanvasService : IService
    {
        void LoadMainMenu();
        void LoadInGameMenu(ILifeable interfaceLifeable, IDamageable interfaceDamageable);
        void LoadPopUpCanvas();
    }
}