using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;
    private readonly IYandexService _yandexService;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory, IYandexService yandexService)
    {
        _progressService = progressService;
        _gameFactory = gameFactory;
        _yandexService = yandexService;
    }

    public void SaveProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        {
            progressWriter.UpdateProgress(_progressService.Progress);
        }

        string progress = _progressService.Progress.ToJson();
        PlayerPrefs.SetString(Constants.PROGRESS_KEY, progress);

#if !UNITY_EDITOR
        Debug.Log("SaveLoadService.SaveProgress save to Yandex");
        SaveProgressToYandex(progress);
#endif
    }

    public PlayerProgress LoadProgress(bool local)
    {
        string progressString = GetLocalOrCloudProgressStrign(local); 

        return GetPlayerProgress(progressString);
    }

    private string GetLocalOrCloudProgressStrign(bool local)
    {
        string progressString;
        if (local)
        {
            progressString = PlayerPrefs.GetString(Constants.PROGRESS_KEY);
        }
        else
        {
            progressString = _yandexService.API.PlayerProgress;
        }

        return progressString;
    }

    private PlayerProgress GetPlayerProgress(string progressString) =>
        DeserializeJProgressJSON(progressString);

    private PlayerProgress DeserializeJProgressJSON(string progressString) =>
        progressString.ToDeserialized<PlayerProgress>();

    private void SaveProgressToYandex(string progress) => 
        _yandexService.API.SaveToYandex(progress);
}
