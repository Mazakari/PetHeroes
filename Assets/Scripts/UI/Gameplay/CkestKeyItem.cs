using UnityEngine;
using UnityEngine.UI;

public class CkestKeyItem : MonoBehaviour
{
    [SerializeField] private KeyType _type;
    [SerializeField] private Image _keyImage;
    [SerializeField] private Image _keySilouhetteImage;

    public bool IsCollected { get; private set; } = false;
    public KeyType Type => _type;

    private void OnEnable() => 
        HideCollectedKeyImage();

    public void ShowKey()
    {
        IsCollected = true;
        SwitchKeyVisibilityState(IsCollected);
    }

    //private void InitItem()
    //{
    //    if (!IsCollected)
    //    {
    //        SwitchKeyVisibilityState(IsCollected);
    //        return;
    //    }

    //    SwitchKeyVisibilityState(IsCollected);
    //}

    private void InitItem()
    {
        IsCollected = false;
        SwitchKeyVisibilityState(IsCollected);
    }

    private void SwitchKeyVisibilityState(bool isKeyCollected)
    {
        if (_keySilouhetteImage!=null && _keyImage!=null)
        {
            _keySilouhetteImage.gameObject.SetActive(!isKeyCollected);
            _keyImage.gameObject.SetActive(isKeyCollected);
        }
    }

    private void HideCollectedKeyImage()
    {
        if (_keyImage)
        {
            InitItem();
        }
    }
}