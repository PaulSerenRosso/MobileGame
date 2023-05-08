using System.Linq;

namespace Service.Fight
{
    public class TournamentService : ITournamentService
    {
        private IGameService _gameService;
        private EnvironmentSO[] _environmentSOs;
        private EnemyGlobalSO[] _enemyGlobalSOs;
        private Fight[] _fights = new Fight[3];
        private bool _isTournamentSet;

        public void SetupTournament(IGameService gameService, EnvironmentSO[] environmentSOs, EnemyGlobalSO[] enemyGlobalSOs)
        {
            if (!_isTournamentSet)
            {
                _gameService = gameService;
                _environmentSOs = environmentSOs;
                _enemyGlobalSOs = enemyGlobalSOs;
                Fight quarterFight = new Fight(TournamentState.QUARTER, environmentSOs[0], enemyGlobalSOs[0],
                    FightState.DEFEAT);
                _fights[0] = quarterFight;
                Fight demiFight = new Fight(TournamentState.DEMI, environmentSOs[1], enemyGlobalSOs[1],
                    FightState.DEFEAT);
                _fights[1] = demiFight;
                Fight finalFight = new Fight(TournamentState.FINAL, environmentSOs[0], enemyGlobalSOs[2],
                    FightState.DEFEAT);
                _fights[2] = finalFight;
                _isTournamentSet = true;
            }
        }

        public Fight GetCurrentFight()
        {
            return _fights.FirstOrDefault(fight => fight._fightState == FightState.DEFEAT);
        }

        public Fight[] GetFights()
        {
            return _fights;
        }
    }
}