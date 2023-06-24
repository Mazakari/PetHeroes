using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameMetaData;

public class GameplayCanvas : MonoBehaviour, ISavedProgress
{
    [SerializeField] private LevelCompletePopup _LevelCompletePopup;
    [SerializeField] private Image _levelArtifactImage;

    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private CurrentLevelDisplay _levelDisplay;

    private ILevelCellsService _levelCellsService;
    private IYandexService _yandexService;

    public static Action OnNextLevel;
    public static Action OnRestartLevel;
    public static Action OnMainMenuButton;

    private string _nextLevelName;
    private int _currentLevelNumber;

    private void OnEnable()
    {
        _levelCellsService = AllServices.Container.Single<ILevelCellsService>();
        _yandexService = AllServices.Container.Single<IYandexService>();

        _LevelCompletePopup.gameObject.SetActive(false);

        LevelState.OnLevelResultShow += ShowLevelCompletePopup;
        GameLoopState.OnNextLevelNameSet += UpdateNextLevel;
        GameLoopState.OnCurrentLevelSet += UpdateCurrentLevel;

        UpdateLevelDisplay();
       
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveAllListeners();
        LevelState.OnLevelResultShow -= ShowLevelCompletePopup;
    }

    private void ShowLevelCompletePopup(bool artifactLocked)
    {
        SetArtifactImage();

        _levelCellsService.SaveCompletedLevel(artifactLocked);
        _levelCellsService.UnlockNextLevel(_nextLevelName);

        _LevelCompletePopup.ShowArtifact(artifactLocked);
        _LevelCompletePopup.gameObject.SetActive(true);

        ShowYandexRateGamePopup();
    }

    private void SetArtifactImage() => 
        _levelArtifactImage.sprite = _levelCellsService.Current.ArtifactSprite;

    // Send callback for GameLoopState
    public void LoadMainMenu() => 
        OnMainMenuButton?.Invoke();

    private void UpdateLevelDisplay() =>
        _levelDisplay.SetLevelNumber(_levelCellsService.Current.LevelNumber);

    private void UpdateNextLevel(string name) => 
        _nextLevelName = name;

    private void UpdateCurrentLevel(int newCurrentLevel) =>
       _currentLevelNumber = newCurrentLevel;

    public void UpdateProgress(PlayerProgress progress)
    {
#if !UNITY_EDITOR
        // Save yandex leaderboard
        _yandexService.API.SaveYandexLeaderboard(_currentLevelNumber);
#endif
        progress.gameData.nextLevel = _nextLevelName;
        CopyProgress(_levelCellsService.LevelsData, progress.gameData.levels);
    }

    public void LoadProgress(PlayerProgress progress) => 
        _nextLevelName = progress.gameData.nextLevel;


    private void CopyProgress(LevelCellsData[] source, List<LevelCellsData> targetLevelsData)
    {
        if (targetLevelsData.Count == 0)
        {
            FillProgressWithInitialLevelsData(source, targetLevelsData);
            return;
        }

        RewriteCurrentAndNextLevelsData(targetLevelsData);
    }
    private void FillProgressWithInitialLevelsData(LevelCellsData[] source, List<LevelCellsData> targetLevelsData)
    {
        for (int i = 0; i < source.Length; i++)
        {
            LevelCellsData data;
            data.number = source[i].number;
            data.locked = source[i].locked;
            data.sceneName = source[i].sceneName;

            data.artifactSprite = source[i].artifactSprite;
            data.artifactLocked = source[i].artifactLocked;

            targetLevelsData.Add(data);
        }
    }

    private void RewriteCurrentAndNextLevelsData(List<LevelCellsData> targetLevelsData)
    {
        UpdateCurrentLevelData(_levelCellsService.CurrentLevelName, targetLevelsData);
        UpdateNextLevelData(_nextLevelName, targetLevelsData);
    }
    private void UpdateCurrentLevelData(string completedLevelName, List<LevelCellsData> targetLevelsData)
    {
        LevelCellsData currentData = FindLevelDataInLevelCellService(completedLevelName);

        for (int i = 0; i < targetLevelsData.Count; i++)
        {
            if (targetLevelsData[i].sceneName.Equals(completedLevelName))
            {
                if (targetLevelsData[i].artifactLocked == false)
                {
                    currentData.artifactLocked = false;
                }

                targetLevelsData[i] = currentData;
            }
        }
    }
    private void UpdateNextLevelData(string nextLevelName, List<LevelCellsData> targetLevelsData)
    {
        LevelCellsData nextLevel = FindLevelDataInLevelCellService(nextLevelName);

        for (int i = 0; i < targetLevelsData.Count; i++)
        {
            if (targetLevelsData[i].sceneName.Equals(nextLevelName))
            {
                targetLevelsData[i] = nextLevel;
            }
        }
    }

    private LevelCellsData FindLevelDataInLevelCellService(string levelName)
    {
        LevelCellsData data = new();

        for (int i = 0; i < _levelCellsService.LevelsData.Length; i++)
        {
            if (_levelCellsService.LevelsData[i].sceneName.Equals(levelName))
            {
                data = _levelCellsService.LevelsData[i];
            }
        }

        return data;
    }

    private void ShowYandexRateGamePopup()
    {
        if (_levelCellsService.CurrentLevelName.Equals(Constants.SHOW_YANDEX_RATE_GAME_POPUP_LEVEL))
        {
            Debug.Log($"ShowYandexRateGamePopup at {Constants.SHOW_YANDEX_RATE_GAME_POPUP_LEVEL}");
            AllServices.Container.Single<IYandexService>().API.ShowRateGamePopup();
        }
    }
}
