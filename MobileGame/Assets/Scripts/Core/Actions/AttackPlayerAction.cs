using HelperPSR.Pool;
using HelperPSR.Tick;
using UnityEngine;

namespace Actions
{
    public class AttackPlayerAction : PlayerAction
    {
        public TickTimer AttackTimer;
        public bool IsCancelTimeOn;
        public AttackActionSO AttackActionSo;

        [SerializeField] private Material[] _materials;
        [SerializeField] private MeshRenderer _meshRenderer;

        private Pool<GameObject>[] _hitPools;
        private bool _isAttacking;
        private int _comboCount;

        public override bool IsInAction
        {
            get => _isAttacking;
        }

        public override void MakeAction()
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
            MakeActionEvent?.Invoke();
        } 

        public override void SetupAction(params object[] arguments)
        {
            AttackTimer = new TickTimer(0, (TickManager)arguments[0]);
            _hitPools = new Pool<GameObject>[AttackActionSo.HitsSO.Length];
            for (int i = 0; i < _hitPools.Length; i++)
            {
                _hitPools[i] = new Pool<GameObject>(AttackActionSo.HitsSO[i].Particle, 2);
            }
        }
        
        public event System.Action InitCancelAttackEvent;
        public event System.Action InitBeforeHitEvent;
        public event System.Action EndRecoveryEvent;

        private void InitiateCancelTimer()
        {
            Debug.Log("CancelTime");
            _meshRenderer.material = _materials[0];
            AttackTimer.ResetEvents();
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].CancelTime;
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
            EndActionEvent?.Invoke();
            IsCancelTimeOn = false;
        }

        private void InitiateBeforeHitTimer()
        {
            Debug.Log("BeforeHit");
            _meshRenderer.material = _materials[1];
            AttackTimer.ResetEvents();
            IsCancelTimeOn = false;
            InitBeforeHitEvent?.Invoke();
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].TimeBeforeHit;
            AttackTimer.TickEvent += InitiateRecoveryTimer;
            AttackTimer.Initiate();
        }


        private void InitiateRecoveryTimer()
        {
            Debug.Log("RecoveryTime");
            AttackTimer.ResetEvents();
            Hit();
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].RecoveryTime;
            if (_comboCount != AttackActionSo.HitsSO.Length - 1)
            {
                AttackTimer.TickEvent += InitiateComboTimer;
            }
            else
            {
                AttackTimer.TickEvent += BreakCombo;
            }

            AttackTimer.TickEvent += RaiseEndRecovery;
            AttackTimer.Initiate();
        }

        private void RaiseEndRecovery()
        {
            EndRecoveryEvent?.Invoke();
            AttackTimer.TickEvent -= RaiseEndRecovery;
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
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].ComboTime;
            _comboCount++;
            EndActionEvent?.Invoke();
            AttackTimer.TickEvent += BreakCombo;
            AttackTimer.Initiate();
        }
    }
}