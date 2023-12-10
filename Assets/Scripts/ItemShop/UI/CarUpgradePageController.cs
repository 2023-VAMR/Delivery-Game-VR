using ItemShop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CarUpgradePageController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedUpPrice;
    [SerializeField] TextMeshProUGUI durabilityUpPrice;

    private static string shopDataAddress = "Assets/Data/ItemShop/ShopData.asset";
    private ShopData _shopData;

    public Action onUpgradeSpeed;
    public Action onUpgradeDurability;

    private void Awake()
    {

        Addressables.LoadAssetAsync<ShopData>(shopDataAddress).Completed
            += (handle) =>
            {
                _shopData = handle.Result;
                LoadData();
            };
    }

    private void OnEnable()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (_shopData is null) return;
        speedUpPrice.text = _shopData.speedUpgradePrice.ToString();
        durabilityUpPrice.text = _shopData.durabilityUpgradePrice.ToString();
    }

    public void OnClickUpgradeSpeed()
    {
        onUpgradeSpeed();
    }

    public void OnClickUpgradeDurability()
    {
        onUpgradeDurability();
    }
}
