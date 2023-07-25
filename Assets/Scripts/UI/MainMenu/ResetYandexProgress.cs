using UnityEngine;

public class ResetYandexProgress : MonoBehaviour
{
    private IPersistentProgressService _progressService;
    private IYandexService _yandexService;
    private ISkinsService _skinService;

    private void OnEnable()
    {
        _progressService = AllServices.Container.Single<IPersistentProgressService>();
        _yandexService = AllServices.Container.Single<IYandexService>();
        _skinService = AllServices.Container.Single<ISkinsService>();
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
       
        return new(
            initialMoney: Constants.NEW_PROGRESS_PLAYER_MONEY_AMOUNT,
            initialLevel: Constants.NEW_PROGRESS_FIRST_LEVEL_SCENE_NAME,
            _skinService.DefaultSkinPrefab);
    }
}
