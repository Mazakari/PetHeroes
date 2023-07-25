using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour, ISavedProgress
{
    [Header("Level Selection Popup")]
    [SerializeField] private GameObject _levelSelectionPopup;
    [SerializeField] private Transform _levelSelectionContent;

    [Space(10)]
    [Header("Shop Settings")]
    [SerializeField] private Button _shopButton;
    public static event Action OnShopButtonPress;

    [Space(10)]
    [Header("Settings Popup")]
    [SerializeField] private GameObject _settingsPopup;

    [Space(10)]
    [Header("Developers Popup")]
    [SerializeField] private GameObject _developersPopup;

    [Space(10)]
    [Header("Yandex Progress Reset")]
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
    public void LoadShop() =>
      OnShopButtonPress?.Invoke();

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
            ParentCellAndResetScale(i);
        }
    }

    private void ParentCellAndResetScale(int i)
    {
        LevelCell cell = _levelCellsService.Levels[i];
        cell.transform.SetParent(_levelSelectionContent);
        cell.GetComponent<RectTransform>().localScale = Vector3.one;
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
