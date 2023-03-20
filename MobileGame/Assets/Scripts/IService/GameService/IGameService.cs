namespace Service
{
    public interface IGameService : IService
    {
        public GlobalSettingsGameSO GlobalSettingsSO  {get;}
    }
}