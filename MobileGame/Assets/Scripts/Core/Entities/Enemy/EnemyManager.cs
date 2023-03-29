using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using Interfaces;
using Service;
using Service.Hype;
using UnityEngine;
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour, IDeathable, IDamageable, ILifeable, IUpdatable
{
    [SerializeField] private Tree.Tree _tree;
    [SerializeField] private EnemySO _so;
    private float _health;
    public EnemyEnums.EnemyMobilityState CurrentMobilityState;
    public EnemyEnums.EnemyBlockingState CurrentBlockingState;
    private List<EnemyStunTrigger> _currentStunTriggers;

    public event Action<float> OnDamageReceived;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _health = _so.Health;
        CurrentMobilityState = EnemyEnums.EnemyMobilityState.VULNERABLE;
        CurrentBlockingState = EnemyEnums.EnemyBlockingState.VULNERABLE;
        OnDamageReceived += TakeStun;
        UpdateManager.Register(this);
        _currentStunTriggers = new List<EnemyStunTrigger>();
    }

    private void OnDisable()
    {
        UpdateManager.UnRegister(this);
    }

    public void OnUpdate()
    {
        if (_currentStunTriggers.Count < 1 || CurrentMobilityState != EnemyEnums.EnemyMobilityState.VULNERABLE) return;
        _currentStunTriggers.RemoveAll(enemyStunTrigger => enemyStunTrigger.Time > _so.TimeStunAvailable);
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger => enemyStunTrigger.Time < _so.TimeStunAvailable))
        {
            enemyStunTrigger.Time += Time.deltaTime;
        }

        if (_currentStunTriggers.Any(enemyStunTrigger => (enemyStunTrigger.DamageAmount / _so.Health) >= _so.PercentageHealth))
        {
            CurrentMobilityState = EnemyEnums.EnemyMobilityState.STAGGER;
            _currentStunTriggers.Clear();
        }
    }

    public void Setup(Transform playerTransform, ITickeableService tickeableService,
        EnvironmentGridManager environmentGridManager, IPoolService poolService, IHypeService hypeService)
    {
        _tree.Setup(playerTransform, tickeableService, environmentGridManager, poolService, hypeService);
        // TODO: Add the right EnemySO in setup
    }

    public void RenderContainer()
    {
        
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
    
    public void TakeDamage(float amount)
    {
        OnDamageReceived.Invoke(amount);
        if (_health - amount <= 0)
        {
            _health = 0;
            Die();
        }
        else _health -= amount;
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
        foreach (var enemyStunTrigger in _currentStunTriggers.Where(enemyStunTrigger => enemyStunTrigger.Time < _so.TimeStunAvailable))
        {
            enemyStunTrigger.DamageAmount += amount;
        }
    }
}