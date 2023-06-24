using UnityEngine;

public class PlayerAuthorization : MonoBehaviour
{
    [SerializeField] private GameObject _authorizePlayer;
    [SerializeField] private GameObject _playerYandexID;
    [SerializeField] private PlayerYandexID _yandexID;

    private IYandexService _yandexService;

    private void OnEnable()
    {
        _yandexService = AllServices.Container.Single<IYandexService>();

#if !UNITY_EDITOR
       InitAuthDisplay();
       _yandexService.API.OnAuthorizedStatusResponse += ShowAuthState;
#endif
    }

    private void OnDisable()
    {
#if !UNITY_EDITOR
       _yandexService.API.OnAuthorizedStatusResponse -= ShowAuthState;
#endif
    }

    public void AuthorizeButton() => 
        _yandexService.API.Authorize();

    private void InitAuthDisplay()
    {
        _authorizePlayer.SetActive(true);
        _playerYandexID.SetActive(false);
    }

    private void ShowAuthState()
    {
        bool authorized = _yandexService.API.PlayerLoggedIn;

        _authorizePlayer.SetActive(!authorized);
        _playerYandexID.SetActive(authorized);

        GetPlayerDataAndUpdateYandexIDUI(authorized);
    }

    private void GetPlayerDataAndUpdateYandexIDUI(bool authorized)
    {
        if (authorized)
        {
            InitYandexPlayerID();
        }
    }

    private void InitYandexPlayerID()
    {
        _yandexService.API.GetPlayerData();

        string playerImageUrl = _yandexService.API.PlayerAvatarUrl;
        string playerName = _yandexService.API.PlayerIDName;

        Debug.Log($"PlayerAuthorization.InitYandexPlayerID");

        Debug.Log($"Avatar url = {_yandexService.API.PlayerAvatarUrl}");
        Debug.Log($"PlayerIDName = {_yandexService.API.PlayerIDName}");

        _yandexID.InitID(playerName, playerImageUrl);
    }
}
