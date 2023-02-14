using Attributes;

namespace Service
{
    public class InputService : IInputService
    {
        public PlayerInputs PlayerInputs;
        
        [ServiceInit]
        private void Initialize()
        {
            PlayerInputs = new PlayerInputs();
        }

        public void EnablePlayerMap(bool value)
        {
            if (PlayerInputs == null) return;
            if (value) PlayerInputs.Enable();
            else PlayerInputs.Disable();
        }
    }
}