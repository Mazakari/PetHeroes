using UnityEngine;

public class LanguageService : ILanguageService
{
    public enum CurrentLanguage
    {
        Ru,
        En,
    }

    public CurrentLanguage Language { get; private set; }

    private SystemLanguage _language;

    public LanguageService()
    {
        SetSystemLanguage();
        Debug.Log($"System language = {_language}");
        Debug.Log($"CurrentLanguage = {Language}");

    }

    private void SetSystemLanguage()
    {
        _language = Application.systemLanguage;

        Language = _language switch
        {
            SystemLanguage.English => CurrentLanguage.En,
            SystemLanguage.Russian => CurrentLanguage.Ru,
            _ => CurrentLanguage.En,
        };
    }
}
