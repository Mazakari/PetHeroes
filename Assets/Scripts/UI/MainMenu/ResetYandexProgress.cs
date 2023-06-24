using UnityEngine;

public class ResetYandexProgress : MonoBehaviour
{
    private IPersistentProgressService _progressService;
    private IYandexService _yandexService;

    private void OnEnable()
    {
        _progressService = AllServices.Container.Single<IPersistentProgressService>();
        _yandexService = AllServices.Container.Single<IYandexService>();

    }

    public void ResetProgress()
    {
        _progressService.Progress = NewProgress();
        string progress = _progressService.Progress.ToJson();
        _yandexService.API.SaveToYandex(progress);
    }

    private PlayerProgress NewProgress()
    {
        Debug.Log("ResetYandexProgress. Create new progress");
        return new(initialLevel: Constants.NEW_PROGRESS_FIRST_LEVEL_SCENE_NAME);
    }
}
