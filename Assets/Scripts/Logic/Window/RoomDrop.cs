using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomDrop : MonoBehaviour
{
    [SerializeField] private DropTable_SO _dropTable;

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
        DropType type = GetDrop();

        switch (type)
        {
            case DropType.Empty:
                break;

            case DropType.Money:
                _dropService.ActivateMoneyDropable(_dropSpawnPoint);
                break;

            case DropType.Fire:
                _dropService.ActivateFireDropable(_dropSpawnPoint);
                break;

            default:
                Debug.LogError("DropType not found");
                break;
        }
    }

    private DropType GetDrop()
    {
        int roll = Random.Range(0, 100);
        for (int i = 0; i < _dropTable.table.Count; i++)
        {
            roll -= _dropTable.table[i].probability;
            if (roll < 0)
            {
                return _dropTable.table[i].drop;
            }
        }
        return _dropTable.table[0].drop;
    }
}
