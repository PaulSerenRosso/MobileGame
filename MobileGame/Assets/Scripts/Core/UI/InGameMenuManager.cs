using Service;
using Service.Currency;
using Service.Fight;
using Service.Hype;
using Service.UI;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    [SerializeField] private InGameMenuHypeManager _inGameMenuHypeManager;
    [SerializeField] private InGameMenuRoundManager _inGameMenuRoundManager;
    [SerializeField] private InGameMenuEndFightManager _inGameMenuEndFightManager;
    [SerializeField] private Button _stopCinematicButton;
    public InGameMenuTutorialManager InGameMenuTutorialManager;

    private IFightService _fightService;

    public void SetupMenu(IFightService fightService, IHypeService hypeService, ITournamentService tournamentService,
        ICurrencyService currencyService, IGameService gameService)
    {
        _inGameMenuHypeManager.Init(hypeService, fightService);
        _inGameMenuRoundManager.Init(fightService);
        _inGameMenuEndFightManager.Init(fightService, currencyService, tournamentService, gameService);
        _fightService = fightService;
        if (_fightService.GetFightTutorial()) InGameMenuTutorialManager.Init(fightService);
        _fightService.ActivatePauseEvent += () => _stopCinematicButton.gameObject.SetActive(true);
        _fightService.DeactivatePauseEvent += () => _stopCinematicButton.gameObject.SetActive(false);
    }
    
    public void StopCinematic()
    {
        _fightService.StopCinematic();
    }
    
    public void QuitTutorial()
    {
        _fightService.QuitFightTutorial(true);
    }
}