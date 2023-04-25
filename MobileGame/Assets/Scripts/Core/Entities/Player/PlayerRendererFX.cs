using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private GameObject _tauntParticle;
        [SerializeField] private GameObject _stunParticle;

        public void ActivateTauntFX()
        {
            _tauntParticle.gameObject.SetActive(true);
        }

        public void DeactivateTauntFX()
        {
            _tauntParticle.gameObject.SetActive(false);
        }

        public void ActivateStunFX()
        {
            _stunParticle.SetActive(true);
        }
        
        public void DeactivateStunFX()
        {
            _stunParticle.SetActive(false);
        }
    }
}