using HelperPSR.Pool;
using HelperPSR.Tick;
using UnityEngine;

namespace Action
{
    public class AttackAction : MonoBehaviour, IAction
    {
        public TickTimer AttackTimer;
        public bool IsCancelTimeOn;

        [SerializeField] private AttackSO _attackSO;
        [SerializeField] private Material[] _materials;
        [SerializeField] private MeshRenderer _meshRenderer;

        private Pool<GameObject>[] _hitPools;
        private bool _isAttacking;
        private int _comboCount;

        public bool IsInAction
        {
            get => _isAttacking;
        }

        public void MakeAction()
        {
            Debug.Log("MakeAction Hit");
            if (_comboCount != 0)
            {
                Debug.Log("MakeAction !=0");
                AttackTimer.Cancel();
                AttackTimer.ResetEvents();
            }

            IsCancelTimeOn = true;
            _isAttacking = true;
            AttackTimer.InitiateEvent += InitiateCancelTimer;
            AttackTimer.Initiate();
        }

        public void SetupAction(params object[] arguments)
        {
            AttackTimer = new TickTimer(0, (TickManager)arguments[0]);
            _hitPools = new Pool<GameObject>[_attackSO.HitsSO.Length];
            for (int i = 0; i < _hitPools.Length; i++)
            {
                _hitPools[i] = new Pool<GameObject>(_attackSO.HitsSO[i].Particle, 2);
            }
        }

        public event System.Action MakeActionEvent;
        public event System.Action InitCancelAttackEvent;
        public event System.Action InitBeforeHitEvent;

        private void InitiateCancelTimer()
        {
            Debug.Log("CancelTime");
            _meshRenderer.material = _materials[0];
            AttackTimer.ResetEvents();
            AttackTimer.Time = _attackSO.HitsSO[_comboCount].CancelTime;
            AttackTimer.TickEvent += InitiateBeforeHitTimer;
            AttackTimer.CancelEvent += BreakCombo;
            AttackTimer.CancelEvent += AttackTimer.ResetCancelEvent;
            InitCancelAttackEvent?.Invoke();
        }

        private void BreakCombo()
        {
            Debug.Log("BreakCombo");
            _meshRenderer.material = _materials[3];
            AttackTimer.ResetEvents();
            AttackTimer.Cancel();
            _comboCount = 0;
            _isAttacking = false;
            IsCancelTimeOn = false;
        }

        private void InitiateBeforeHitTimer()
        {
            Debug.Log("BeforeHit");
            _meshRenderer.material = _materials[1];
            AttackTimer.ResetEvents();
            IsCancelTimeOn = false;
            InitBeforeHitEvent?.Invoke();
            AttackTimer.Time = _attackSO.HitsSO[_comboCount].TimeBeforeHit;
            AttackTimer.TickEvent += InitiateRecoveryTimer;
            AttackTimer.Initiate();
        }

        private void InitiateRecoveryTimer()
        {
            Debug.Log("RecoveryTime");
            AttackTimer.ResetEvents();
            Hit();
            AttackTimer.Time = _attackSO.HitsSO[_comboCount].RecoveryTime;
            if (_comboCount != _attackSO.HitsSO.Length - 1)
            {
                AttackTimer.TickEvent += InitiateComboTimer;
            }
            else
            {
                AttackTimer.TickEvent += BreakCombo;
            }

            AttackTimer.Initiate();
        }

        private void Hit()
        {
            Debug.Log("Hit");
            var hit = _hitPools[_comboCount].GetFromPool();
            hit.transform.position = transform.position;
            hit.transform.forward = transform.forward;
            _hitPools[_comboCount].AddToPoolLatter(hit, hit.GetComponent<ParticleSystem>().main.duration);
        }

        private void InitiateComboTimer()
        {
            Debug.Log("ComboTime");
            _meshRenderer.material = _materials[2];
            AttackTimer.ResetEvents();
            _isAttacking = false;
            AttackTimer.Time = _attackSO.HitsSO[_comboCount].ComboTime;
            _comboCount++;
            AttackTimer.TickEvent += BreakCombo;
            AttackTimer.Initiate();
        }
    }
}