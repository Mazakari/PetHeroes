public interface ISaveLoadService : IService
{
    void SaveProgress();
    PlayerProgress LoadProgress(bool local);
}