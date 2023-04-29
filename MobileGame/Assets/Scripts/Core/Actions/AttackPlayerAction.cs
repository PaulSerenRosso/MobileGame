using System;
using Cysharp.Threading.Tasks;
using HelperPSR.Pool;
using HelperPSR.Tick;
using Service.Hype;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Actions
{
    public class AttackPlayerAction : PlayerAction
    {
        public AttackActionSO AttackActionSo;
        public bool IsCancelTimeOn;
        public TickTimer AttackTimer;

        [SerializeField] private Material[] _materials;
        [SerializeField] private Renderer _meshRenderer;

        private bool _isAttacking;
        private int _comboCount;
        private IHypeService _hypeService;
        private IHypeable _hypeable;
        private Pool<GameObject>[] _hitPools;

        public event Action HitDamagedEvent;
        public event Action<HitSO> CancelAnimationEvent;

        public event Action<HitSO> MakeActionAnimationEvent;
        public event Action InitCancelAttackEvent;
        public event Action InitBeforeHitEvent;
        public event Action EndRecoveryEvent;
        public event Func<HitSO, bool> CheckCanDamageEvent;

        public override bool IsInAction
        {
            get => _isAttacking;
        }

        public override void MakeAction()
        {
            if (_comboCount != 0)
            {
                AttackTimer.Cancel();
                AttackTimer.ResetEvents();
            }

            IsCancelTimeOn = true;
            _isAttacking = true;
            AttackTimer.InitiateEvent += InitiateCancelTimer;
            AttackTimer.Initiate();
            MakeActionEvent?.Invoke();
            MakeActionAnimationEvent?.Invoke(AttackActionSo.HitsSO[_comboCount]);
        }

        public override void SetupAction(params object[] arguments)
        {
            AttackTimer = new TickTimer(0, (TickManager)arguments[0]);
            _hitPools = new Pool<GameObject>[AttackActionSo.HitsSO.Length];
            for (int i = 0; i < _hitPools.Length; i++)
            {
                _hitPools[i] = new Pool<GameObject>(AttackActionSo.HitsSO[i].Particle, 2);
            }

            _hypeable = (IHypeable)arguments[1];
            _hypeService = (IHypeService)arguments[2];
        }

        private void InitiateCancelTimer()
        {
            // _meshRenderer.material = _materials[0];
            AttackTimer.ResetEvents();
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].CancelTime;
            AttackTimer.TickEvent += InitiateBeforeHitTimer;
            AttackTimer.CancelEvent += BreakCombo;
            AttackTimer.CancelEvent +=()=>  CancelAnimationEvent?.Invoke(AttackActionSo.HitsSO[_comboCount]);
            AttackTimer.CancelEvent += AttackTimer.ResetCancelEvent;
            InitCancelAttackEvent?.Invoke();
        }

        private void BreakCombo()
        {
            // _meshRenderer.material = _materials[3];
            AttackTimer.ResetEvents();
            AttackTimer.Cancel();
            _comboCount = 0;
            _isAttacking = false;
            IsCancelTimeOn = false;
            EndActionEvent?.Invoke();
        }

        private void InitiateBeforeHitTimer()
        {
            // _meshRenderer.material = _materials[1];
            AttackTimer.ResetEvents();
            IsCancelTimeOn = false;
            InitBeforeHitEvent?.Invoke();
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].TimeBeforeHit;
            AttackTimer.TickEvent += InitiateRecoveryTimer;
            AttackTimer.Initiate();
        }

        private void InitiateRecoveryTimer()
        {
            // _meshRenderer.material = _materials[4];
            AttackTimer.ResetEvents();
            Hit();
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].RecoveryTime;
            AttackTimer.TickEvent += RaiseEndRecovery;
            if (_comboCount != AttackActionSo.HitsSO.Length - 1)
            {
                AttackTimer.TickEvent += InitiateComboTimer;
            }
            else
            {
                AttackTimer.TickEvent += BreakCombo;
            }

            AttackTimer.Initiate();
        }

        private void RaiseEndRecovery()
        {
            EndRecoveryEvent?.Invoke();
            CancelAnimationEvent?.Invoke(AttackActionSo.HitsSO[_comboCount]);
            AttackTimer.TickEvent -= RaiseEndRecovery;
        }

        private void Hit()
        {
            if (CheckCanDamageEvent.Invoke(AttackActionSo.HitsSO[_comboCount]))
            {
                var hit = _hitPools[_comboCount].GetFromPool();
                _hitPools[_comboCount].AddToPoolLatter(hit, hit.GetComponent<ParticleSystem>().main.duration);
                if (_hypeable.TryDecreaseHypeEnemy(AttackActionSo.HitsSO[_comboCount].HypeAmount, transform.position, hit.transform, AttackActionSo.HitsSO[_comboCount].ParticlePosition))
                {
                    HitDamagedEvent?.Invoke();
                }
            }
        }

        private void InitiateComboTimer()
        {
            // _meshRenderer.material = _materials[2];
            AttackTimer.ResetEvents();
            _isAttacking = false;
            AttackTimer.Time = AttackActionSo.HitsSO[_comboCount].ComboTime;
            _comboCount++;
            AttackTimer.TickEvent += BreakCombo;
            AttackTimer.Initiate();
            EndActionEvent?.Invoke();
        }
    }
}