using UnityEngine;

public class RoomDrop : MonoBehaviour
{
    // TO DO Add items list with weights
    private IDropService _dropService;
    private Transform _dropSpawnPoint;

    private void OnEnable() => 
        InitDrop();

    private void InitDrop()
    {
        _dropService = AllServices.Container.Single<IDropService>();
        _dropSpawnPoint = gameObject.transform;
    }

    public void DropItem()
    {
        // TO DO Roll item weight and choose which to drop

        int rnd = Random.Range(0, 100);
        if (rnd < 70)
        {
            return;
        }
        else if(rnd > 70 && rnd < 90)
        {
            _dropService.ActivateMoneyDropable(_dropSpawnPoint);
            return;
        }
        else if(rnd > 90)
        {
            _dropService.ActivateFireDropable(_dropSpawnPoint);
            return;
        }
    }
}
