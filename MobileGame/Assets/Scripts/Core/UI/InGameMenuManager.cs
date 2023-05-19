using Service.Currency;
using Service.Fight;
using Service.Hype;
using Service.UI;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] private InGameMenuHypeManager _inGameMenuHypeManager;
    [SerializeField] private InGameMenuRoundManager _inGameMenuRoundManager;
    [SerializeField] private InGameMenuEndFightManager _inGameMenuEndFightManager;
    public InGameMenuTutorialManager InGameMenuTutorialManager;

    private IFightService _fightService;

    public void SetupMenu(IFightService fightService, IHypeService hypeService, ITournamentService tournamentService,
        ICurrencyService currencyService)
    {
        _inGameMenuHypeManager.Init(hypeService);
        _inGameMenuRoundManager.Init(fightService);
        _inGameMenuEndFightManager.Init(fightService, currencyService, tournamentService);
        _fightService = fightService;
        if (_fightService.GetFightTutorial()) InGameMenuTutorialManager.Init(fightService);
    }
    
    public void QuitTutorial()
    {
        _fightService.QuitFightTutorial(true);
    }
}