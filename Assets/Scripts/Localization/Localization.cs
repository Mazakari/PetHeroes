using TMPro;
using UnityEngine;

public class Localization : MonoBehaviour
{
    [SerializeField] private TMP_Text _localizationText;

    [SerializeField] private string _russianText;
    [SerializeField] private string _englishText;
    [SerializeField] private string _turkishText;

    private ILanguageService _languageService;

    private void OnEnable()
    {
        _languageService = AllServices.Container.Single<ILanguageService>();

#if !UNITY_EDITOR
        SetLocalization();
#endif

    }

    private void SetLocalization()
    {
        if (_localizationText != null)
        {
            Debug.Log("SetLocalization");
            _localizationText.text = _languageService.Language switch
            {
                LanguageService.CurrentLanguage.Ru => _russianText,
                LanguageService.CurrentLanguage.En => _englishText,
                LanguageService.CurrentLanguage.Tr => _turkishText,
                _ => _englishText,
            };
        }
    }
}
