using UnityEngine;

public interface IDropService : IService
{
    void ActivateFireDropable(Transform at);
    void ActivateMoneyDropable(Transform at);
    void InitDropablesPools();
    void PoolDroppable(GameObject dropable);
}