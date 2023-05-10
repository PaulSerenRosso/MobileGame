using System.Linq;

namespace Service.Fight
{
    public class TournamentService : ITournamentService
    {
        private EnvironmentSO[] _environmentSOs;
        private EnemyGlobalSO[] _enemyGlobalSOs;
        private Fight[] _fights = new Fight[3];
        private TournamentStep _playerCurrentStep;
        private bool _isSet;

        public void Setup(EnvironmentSO[] environmentSOs, EnemyGlobalSO[] enemyGlobalSOs)
        {
            _environmentSOs = environmentSOs;
            _enemyGlobalSOs = enemyGlobalSOs;
            SetTournament();
        }

        public void SetPlayerCurrentFight(TournamentStep tournamentStep)
        {
            _playerCurrentStep = tournamentStep;
        }

        public bool CompareState(FightState stateToCompare)
        {
            bool isFinish = false;
            foreach (var fight in _fights)
            {
                if (fight.FightState == stateToCompare) isFinish = true;
            }

            return isFinish;
        }

        public bool GetSet()
        {
            return _isSet;
        }

        public Fight GetFightStep(TournamentStep tournamentStep)
        {
            return _fights.First(fight => fight.TournamentStep == tournamentStep);
        }

        public Fight GetCurrentFightPlayer()
        {
            return _fights.First(fight => fight.TournamentStep == _playerCurrentStep);
        }

        public Fight[] GetFights()
        {
            return _fights;
        }

        private void SetTournament()
        {
            if (!_isSet)
            {
                _playerCurrentStep = TournamentStep.QUARTER; 
                Fight quarterFight = new Fight(TournamentStep.QUARTER, _environmentSOs[0], _enemyGlobalSOs[0],
                    FightState.WAITING);
                _fights[0] = quarterFight;
                Fight demiFight = new Fight(TournamentStep.DEMI, _environmentSOs[1], _enemyGlobalSOs[1],
                    FightState.WAITING);
                _fights[1] = demiFight;
                Fight finalFight = new Fight(TournamentStep.FINAL, _environmentSOs[0], _enemyGlobalSOs[2],
                    FightState.WAITING);
                _fights[2] = finalFight;
                _isSet = true;
            }
        }

        public void ResetTournament()
        {
            _isSet = false;
            SetTournament();
        }
    }
}