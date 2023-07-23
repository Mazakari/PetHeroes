using System.Collections.Generic;
using UnityEngine;

public class DropService : IDropService
{
    public List<GameObject> MoneyDropablesPool { get; private set; }
    public List<GameObject> FireDropablesPool { get; private set; }

    private int _poolSize = 8;

    private const string DROPABLES_PARENT_NAME = "--Dropables--";
    private GameObject _dropablesParent;

    public void InitDropablesPools()
    {
        _dropablesParent = new GameObject(DROPABLES_PARENT_NAME);
        _dropablesParent.transform.position = Vector3.zero;

        MoneyDropablesPool = new List<GameObject>();
        FireDropablesPool = new List<GameObject>();

        PopulatePool(MoneyDropablesPool, AssetPath.MONEY_DROPABLE_PREFAB_PATH, _dropablesParent.transform);
        PopulatePool(FireDropablesPool, AssetPath.FIRE_DROPABLE_PREFAB_PATH, _dropablesParent.transform);
    }

    public void ActivateMoneyDropable(Transform at)
    {
        while (!FreeDropable(MoneyDropablesPool))
        {
            PopulatePool(MoneyDropablesPool, AssetPath.MONEY_DROPABLE_PREFAB_PATH, _dropablesParent.transform);
        }

        foreach (GameObject item in MoneyDropablesPool)
        {
            if (!item.activeSelf)
            {
                item.transform.position = at.position;
                if (item.TryGetComponent(out IDropableItem dropable))
                {
                    dropable.ActivateDropable();
                }
                return;
            }
        }
    }
    public void ActivateFireDropable(Transform at)
    {
        while (!FreeDropable(FireDropablesPool))
        {
            PopulatePool(FireDropablesPool, AssetPath.FIRE_DROPABLE_PREFAB_PATH, _dropablesParent.transform);
        }

        foreach (GameObject item in FireDropablesPool)
        {
            if (!item.activeSelf)
            {
                item.transform.position = at.position;
                if (item.TryGetComponent(out IDropableItem dropable))
                {
                    dropable.ActivateDropable();
                }
                return;
            }
        }
    }

    public void PoolDroppable(GameObject dropable)
    {
        if (dropable.TryGetComponent(out IDropableItem item))
        {
            item.ResetDropable();
        }
    }

    private bool FreeDropable(List<GameObject> dropables)
    {
        bool freeLeft = false;

        foreach (GameObject item in dropables)
        {
            if (!item.activeSelf)
            {
                freeLeft = true;
                return freeLeft;
            }
        }

        return freeLeft;
    }

    private void PopulatePool(List<GameObject> dropables, string prefabPath, Transform parent)
    {
        if (dropables != null) 
        {
            GameObject elementPrefab = Resources.Load<GameObject>(prefabPath);
            GameObject poolElement;

            for (int i = 0; i < _poolSize; i++)
            {
                poolElement = Object.Instantiate(elementPrefab, parent);
                dropables.Add(poolElement);

                if (dropables[i].TryGetComponent(out IDropableItem item))
                {
                    item.ResetDropable();
                }
            }
        }
    }
}
