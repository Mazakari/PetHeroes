using UnityEngine;

public class MoneyDropable : MonoBehaviour, IDropableItem
{
    [SerializeField] private int _amonut = 10;

    private ILevelProgressService _levelProgressService;

    [SerializeField] private PlatformCollision _platformCollision;

    private void OnEnable()
    {
        _levelProgressService = AllServices.Container.Single<ILevelProgressService>();
        _platformCollision.SetDropableReference(this);
    }

    public void ActivateDropable() => 
        _platformCollision.ActivateDropabable();

    public void ResetDropable() => 
        _platformCollision.ResetDropabable();

    public void Use() => 
        _levelProgressService.AddScores(_amonut);

}
