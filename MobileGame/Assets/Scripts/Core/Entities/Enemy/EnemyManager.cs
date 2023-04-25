using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using Service;
using Service.Hype;
using UnityEngine;
using UnityEngine.Serialization;
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour, IUpdatable, IRemoteConfigurable, IHypeable
{
    public Action CanUltimateEvent;
    public Animator Animator;
    public EnemyEnums.EnemyMobilityState CurrentMobilityState;
    public EnemyEnums.EnemyBlockingState CurrentBlockingState;
    public EnemyInGameSO EnemyInGameSo;

    [SerializeField] private Tree.Tree _tree;

    [FormerlySerializedAs("_remoteConfigTimeStunName")] [SerializeField] private string _remoteConfigTimeStunAvailableName;
    [SerializeField] private string _remoteConfigTimeStunInvulnerableName;
    [SerializeField] private string _remoteConfigStunPercentageHealthName;
    [SerializeField] private string _remoteConfigBlockPercentageDamageReduction;
    [SerializeField] private string _remoteConfigAngleBlock;
    [SerializeField] private ParticleSystem _ultimateParticle;
    [SerializeField] private GameObject _blockParticle;

    private List<EntityStunTrigger> _currentStunTriggers;
    private float _timerInvulnerable;
    private IHypeService _hypeService;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
        CurrentBlockingState = EnemyEnums.EnemyBlockingState.VULNERABLE;
        _currentStunTriggers = new List<EntityStunTrigger>();
    }

    private void OnEnable()
    {
        UpdateManager.Register(this);
        RemoteConfigManager.RegisterRemoteConfigurable(this);
    }

    private void OnDisable()
    {
        RemoteConfigManager.UnRegisterRemoteConfigurable(this);
        UpdateManager.UnRegister(this);
    }

    public void OnUpdate()
    {
        if (EnemyEnums.EnemyMobilityState.INVULNERABLE == CurrentMobilityState) TimerInvulnerable();
        if (_currentStunTriggers.Count < 1 || CurrentMobilityState != EnemyEnums.EnemyMobilityState.VULNERABLE) return;
        _currentStunTriggers.RemoveAll(enemyStunTrigger => enemyStunTrigger.Time > EnemyInGameSo.TimeStunAvailable);
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
                     enemyStunTrigger.Time < EnemyInGameSo.TimeStunAvailable))
        {
            enemyStunTrigger.Time += Time.deltaTime;
        }

        if (!_currentStunTriggers.Any(enemyStunTrigger =>
                (enemyStunTrigger.DamageAmount / _hypeService.GetMaximumHype()) >=
                EnemyInGameSo.PercentageHealthStun)) return;
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.STAGGER;
        _currentStunTriggers.Clear();
    }

    public void Setup(Transform playerTransform, ITickeableService tickeableService,
        GridManager gridManager, IPoolService poolService, IHypeService hypeService)
    {
        _hypeService = hypeService;
        _tree.Setup(playerTransform, tickeableService, gridManager, poolService, hypeService);
        _timerInvulnerable = 0;
        _hypeService.GetEnemyGainUltimateEvent += ActivateFXUltimate;
        _hypeService.GetEnemyLoseUltimateEvent += DeactivateFXUltimate;
    }

    private void ActivateFXUltimate(float obj)
    {
        _ultimateParticle.gameObject.SetActive(true);
    }

    private void DeactivateFXUltimate(float obj)
    {
        _ultimateParticle.gameObject.SetActive(false);
    }

    private void TimerInvulnerable()
    {
        _timerInvulnerable += Time.deltaTime;
        if (_timerInvulnerable >= EnemyInGameSo.TimeInvulnerable)
        {
            _currentStunTriggers.Clear();
            CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
            _timerInvulnerable = 0;
        }
    }

    public void SetRemoteConfigurableValues()
    {
        EnemyInGameSo.PercentageHealthStun = RemoteConfigManager.Config.GetFloat(_remoteConfigStunPercentageHealthName);
        EnemyInGameSo.TimeStunAvailable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunAvailableName);
        EnemyInGameSo.TimeInvulnerable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunInvulnerableName);
        EnemyInGameSo.PercentageDamageReduction =
            RemoteConfigManager.Config.GetFloat(_remoteConfigBlockPercentageDamageReduction);
        EnemyInGameSo.AngleBlock = RemoteConfigManager.Config.GetFloat(_remoteConfigAngleBlock);
    }

    public void ResetEnemy()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
        CurrentBlockingState = EnemyEnums.EnemyBlockingState.VULNERABLE;
        _hypeService.ResetHypeEnemy();
    }

    public void StopTree(Action callback)
    {
        ResetAnimatorParameters();
        _tree.StopTree();
        _tree.ResetTree(callback);
    }

    public void ReplayTree()
    {
        ResetAnimatorParameters();
        _tree.ReplayTree();
    }

    private void ResetAnimatorParameters()
    {
        Animator.SetInteger("Direction", -1);
        Animator.SetInteger("Attack", -1);
        Animator.SetBool("IsTaunting", false);
        Animator.SetBool("IsBlocking", false);
    }

    public bool TryDecreaseHypeEnemy(float amount, Vector3 posToCheck, Transform particleTransform)
    {
        if (CurrentBlockingState == EnemyEnums.EnemyBlockingState.BLOCKING)
        {
            _blockParticle.gameObject.transform.position =
                (1 * Vector3.Normalize(posToCheck - transform.position) + transform.position) + new Vector3(0, 1, 0);
            _blockParticle.gameObject.SetActive(true);
            Vector3 normalizedPos = (posToCheck - transform.position).normalized;
            float dot = Vector3.Dot(normalizedPos, transform.forward);
            float angle = Mathf.Acos(dot);
            if (angle > EnemyInGameSo.AngleBlock)
            {
                float damage = (1 - EnemyInGameSo.PercentageDamageReduction) * amount;
                if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.INVULNERABLE) TakeStun(damage);
                _hypeService.DecreaseHypeEnemy(damage);
                return true;
            }

            return false;
        }

        particleTransform.position = (1 * Vector3.Normalize(posToCheck - transform.position) + transform.position) +
                                     new Vector3(0, 1, 0);
        if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.INVULNERABLE) TakeStun(amount);
        _hypeService.DecreaseHypeEnemy(amount);
        return true;
    }

    private void TakeStun(float amount)
    {
        if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.VULNERABLE) return;
        _currentStunTriggers.Add(new EntityStunTrigger(0, amount));
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
                     enemyStunTrigger.Time < EnemyInGameSo.TimeStunAvailable))
        {
            enemyStunTrigger.DamageAmount += amount;
        }
    }
}