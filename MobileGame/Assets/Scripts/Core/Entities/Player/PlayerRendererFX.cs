using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void ActivateTauntFX()
        {
            _particleSystem.gameObject.SetActive(true);
        }

        private void DeactivateTauntFX()
        {
            _particleSystem.gameObject.SetActive(false);
        }
    }
}