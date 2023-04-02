using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using Interfaces;
using Service;
using Service.Hype;
using UnityEngine;
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour, IDeathable, IDamageable, ILifeable, IUpdatable, IRemoteConfigurable
{
    public EnemyEnums.EnemyMobilityState CurrentMobilityState;
    public EnemyEnums.EnemyBlockingState CurrentBlockingState;

    [SerializeField] private Tree.Tree _tree;
    [SerializeField] private EnemySO _so;
    [SerializeField] private string _remoteConfigHealthName;
    [SerializeField] private string _remoteConfigTimeStunName;
    [SerializeField] private string _remoteConfigTimeStunInvulnerableName;
    [SerializeField] private string _remoteConfigStunPercentageHealthName;
    private float _health;
    private List<EnemyStunTrigger> _currentStunTriggers;
    private float _timerInvulnerable;

    public event Action<float> OnDamageReceived;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _health = _so.Health;
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
        CurrentBlockingState = EnemyEnums.EnemyBlockingState.VULNERABLE;
        OnDamageReceived += TakeStun;
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
        _currentStunTriggers.RemoveAll(enemyStunTrigger => enemyStunTrigger.Time > _so.TimeStunAvailable);
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
                     enemyStunTrigger.Time < _so.TimeStunAvailable))
        {
            enemyStunTrigger.Time += Time.deltaTime;
        }

        if (_currentStunTriggers.Any(enemyStunTrigger =>
                (enemyStunTrigger.DamageAmount / _so.Health) >= _so.PercentageHealth))
        {
            CurrentMobilityState = EnemyEnums.EnemyMobilityState.STAGGER;
            _currentStunTriggers.Clear();
        }
    }

    public void Setup(Transform playerTransform, ITickeableService tickeableService,
        EnvironmentGridManager environmentGridManager, IPoolService poolService, IHypeService hypeService)
    {
        _tree.Setup(playerTransform, tickeableService, environmentGridManager, poolService, hypeService);
        _timerInvulnerable = 0;
        // TODO: Add the right EnemySO in setup
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

    public void Die()
    {
        Debug.Log("BOSS IS KO, CONGRATS!");
    }

    [ContextMenu("TakeDamage")]
    public void TakeDamageTest()
    {
        OnDamageReceived.Invoke(5);
        if (_health - 5 <= 0)
        {
            _health = 0;
            Die();
        }
        else _health -= 5;
    }

    public void TakeDamage(float amount, Vector3 posToCheck = new())
    {
        if (CurrentBlockingState == EnemyEnums.EnemyBlockingState.BLOCKING)
        {
            float angle = Vector3.Angle(transform.forward, posToCheck);
            if (angle < 10) return;
            OnDamageReceived.Invoke(amount);
            if (_health - (1 - _so.PercentageDamageReduction) * amount <= 0)
            {
                _health = 0;
                Die();
            }
            else
            {
                _health -= (1 - _so.PercentageDamageReduction) * amount;
            }
            return;
        }

        OnDamageReceived.Invoke(amount);
        if (_health - amount <= 0)
        {
            _health = 0;
            Die();
        }
        else
        {
            _health -= amount;
        }
    }

    public event Action ChangeHealth;

    public float GetHealth()
    {
        return _health;
    }

    public void GainHealth(float amount)
    {
        if (_health >= _so.Health) return;
        if (_health + amount >= _so.Health) _health = _so.Health;
        else _health += amount;
    }

    private void TakeStun(float amount)
    {
        if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.VULNERABLE) return;
        _currentStunTriggers.Add(new EnemyStunTrigger(0, amount));
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger =>
                     enemyStunTrigger.Time < _so.TimeStunAvailable))
        {
            enemyStunTrigger.DamageAmount += amount;
        }
    }

    public void SetRemoteConfigurableValues()
    {
        _so.Health = RemoteConfigManager.Config.GetFloat(_remoteConfigHealthName);
        _so.PercentageHealth = RemoteConfigManager.Config.GetFloat(_remoteConfigStunPercentageHealthName);
        _so.TimeStunAvailable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunName);
        _so.TimeInvulnerable = RemoteConfigManager.Config.GetFloat(_remoteConfigTimeStunInvulnerableName);
    }
}