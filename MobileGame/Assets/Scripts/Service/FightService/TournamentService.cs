using System.Linq;

namespace Service.Fight
{
    public class TournamentService : ITournamentService
    {
        private EnvironmentSO[] _environmentSOs;
        private EnemyGlobalSO[] _enemyGlobalSOs;
        private Fight[] _fights = new Fight[3];
        private bool _isTournamentSet;

        public void SetupTournament(EnvironmentSO[] environmentSOs, EnemyGlobalSO[] enemyGlobalSOs)
        {
            _environmentSOs = environmentSOs;
            _enemyGlobalSOs = enemyGlobalSOs;
            SetTournament();
        }

        public bool GetStateTournament()
        {
            bool isFinish = true;
            foreach (var fight in _fights)
            {
                if (fight._fightState == FightState.DEFEAT) isFinish = false;
            }

            return isFinish;
        }

        public Fight GetCurrentFight()
        {
            return _fights.FirstOrDefault(fight => fight._fightState == FightState.DEFEAT);
        }

        public Fight[] GetFights()
        {
            return _fights;
        }

        private void SetTournament()
        {
            if (!_isTournamentSet)
            {
                Fight quarterFight = new Fight(TournamentState.QUARTER, _environmentSOs[0], _enemyGlobalSOs[0],
                    FightState.DEFEAT);
                _fights[0] = quarterFight;
                Fight demiFight = new Fight(TournamentState.DEMI, _environmentSOs[1], _enemyGlobalSOs[1],
                    FightState.DEFEAT);
                _fights[1] = demiFight;
                Fight finalFight = new Fight(TournamentState.FINAL, _environmentSOs[0], _enemyGlobalSOs[2],
                    FightState.DEFEAT);
                _fights[2] = finalFight;
                _isTournamentSet = true;
            }
        }

        public void ResetTournament()
        {
            _isTournamentSet = false;
            SetTournament();
        }
    }
}