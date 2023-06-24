
using UnityEngine;

public class Constants
{
    #region NEW PROGRESS DATA
    public const string PROGRESS_KEY = "ProgressKey";
    public const string INITIAL_SCENE_NAME = "Initial";
    public const string NEW_PROGRESS_FIRST_LEVEL_SCENE_NAME = "Level1";
    public const int NEW_PROGRESS_PLAYER_MONEY_AMOUNT = 0;
    #endregion

    #region SCENE NAMES
    public const string MAIN_MENU_SCENE_NAME = "MainMenu";
    public const string FIRST_LEVEL_NAME = "Level1";
    #endregion

    #region SCENE ASSETS
    public const string PLAYER_SPAWN_POINT_TAG = "PlayerSpawnPoint";
    #endregion

    #region PROGRESS DATA
    public static readonly string SAVE_DATA_FOLDER_PATH = $"{Application.dataPath}/Saves";
    public static readonly string SAVE_DATA_PATH = $"{SAVE_DATA_FOLDER_PATH}/save.txt";
    #endregion

    #region ASSETS PATHS
    public const string LEVELS_DATA_SO_PATH = "Prefabs/LevelSelection/LevelsDataSO";
    #endregion

    #region YANDEX DATA
    public const string SHOW_YANDEX_RATE_GAME_POPUP_LEVEL = "Level5";
    #endregion

    #region LOCALIZATION
    public const string LOCALIZARION_RU = "ru";
    public const string LOCALIZARION_EN = "en";
    public const string LOCALIZARION_TR = "tr";
    #endregion


}
