using System.Collections;
using HelperPSR.Pool;
using UnityEngine;

namespace Core.Entities
{
    public class GhostTrail : MonoBehaviour
    {
        [SerializeField] private GameObject _ghostGO;
        [SerializeField] private float _activeTime = 1f;
        [SerializeField] private float _meshRefreshRate = 0.1f;
        [SerializeField] private float _meshDisableDelay = 0.8f;

        private Pool<GameObject> _pool;
        private float _timer;

        public void InitGhostTrail()
        {
            _timer = _activeTime;
            _pool = new Pool<GameObject>(_ghostGO, 10);
        }

        public void ActivateTrail(Vector2 direction)
        {
            int directionToGo = -1;
            switch (direction)
            {
                case var v when v == Vector2.left:
                    directionToGo = 2;
                    break;
                case var v when v == Vector2.right:
                    directionToGo = 3;
                    break;
                case var v when v == Vector2.up:
                    directionToGo = 0;
                    break;
                case var v when v == Vector2.down:
                    directionToGo = 1;
                    break;
            }

            StartCoroutine(ActivateTrailCoroutine(directionToGo));
        }
        
        private IEnumerator ActivateTrailCoroutine(int direction)
        {
            while (_timer > 0)
            {
                _timer -= _meshRefreshRate;

                GameObject gObj = _pool.GetFromPool();
                gObj.transform.SetPositionAndRotation(transform.position, transform.rotation);
                GhostRenderer ghostRenderer = gObj.GetComponentInChildren<GhostRenderer>();
                ghostRenderer.AnimateMaterial(direction);
                StartCoroutine(_pool.AddToPoolLatter(gObj, _meshDisableDelay));
                yield return new WaitForSeconds(_meshRefreshRate);
            }

            _timer = _activeTime;
        }
    }
}
