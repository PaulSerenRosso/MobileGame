using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private GameObject _tauntParticle;
        [SerializeField] private GameObject _stunParticle;
        [SerializeField] private GameObject _movementParticle;
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

        private void ActivateMovementParticle(Vector2 obj)
        {
            _movementParticle.SetActive(true);
            _movementParticle.transform.forward = transform.TransformDirection(-new Vector3(obj.x,0 , obj.y));
        }
        
    

        private void DeactivateMovementParticle()
        {
            _movementParticle.SetActive(false);
        }
    }
}