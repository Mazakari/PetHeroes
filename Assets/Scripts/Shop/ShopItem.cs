using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShopItemType
{
    Skin
}

public class ShopItem : MonoBehaviour
{
    public ShopItemType Type { get; private set; }

    [SerializeField] private Image _itemImage;

    private GameObject _itemPrefab;
    public GameObject ItemPrefab => _itemPrefab;

    public GameObject ItemModel { get; set; }

    private int _unlockCost;
    [SerializeField] private TMP_Text _unlockCost_Text;
    [SerializeField] private TMP_Text _equiped_Text;
    
    [SerializeField] private Button _unlockButton;
    [SerializeField] private Button _equipButton;

    [SerializeField] private ShopItemSounds _itemSounds; 

    private ISkinsService _skinsService;
    private IShopService _shopService;
    private IMetaResourcesService _metaResourcesService;

    [HideInInspector] public bool isEquipped = false;
    [HideInInspector] public bool isLocked = false;

    public static event Action<ShopItem> OnShopItemEquipped;
    public static event Action<int> OnShopItemBuy;

    private void OnEnable()
    {
        _skinsService = AllServices.Container.Single<ISkinsService>();
        _shopService = AllServices.Container.Single<IShopService>();
        _metaResourcesService = AllServices.Container.Single<IMetaResourcesService>();

        _unlockButton.onClick.AddListener(Unlock);
        _equipButton.onClick.AddListener(Equip);
    }

    private void OnDisable()
    {
        _unlockButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
    }

    public void InitSkinItem(ShopItemSkinDataSO itemDataSO)
    {
        Type = itemDataSO.Type;
        _itemImage.sprite = itemDataSO.Sprite;
        _itemPrefab = itemDataSO.SkinPrefab;
        _unlockCost = itemDataSO.UnlockCost;
        _unlockCost_Text.text = _unlockCost.ToString();
        isLocked = itemDataSO.IsLocked;

        SpawnItemModel();
        UpdateState();
    }
   
    public void Unequip()
    {
        if (!isLocked && isEquipped)
        {
            // Unequip item
            SwitchEquiped(false);
        }
    }

    public void UpdateState()
    {
        _unlockButton.gameObject.SetActive(true);
        _equiped_Text.gameObject.SetActive(false);

        if (!isLocked)
        {
            _unlockButton.gameObject.SetActive(false);

            if (isEquipped)
            {
                SetCurrentSkin();
                OnShopItemEquipped?.Invoke(this);

                _equiped_Text.gameObject.SetActive(true);
            }

            return;
        }
    }

    private void Unlock()
    {
        ShowItemModel();

        if (isLocked)
        {
            // if player enough money
            
            int money = _metaResourcesService.PlayerMoney;
            if(money >= _unlockCost)
            {
                // Deduct player money
                money -= _unlockCost;
                _metaResourcesService.PlayerMoney = money;

                // Send callback for PlayerMoney to updte money counter
                OnShopItemBuy?.Invoke(money);

                // Unlock item
                isLocked = false;
                _unlockButton.gameObject.SetActive(false);

                // Play unlock sound
                _itemSounds.PlayUnlockSound();

                return;
            }
        }

        Debug.Log("Not enough money");
    }

    private void Equip()
    {
        ShowItemModel();

        if (!isLocked)
        {
            if (Type == ShopItemType.Skin)
            {
                if (!isEquipped)
                {
                    // Set item as currently equipped
                    SetCurrentSkin();

                    _itemSounds.PlayEquipSound();

                    // Send event OnItemEquip? To Unequip currently equipped item
                    OnShopItemEquipped?.Invoke(this);

                    SwitchEquiped(true);
                    UpdateState();
                }
            }
        }
    }

    private void SetCurrentSkin() => 
        _skinsService.SetCurrentSkinPrefab(_itemPrefab);

    private void SwitchEquiped(bool isEquiped) =>
        isEquipped = isEquiped;

    private void SpawnItemModel()
    {
        if (_itemPrefab != null)
        {
            ItemModel = Instantiate(_itemPrefab);
            if (ItemModel != null)
            {
                DeactivateUnnecessaryScripts();
                SetModelRigidbodyToKinematic();

                ItemModel.SetActive(false);
            }
        }
    }

    private void DeactivateUnnecessaryScripts() => 
        DeactivatePlayerInput();

    private void DeactivatePlayerInput()
    {
        if (ItemModel.TryGetComponent(out LaunchPlayer input))
        {
            input.enabled = false;
        }
    }

    private void SetModelRigidbodyToKinematic()
    {
        if (ItemModel.TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.isKinematic = true;
        }
    }

    private void ShowItemModel() =>
       _shopService.ShowItemModelByName(Type, ItemModel.name);

}
