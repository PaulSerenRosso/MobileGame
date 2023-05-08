namespace Service.Fight
{
    public interface ITournamentService : IService
    {
        void SetupTournament(IGameService gameService, EnvironmentSO[] environmentSOs, EnemyGlobalSO[] enemyGlobalSOs);
        
        Fight GetCurrentFight();
        Fight[] GetFights();

    }
    public class Fight
    {
        public TournamentState _tournamentState;
        public EnvironmentSO _environmentSO;
        public EnemyGlobalSO _enemyGlobalSO;
        public FightState _fightState;

        public Fight(TournamentState tournamentState, EnvironmentSO environmentSO, EnemyGlobalSO enemyGlobalSO, FightState fightState)
        {
            _tournamentState = tournamentState;
            _environmentSO = environmentSO;
            _enemyGlobalSO = enemyGlobalSO;
            _fightState = fightState;
        }
    }

    public enum TournamentState
    {
        QUARTER,
        DEMI,
        FINAL
    }
    
    public enum FightState
    {
        VICTORY,
        DEFEAT
    }
    
}