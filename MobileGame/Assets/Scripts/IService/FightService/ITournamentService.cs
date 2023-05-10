namespace Service.Fight
{
    public interface ITournamentService : IService
    {
        void Setup(EnvironmentSO[] environmentSOs, EnemyGlobalSO[] enemyGlobalSOs);
        void ResetTournament();
        void SetPlayerCurrentFight(TournamentStep tournamentStep);
        bool CompareState(FightState stateToCompare);
        bool GetSet();
        Fight GetFightStep(TournamentStep tournamentStep);
        Fight GetCurrentFightPlayer();
        Fight[] GetFights();
    }
    
    public class Fight
    {
        public TournamentStep TournamentStep;
        public EnvironmentSO EnvironmentSO;
        public EnemyGlobalSO EnemyGlobalSO;
        public FightState FightState;

        public Fight(TournamentStep tournamentStep, EnvironmentSO environmentSO, EnemyGlobalSO enemyGlobalSO, FightState fightState)
        {
            TournamentStep = tournamentStep;
            EnvironmentSO = environmentSO;
            EnemyGlobalSO = enemyGlobalSO;
            FightState = fightState;
        }
    }

    public enum TournamentStep
    {
        QUARTER,
        DEMI,
        FINAL
    }
    
    public enum FightState
    {
        VICTORY,
        DEFEAT,
        WAITING
    }
    
}