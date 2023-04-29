using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Enemy;
using Environment.MoveGrid;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.RemoteConfigs;
using Player;
using Service;
using Service.Hype;
using UnityEngine;
using UnityEngine.Serialization;
using Tree = BehaviorTree.Trees;

public class EnemyManager : MonoBehaviour, IUpdatable, IRemoteConfigurable, IHypeable
{
    public Action CanUltimateEvent;
    public Animator Animator;
    public bool IsBoosted;
    public EnemyEnums.EnemyMobilityState CurrentMobilityState;
    public EnemyEnums.EnemyBlockingState CurrentBlockingState;
    public EnemyInGameSO EnemyInGameSo;
    
    [SerializeField] private Tree.Tree _tree;

    [FormerlySerializedAs("_remoteConfigTimeStunName")] [SerializeField] private string _remoteConfigTimeStunAvailableName;
    [SerializeField] private string _remoteConfigTimeStunInvulnerableName;
    [SerializeField] private string _remoteConfigStunPercentageHealthName;
    [SerializeField] private string _remoteConfigBlockPercentageDamageReduction;
    [SerializeField] private string _remoteConfigAngleBlock;
    [SerializeField] private string _remoteConfigPercentageDamageReductionBoostChimist;
    [SerializeField] private ParticleSystem _ultimateParticle;
    [SerializeField] private GameObject _blockParticle;
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers;
    [SerializeField] private int _timeShaderActivate = 1500;

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
        GridManager gridManager, IPoolService poolService, IHypeService hypeService, PlayerRenderer playerRenderer)
    {
        _hypeService = hypeService;
        _tree.Setup(playerTransform, tickeableService, gridManager, poolService, hypeService, playerRenderer);
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
        EnemyInGameSo.PercentageDamageReductionBoostChimist =
            RemoteConfigManager.Config.GetFloat(_remoteConfigPercentageDamageReductionBoostChimist);
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

    public bool TryDecreaseHypeEnemy(float amount, Vector3 posToCheck, Transform particleTransform, Enums.ParticlePosition particlePosition)
    {
        float damage = amount;
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
                damage = (1 - EnemyInGameSo.PercentageDamageReduction) * amount;
                if (IsBoosted) damage = (1 - EnemyInGameSo.PercentageDamageReductionBoostChimist) * damage;
                if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.INVULNERABLE) TakeStun(damage);
                ActivateShader();
                _hypeService.DecreaseHypeEnemy(damage);
                return true;
            }

            return false;
        }

        if (IsBoosted) damage = (1 - EnemyInGameSo.PercentageDamageReductionBoostChimist) * amount;
        Vector3 pos;
        switch (particlePosition)
        {
            case Enums.ParticlePosition.Neck:
                pos = new Vector3(transform.position.x, transform.localScale.y + 0.5F, transform.position.z);
                particleTransform.position = pos;
                break;
            case Enums.ParticlePosition.Punch:
                particleTransform.position = (1 * Vector3.Normalize(posToCheck - transform.position) + transform.position) +
                                             new Vector3(0, 1, 0);
                break;
            case Enums.ParticlePosition.Uppercut:
                pos = (1 * Vector3.Normalize(posToCheck - transform.position) + transform.position) +
                      new Vector3(0, 1, 0);
                pos.y = transform.localScale.y + 0.5F;
                particleTransform.position = pos;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(particlePosition), particlePosition, null);
        }

        if (CurrentMobilityState != EnemyEnums.EnemyMobilityState.INVULNERABLE) TakeStun(amount);
        ActivateShader();
        _hypeService.DecreaseHypeEnemy(damage);
        return true;
    }

    private void ActivateShader()
    {
        foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
        {
            skinnedMeshRenderer.material.SetInt("_TakeDamage", 1);
        }

        DeactivateShader();
    }

    private async void DeactivateShader()
    {
        await UniTask.Delay(_timeShaderActivate);
        foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
        {
            skinnedMeshRenderer.material.SetInt("_TakeDamage", 0);
        }
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