using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private GameObject _tauntParticle;
        [SerializeField] private GameObject _stunParticle;
        [SerializeField] private GameObject _movementParticle;
        [SerializeField] private GameObject _invisibleWall;
        
        public void ActivateTauntFX()
        {
            _tauntParticle.gameObject.SetActive(true);
        }

        public void DeactivateTauntFX()
        {
            _tauntParticle.gameObject.SetActive(false);
        }

        public void ActivateStunFeedback()
        {
            _stunParticle.SetActive(true);
            AnimSetBool("IsStun", true);
        }
        
        public void DeactivateStunFeedback()
        {
            _stunParticle.SetActive(false);
            AnimSetBool("IsStun", false);
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

        private void ActivateInvisibleWall(Vector3 posToCheck)
        {
            _invisibleWall.gameObject.transform.position = (1 * (posToCheck - transform.position).normalized + transform.position);
            _invisibleWall.SetActive(true);
        }
    }
}