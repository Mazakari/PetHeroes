using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour, ISavedProgress
{
    [Header("Level Selection Popup")]
    [Space(10)]
    [SerializeField] private GameObject _levelSelectionPopup;
    [SerializeField] private Transform _levelSelectionContent;

    [Header("Settings Popup")]
    [Space(10)]
    [SerializeField] private GameObject _settingsPopup;

    [Header("Developers Popup")]
    [Space(10)]
    [SerializeField] private GameObject _developersPopup;

    [Header("Yandex Progress Reset")]
    [Space(10)]
    [SerializeField] private Button _yandexProgressResetButton;

    private ILevelCellsService _levelCellsService;


    private void OnEnable()
    {
        InitYandexProgressResetButton();
        MainMenuState.OnAuthorizationPlayerProgressSynced += OverwriteLevelCellsData;

        _levelCellsService = AllServices.Container.Single<ILevelCellsService>();
        SettingsPopup.OnSettingsSaved += HideSettingsPopup;

        InitPopups();
    }
    private void Start() => 
        InitLevelsSelectionPopup();

    private void OnDisable()
    {
        SettingsPopup.OnSettingsSaved -= HideSettingsPopup;
        MainMenuState.OnAuthorizationPlayerProgressSynced -= OverwriteLevelCellsData;
    }

    public void ShowSelectLevelsPopup()
    {
        Debug.Log("ShowSelectLevelsPopup");
        _levelSelectionPopup.SetActive(true);
    }

    public void HideSelectLevelsPopup() => 
        _levelSelectionPopup.SetActive(false);

    public void ShowSettingsPopup() =>
        _settingsPopup.SetActive(true);

    public void ShowDevelopersPopup() =>
        _developersPopup.SetActive(true);

    public void HideDevelopersPopup() =>
       _developersPopup.SetActive(false);

    public void QuitGame() => 
        Application.Quit();

    private void HideSettingsPopup() =>
        _settingsPopup.SetActive(false);

    private void InitPopups()
    {
        _levelSelectionPopup.SetActive(false);
        _settingsPopup.SetActive(false);
        _developersPopup.SetActive(false);
    }

    private void InitLevelsSelectionPopup()
    {
        for (int i = 0; i < _levelCellsService.Levels.Length; i++)
        {
            _levelCellsService.Levels[i].transform.SetParent(_levelSelectionContent);
        }
    }

    private void InitYandexProgressResetButton()
    {
        _yandexProgressResetButton.gameObject.SetActive(false);
#if !UNITY_EDITOR
       _yandexProgressResetButton.gameObject.SetActive(true);
#endif
    }

    public void UpdateProgress(PlayerProgress progress) {}
    public void LoadProgress(PlayerProgress progress) => 
        OverwriteLevelCellsData(progress);

    private void OverwriteLevelCellsData(PlayerProgress progress)
    {
        int number;
        string name;
        bool locked;

        Sprite sprite;
        bool artifactLocked;

        if (progress.gameData.levels.Count > 0)
        {
            for (int i = 0; i < progress.gameData.levels.Count; i++)
            {
                number = progress.gameData.levels[i].number;
                name = progress.gameData.levels[i].sceneName;
                locked = progress.gameData.levels[i].locked;

                sprite = progress.gameData.levels[i].artifactSprite;
                artifactLocked = progress.gameData.levels[i].artifactLocked;


                _levelCellsService.Levels[i].InitLevelCell(number, name, locked, sprite, artifactLocked);
            }
        }
    }
}
