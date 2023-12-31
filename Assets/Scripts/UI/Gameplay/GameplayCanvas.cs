using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour, ISavedProgress
{
    [SerializeField] private LevelCompletePopup _LevelCompletePopup;
    [SerializeField] private GameObject _LevelLosePopup;

    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private CurrentLevelDisplay _levelDisplay;

    private ILevelCellsService _levelCellsService;
    
    private ILevelProgressService _levelProgressService;
    private ITimeService _timeService;


    public static Action OnNextLevel;
    public static Action OnRestartLevel;
    public static Action OnMainMenuButton;

    private string _nextLevelName;
    private int _currentLevelNumber;

    private void OnEnable()
    {
        _levelCellsService = AllServices.Container.Single<ILevelCellsService>();
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();
        _timeService = AllServices.Container.Single<ITimeService>();

        InitPopups();

        _levelProgressService.OnLevelWin += ShowLevelCompletePopup;
        _levelProgressService.OnGameOver += ShowLevelLosePopup;

        GameLoopState.OnNextLevelNameSet += UpdateNextLevel;
        GameLoopState.OnCurrentLevelSet += UpdateCurrentLevel;

        UpdateLevelDisplay();

    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveAllListeners();

        _levelProgressService.OnLevelWin -= ShowLevelCompletePopup;
        _levelProgressService.OnGameOver -= ShowLevelLosePopup;

        GameLoopState.OnNextLevelNameSet -= UpdateNextLevel;
        GameLoopState.OnCurrentLevelSet -= UpdateCurrentLevel;

        _timeService.ResumeGame();
    }
    private void InitPopups()
    {
        _LevelCompletePopup.gameObject.SetActive(false);
        _LevelLosePopup.SetActive(false);
    }

    private void ShowLevelCompletePopup()
    {
        _timeService.PauseGame();

        // TO DO Save completed Level

        _levelCellsService.UnlockNextLevel(_nextLevelName);

        _LevelCompletePopup.gameObject.SetActive(true);

        ShowYandexRateGamePopup();
    }

    private void ShowLevelLosePopup()
    {
        _timeService.PauseGame();
        _LevelLosePopup.SetActive(true);
    }

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

            targetLevelsData.Add(data);
        }
    }

    private void RewriteCurrentAndNextLevelsData(List<LevelCellsData> targetLevelsData) => 
        UpdateNextLevelData(_nextLevelName, targetLevelsData);
   
    private void UpdateNextLevelData(string nextLevelName, List<LevelCellsData> targetLevelsData)
    {
        LevelCellsData nextLevel = FindLevelDataInLevelCellService(nextLevelName);

        for (int i = 0; i < targetLevelsData.Count; i++)
        {
            if (targetLevelsData[i].sceneName.Equals(nextLevelName))
            {
                targetLevelsData[i] = nextLevel;
                return;
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
