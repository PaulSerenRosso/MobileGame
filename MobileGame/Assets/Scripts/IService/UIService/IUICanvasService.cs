using System;

namespace Service.UI
{
    public interface IUICanvasService : IService
    {
        void LoadMainMenu();
        void LoadInGameMenu();
        void LoadPopUpCanvas();

        public event Action InitCanvasEvent;
    }
}