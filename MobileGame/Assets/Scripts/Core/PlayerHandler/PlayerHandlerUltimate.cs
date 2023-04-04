using System.Collections;
using System.Collections.Generic;
using Actions;
using HelperPSR.RemoteConfigs;
using HelperPSR.Tick;
using Player.Handler;
using Service.Hype;
using UnityEngine;

public class PlayerHandlerUltimate : PlayerHandlerRecordable, IRemoteConfigurable
{
    [SerializeField]
    private UltimatePlayerAction _ultimatePlayerAction;

    private float _timeBeforeDeactivateInputUltimate;
    private IHypeService _hypeService;
    private TickManager _tickManager;
    private TickTimer _timerBeforeDeactivateInputUltimate;

    [SerializeField]
    private TauntPlayerAction _tauntPlayerAction; 
    [SerializeField]
    private AttackPlayerAction _attackPlayerAction; 
    [SerializeField]
    private MovementPlayerAction _movementPlayerAction; 
   
    private bool canUltimate;
    protected override PlayerAction GetAction()
    {
        return _ultimatePlayerAction;
    }

    public override void InitializeAction()
    {
       
    }

    private bool CheckCanUltimate()
    {
        return canUltimate;
    }

    private bool CheckIsAttaking()
    {
        return _attackPlayerAction.IsInAction;
    }

    private bool CheckIsMoving()
    {
        return _movementPlayerAction.IsInAction;
    }
    
    private bool CheckIsTaunting()
    {
        return _tauntPlayerAction.IsInAction;
    }
    
    public override void Setup(params object[] arguments)
    {
       base.Setup(arguments);
       RemoteConfigManager.RegisterRemoteConfigurable(this);
       _hypeService =(IHypeService) arguments[0];
       _hypeService.ReachMaximumHypeEvent += ActivateInputUltimate;
       _tickManager = (TickManager)arguments[1];
       _timerBeforeDeactivateInputUltimate = new TickTimer(_timeBeforeDeactivateInputUltimate, _tickManager);
       _timerBeforeDeactivateInputUltimate.TickEvent += DeactivateInputUltimate;
       _ultimatePlayerAction.EndActionEvent += DeactivateInputUltimate;
       AddCondition(CheckCanUltimate);
       AddCondition(CheckIsMoving);
       AddCondition(CheckIsAttaking);
       AddCondition(CheckIsTaunting);
    }

    private void ActivateInputUltimate(float amount)
    {
        canUltimate = true; 
        _timerBeforeDeactivateInputUltimate.Initiate();
    }

    private void DeactivateInputUltimate()
    {
        canUltimate = false; 
        _timerBeforeDeactivateInputUltimate.Cancel();
    }


    public void SetRemoteConfigurableValues()
    {
        _timeBeforeDeactivateInputUltimate = RemoteConfigManager.Config.GetFloat("TimeBeforeDeactivateInputUltimate");
    }
}

