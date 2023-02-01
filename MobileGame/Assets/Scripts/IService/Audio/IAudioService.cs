namespace Service.AudioService
{
    public interface IAudioService : IService
    {
        void PlaySound(int id);
    }
}