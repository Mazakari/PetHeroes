using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexAPI : MonoBehaviour
{
    public string PlayerIDName { get; private set; }
    public string PlayerAvatarUrl { get; private set; }
    public string PlayerProgress { get; private set; }
    public bool PlayerLoggedIn { get; private set; }


    [DllImport("__Internal")]
    private static extern void GetPlayerIDData();

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void SavePlayerDataToYandex(string playerData);

    [DllImport("__Internal")]
    private static extern void LoadPlayerDataFromYandex();

    public event Action OnYandexProgressCopied;

    [DllImport("__Internal")]
    private static extern void UpdateLeaderboardData(int newMaxLevel);

    [DllImport("__Internal")]
    private static extern string GetSystemLanguage();

    [DllImport("__Internal")]
    private static extern void ShowFullscrenAds();

    [DllImport("__Internal")]
    private static extern bool PlayerAuthorized();
    public event Action OnAuthorizedStatusResponse;

    [DllImport("__Internal")]
    private static extern void AuthorizePlayer();


    private ITimeService _timeService;

    private void Awake() =>
        DontDestroyOnLoad(this);

    public void Construct(ITimeService timeService) => 
        _timeService = timeService;

    public void CheckAuthorizedStatus()
    {
        PlayerLoggedIn = PlayerAuthorized();
        Debug.Log($"CheckAuthorizedStatus = {PlayerLoggedIn}");
        OnAuthorizedStatusResponse?.Invoke();
    }

    public void Authorize() => 
        AuthorizePlayer();

    public void GetPlayerData() => 
        GetPlayerIDData();

    public void ShowRateGamePopup()
    {
        if (!PlayerLoggedIn)
        {
            Debug.Log("YandexApi ShowRateGamePopup Player Not Authorized");
            return;
        }

        RateGame();
    }

    public void SetPlayerIDName(string name) =>
        PlayerIDName = name;

    public void SetPlayerIDAvatar(string url) =>
        PlayerAvatarUrl = url;

    public void SaveToYandex(string progress)
    {
        Debug.Log($"SaveToYandex.progress = {progress}");
        SavePlayerDataToYandex(progress);
    }

    public void LoadFromYandex() =>
        LoadPlayerDataFromYandex();

    public void CopyYandexProgress(string progress)
    {
        PlayerProgress = progress;

        OnYandexProgressCopied?.Invoke();
    }

    public void SaveYandexLeaderboard(int newMaxLevel)
    {
        if (!PlayerLoggedIn)
        {
            Debug.Log("YandexApi SaveYandexLeaderboard Player Not Authorized");
            return;
        }

        Debug.Log($"Sending new max level {newMaxLevel} to Yandex leaderboard");
        UpdateLeaderboardData(newMaxLevel);
    }

    public string GetPlatformLanguage() =>
        GetSystemLanguage();

    public void ShowYandexInterstitial() =>
        ShowFullscrenAds();

    public void PauseGame() =>
        _timeService.PauseGame();

    public void UnPauseGame() =>
        _timeService.ResumeGame();

    public void LoadYandexProgressAfterAuthorization()
    {
        if (PlayerLoggedIn)
        {
            // Copy save to _yandexService.API.PlayerProgress and await for OnYandexProgressCopied callback for MainMenuState
            LoadFromYandex();
        }
    }
}
