using UnityEngine;

public class MoneyDropable : MonoBehaviour, IDropable
{
    [SerializeField] private int _amonut = 10;

    private ILevelProgressService _levelProgressService;

    [SerializeField] private PlatformCollision _platformCollision;

    private void OnEnable()
    {
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();
        _platformCollision.SetDropableReference(this);
    }

    public void Activate() => 
        _levelProgressService.AddScores(_amonut);
}
