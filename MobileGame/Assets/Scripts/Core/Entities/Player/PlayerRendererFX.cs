using UnityEngine;

namespace Player
{
    public partial class PlayerRenderer
    {
        [SerializeField] private GameObject _tauntParticle;
        [SerializeField] private GameObject _stunParticle;
        [SerializeField] private GameObject _movementParticle;
        [SerializeField] private ParticleSystem _invisibleWall;
        
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

        private void ActivateInvisibleWall(Vector3 dir)
        { 
            if(dir == Vector3.forward) return;
            _invisibleWall.transform.parent = this.transform;
            _invisibleWall.gameObject.SetActive(true);
            _invisibleWall.Play();
            var transformDirection = dir;
            _invisibleWall.gameObject.transform.localPosition = transformDirection+Vector3.up ;
            if(dir == Vector3.back) _invisibleWall.gameObject.transform.forward = transform.TransformDirection(transformDirection);
            else _invisibleWall.gameObject.transform.forward =-transform.TransformDirection(transformDirection);
            _invisibleWall.transform.parent = null;


        }
    }
}