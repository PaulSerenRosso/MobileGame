using System;

namespace Service.UI
{
    public interface IUICanvasService : IService
    {
        public void LoadMainMenu();
        public void LoadInGameMenu();
        public void OpenFightTutoPanel();
        public void OpenMoveTutoPanel();
        public void OpenTauntTutoPanel();
        public void OpenUltimateTutoPanel();
        

        public event Action InitCanvasEvent;
    }
}