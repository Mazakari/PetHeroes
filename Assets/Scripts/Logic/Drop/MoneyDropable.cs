using UnityEngine;

public class MoneyDropable : MonoBehaviour, IDropable
{
    [SerializeField] private int _amonut = 10;

    private ILevelProgressService _levelProgressService;

    private void OnEnable() => 
        _levelProgressService = AllServices.Container.Single<LevelProgressService>();

    public void Drop()
    {
        // TO DO Get money from bonus pool
        // Drop from the drop point
    }

    public void Activate() => 
        _levelProgressService.AddScores(_amonut);
}
