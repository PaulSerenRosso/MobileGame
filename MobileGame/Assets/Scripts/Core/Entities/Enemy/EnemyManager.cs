using System;
using System.Collections.Generic;
using Enemy;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using Service;
using Service.Hype;
using UnityEngine;
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour, IUpdatable, IRemoteConfigurable
{
    public EnemyEnums.EnemyMobilityState CurrentMobilityState;
    public EnemyEnums.EnemyBlockingState CurrentBlockingState;

    [SerializeField] private Tree.Tree _tree;
    [SerializeField] private EnemySO _so;
    [SerializeField] private string _remoteConfigHealthName;
    [SerializeField] private string _remoteConfigTimeStunName;
    [SerializeField] private string _remoteConfigTimeStunInvulnerableName;
    [SerializeField] private string _remoteConfigStunPercentageHealthName;
    [SerializeField] private string _remoteConfigBlockPercentageDamageReduction;

    private List<EnemyStunTrigger> _currentStunTriggers;
    private float _timerInvulnerable;

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
        // if (EnemyEnums.EnemyMobilityState.INVULNERABLE == CurrentMobilityState) TimerInvulnerable();
        // if (_currentStunTriggers.Count < 1 || CurrentMobilityState != EnemyEnums.EnemyMobilityState.VULNERABLE) return;
        // _currentStunTriggers.RemoveAll(enemyStunTrigger => enemyStunTrigger.Time > _so.TimeStunAvailable);
        // foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
        //              enemyStunTrigger.Time < _so.TimeStunAvailable))
        // {
        //     enemyStunTrigger.Time += Time.deltaTime;
        // }
        //
        // if (_currentStunTriggers.Any(enemyStunTrigger =>
        //         (enemyStunTrigger.DamageAmount / _so.Health) >= _so.PercentageHealthStun))
        // {
        //     CurrentMobilityState = EnemyEnums.EnemyMobilityState.STAGGER;
        //     _currentStunTriggers.Clear();
        // }
    }

    public void Setup(Transform playerTransform, ITickeableService tickeableService,
        EnvironmentGridManager environmentGridManager, IPoolService poolService, IHypeService hypeService)
    {
        _tree.Setup(playerTransform, tickeableService, environmentGridManager, poolService, hypeService);
        _timerInvulnerable = 0;
    }

    private void TimerInvulnerable()
    {
        _timerInvulnerable += Time.deltaTime;
        if (_timerInvulnerable >= _so.TimeInvulnerable)
        {
            _currentStunTriggers.Clear();
            CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
            _timerInvulnerable = 0;
        }
    }

    private void TakeStun(float amount)
    {
        // if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.VULNERABLE) return;
        // _currentStunTriggers.Add(new EnemyStunTrigger(0, amount));
        // foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
        //              enemyStunTrigger.Time < _so.TimeStunAvailable))
        // {
        //     enemyStunTrigger.DamageAmount += amount;
        // }
    }

    public void SetRemoteConfigurableValues()
    {
        _so.PercentageHealthStun = RemoteConfigManager.Config.GetFloat(_remoteConfigStunPercentageHealthName);
        _so.TimeStunAvailable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunName);
        _so.TimeInvulnerable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunInvulnerableName);
        _so.PercentageDamageReduction =
            RemoteConfigManager.Config.GetFloat(_remoteConfigBlockPercentageDamageReduction);
    }
}