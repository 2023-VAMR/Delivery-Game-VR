using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ItemShopPageController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI carrotPrice;

    private static string shopDataAddress = "Assets/Data/ItemShop/ShopData.asset";
    private ShopData _shopData;

    public Action onBuyCarrot;

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
        carrotPrice.text = _shopData.carrotPrice.ToString();
    }

    public void OnClickBuyCarrot()
    {
        onBuyCarrot();
    }
}
