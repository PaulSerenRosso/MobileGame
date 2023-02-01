namespace Service
{
    public interface ISwitchableService : IService
    {
        void EnabledService();
        void DisabledService();

        bool GetIsActiveService { get; }
    }
}