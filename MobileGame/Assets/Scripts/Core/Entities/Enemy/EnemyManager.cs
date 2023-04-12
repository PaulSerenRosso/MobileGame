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
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour, IUpdatable, IRemoteConfigurable, IHypeable
{
    public EnemyEnums.EnemyMobilityState CurrentMobilityState;
    public EnemyEnums.EnemyBlockingState CurrentBlockingState;
    public EnemySO EnemySO;

    public Action CanUltimateEvent;

    [SerializeField] private Tree.Tree _tree;
    [SerializeField] private string _remoteConfigTimeStunName;
    [SerializeField] private string _remoteConfigTimeStunInvulnerableName;
    [SerializeField] private string _remoteConfigStunPercentageHealthName;
    [SerializeField] private string _remoteConfigBlockPercentageDamageReduction;
    [SerializeField] private string _remoteConfigAngleBlock;
    [SerializeField] private string _remoteConfigRounds;

    public Animator Animator;
    private List<EnemyStunTrigger> _currentStunTriggers;
    private float _timerInvulnerable;
    private IHypeService _hypeService;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
        CurrentBlockingState = EnemyEnums.EnemyBlockingState.VULNERABLE;
        UpdateManager.Register(this);
        RemoteConfigManager.RegisterRemoteConfigurable(this);
        _currentStunTriggers = new List<EnemyStunTrigger>();
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
        _currentStunTriggers.RemoveAll(enemyStunTrigger => enemyStunTrigger.Time > EnemySO.TimeStunAvailable);
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
                     enemyStunTrigger.Time < EnemySO.TimeStunAvailable))
        {
            enemyStunTrigger.Time += Time.deltaTime;
        }

        if (!_currentStunTriggers.Any(enemyStunTrigger =>
                (enemyStunTrigger.DamageAmount / _hypeService.GetMaximumHype()) >=
                EnemySO.PercentageHealthStun)) return;
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.STAGGER;
        _currentStunTriggers.Clear();
    }

    public void Setup(Transform playerTransform, ITickeableService tickeableService,
        EnvironmentGridManager environmentGridManager, IPoolService poolService, IHypeService hypeService)
    {
        _hypeService = hypeService;
        _tree.Setup(playerTransform, tickeableService, environmentGridManager, poolService, hypeService);
        _timerInvulnerable = 0;
    }

    private void TimerInvulnerable()
    {
        _timerInvulnerable += Time.deltaTime;
        if (_timerInvulnerable >= EnemySO.TimeInvulnerable)
        {
            _currentStunTriggers.Clear();
            CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
            _timerInvulnerable = 0;
        }
    }

    private void TakeStun(float amount)
    {
        if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.VULNERABLE) return;
        _currentStunTriggers.Add(new EnemyStunTrigger(0, amount));
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
                     enemyStunTrigger.Time < EnemySO.TimeStunAvailable))
        {
            enemyStunTrigger.DamageAmount += amount;
        }
    }

    public void SetRemoteConfigurableValues()
    {
        EnemySO.PercentageHealthStun = RemoteConfigManager.Config.GetFloat(_remoteConfigStunPercentageHealthName);
        EnemySO.TimeStunAvailable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunName);
        EnemySO.TimeInvulnerable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunInvulnerableName);
        EnemySO.PercentageDamageReduction =
            RemoteConfigManager.Config.GetFloat(_remoteConfigBlockPercentageDamageReduction);
        EnemySO.AngleBlock = RemoteConfigManager.Config.GetFloat(_remoteConfigAngleBlock);
        EnemySO.Rounds = RemoteConfigManager.Config.GetInt(_remoteConfigRounds);
    }

    public void ResetEnemy()
    {
        transform.position = new Vector3(0, 0, 0);
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
        CurrentBlockingState = EnemyEnums.EnemyBlockingState.VULNERABLE;
        _hypeService.ResetHypeEnemy();
    }

    public void StopTree()
    {
        _tree.StopTree();
    }

    public void ResetTree()
    {
        _tree.ResetTree();
    }

    public void DecreaseHypeEnemy(float amount, Vector3 posToCheck)
    {
        if (CurrentBlockingState == EnemyEnums.EnemyBlockingState.BLOCKING)
        {
            float damage = (1 - EnemySO.PercentageDamageReduction) * amount;
            float angle = Mathf.Abs(Vector3.Angle(transform.forward, posToCheck));
            if (angle > EnemySO.AngleBlock / 2)
            {
                _hypeService.DecreaseHypeEnemy(damage);
                if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.INVULNERABLE) TakeStun(damage);
            }
            return;
        }

        if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.INVULNERABLE) TakeStun(amount);
        _hypeService.DecreaseHypeEnemy(amount);
    }
}