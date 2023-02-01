using UnityEngine;

namespace Service.AudioService
{
    public class AudioService : IAudioService
    {
        public void PlaySound(int id)
        {
            Debug.Log($"Playing sound at id {id}");
            // TODO PLAY SOUND
        }

    }
}