public interface ILanguageService : IService
{
    LanguageService.CurrentLanguage Language { get; }
}