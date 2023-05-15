using System;
using System.Linq;
using UnityEngine;
using HelperPSR;
using HelperPSR.Collections;

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
                int[] randIndex = new[] { 0, 1, 2 };
                System.Random random = new System.Random();
                CollectionHelper.ShuffleArray(ref random, randIndex);
                _playerCurrentStep = TournamentStep.QUARTER; 
                Fight quarterFight = new Fight(TournamentStep.QUARTER, _environmentSOs[randIndex[0]], _enemyGlobalSOs[randIndex[0]],
                    FightState.WAITING);
                _fights[0] = quarterFight;
                Fight demiFight = new Fight(TournamentStep.DEMI, _environmentSOs[randIndex[1]], _enemyGlobalSOs[randIndex[1]],
                    FightState.WAITING);
                _fights[1] = demiFight;
                Fight finalFight = new Fight(TournamentStep.FINAL, _environmentSOs[randIndex[2]], _enemyGlobalSOs[randIndex[2]],
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