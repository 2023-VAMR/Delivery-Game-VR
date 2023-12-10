using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DeliveryOrderListPageController : MonoBehaviour
{
    [SerializeField] private Transform content;


    private readonly static string DeliveryButtonPrefabAddress = "Assets/Prefabs/UI/Delivery List Button.prefab";
    [SerializeField] private GameObject _deliveryButtonPrefab;

    void Awake()
    {
        LoadPrefab();
    }

    public void AddNewButton(DeliveryPoint food, DeliveryPoint dest, float limitTime, DeliveryListButton.OnStartOrderListener OnStartOrder)
    {
        if (_deliveryButtonPrefab is null) LoadPrefab();
        GameObject gObject = Instantiate(_deliveryButtonPrefab, content);
        DeliveryListButton button = gObject.GetComponent<DeliveryListButton>();

        button.SetButton(food, dest, limitTime, OnStartOrder);
    }

    private void LoadPrefab()
    {
        if (_deliveryButtonPrefab is not null) return;
        Addressables.LoadAssetAsync<GameObject>(DeliveryButtonPrefabAddress).Completed += (handle) => { _deliveryButtonPrefab = handle.Result; };
    }
}
