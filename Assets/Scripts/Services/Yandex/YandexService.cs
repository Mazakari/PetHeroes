public class YandexService : IYandexService
{
    public YandexAPI API { get; }

    public YandexService(YandexAPI yandexAPI, ITimeService timeService)
    {
        API = yandexAPI;
        API.Construct(timeService);
    }
}
