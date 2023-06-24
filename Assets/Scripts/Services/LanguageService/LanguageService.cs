using UnityEngine;

public class LanguageService : ILanguageService
{
    public CurrentLanguage Language { get; private set; }

    private string _language;

    private readonly IYandexService _yandexService;
    public enum CurrentLanguage
    {
        Ru,
        En,
        Tr
    }

    public LanguageService(IYandexService yandexService)
    {
        _yandexService = yandexService;

#if !UNITY_EDITOR
        SetSystemLanguage();
        Debug.Log($"System language = {_language}");
        Debug.Log($"CurrentLanguage = {Language}");
#endif
    }

    private void SetSystemLanguage()
    {
        _language = _yandexService.API.GetPlatformLanguage();

        Language = _language switch
        {
            Constants.LOCALIZARION_RU => CurrentLanguage.Ru,
            Constants.LOCALIZARION_EN => CurrentLanguage.En,
            Constants.LOCALIZARION_TR => CurrentLanguage.Tr,
            _ => CurrentLanguage.En,
        };
    }
}
