using Unity.VisualScripting;
using UnityEngine;

public class ChestKeyPanel : MonoBehaviour
{
    [SerializeField] private CkestKeyItem[] _keys;

    private void OnEnable() => 
        ChestKey.OnKeyPickup += PickupKey;

    private void OnDisable() => 
        ChestKey.OnKeyPickup -= PickupKey;

    public bool CheckKeysCollection()
    {
        bool keysCollected = true;

        for (int i = 0; i < _keys.Length; i++)
        {
            if (_keys[i].IsCollected == false)
            {
                keysCollected = false;
                return keysCollected;
            }
        }

        return keysCollected;
    }

    private void PickupKey(KeyType keyType)
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            if (_keys[i].Type == keyType)
            {
                _keys[i].ShowKey();
                return;
            }
        }
    }
}
