using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Shop : MonoBehaviour
{
    private UIManager UM;
    private ItemShop.PageController _pageController;

    private static string playerDataAddress = "Assets/Data/Player/PlayerData.asset";
    private PlayerData _playerData;

    private static string shopDataAddress = "Assets/Data/ItemShop/ShopData.asset";
    private ShopData _shopData;


    private void Awake()
    {
        _pageController = GetComponentInChildren<ItemShop.PageController>(true);
        Addressables.LoadAssetAsync<PlayerData>(playerDataAddress).Completed 
            += (handle) => 
            {
                _playerData = handle.Result; 
            };
        Addressables.LoadAssetAsync<ShopData>(shopDataAddress).Completed
        += (handle) =>
        {
            _shopData = handle.Result;
        };
        GetComponentInChildren<CarUpgradePageController>(true).onUpgradeSpeed = UpgradeSpeed;
        GetComponentInChildren<CarUpgradePageController>(true).onUpgradeDurability = UpgradeDurability;
        GetComponentInChildren<ItemShopPageController>(true).onBuyCarrot = BuyCarrot;
    }

    private void Start()
    {
        UM = UIManager.Instance;
        DisableShopUI();
    }

    public void EnableShopUI()
    {
        if (_pageController.gameObject.activeSelf) return;
        _pageController.gameObject.SetActive(true);
        UM.EnableUIInput();
    }

    public void DisableShopUI()
    {
        if (!_pageController.gameObject.activeSelf) return;
        _pageController.gameObject.SetActive(false);
        UM.DisableUIInput();
    }

    private void UpgradeSpeed()
    {
        if (_playerData.coin < _shopData.speedUpgradePrice) return;
        _playerData.speed += 10;
        _playerData.coin -= _shopData.speedUpgradePrice;
    }

    private void UpgradeDurability()
    {
        if (_playerData.coin < _shopData.durabilityUpgradePrice) return;
        _playerData.durability += 10;
        _playerData.coin -= _shopData.durabilityUpgradePrice;

    }

    private void BuyCarrot()
    {
        if (_playerData.coin < _shopData.carrotPrice) return;
        if (_playerData.maxCarrotNum <= _playerData.carrotNum) return;
        _playerData.carrotNum += 1;
        _playerData.coin -= _shopData.carrotPrice;
    }
}
