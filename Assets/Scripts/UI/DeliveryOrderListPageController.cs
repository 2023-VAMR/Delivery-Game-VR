using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DeliveryOrderListPageController : MonoBehaviour
{
    [SerializeField] private Transform content;


    private readonly static string DeliveryButtonPrefabAddress = "Assets/Prefabs/UI/Delivery List Button.prefab";
    private GameObject _deliveryButtonPrefab;

    void Awake()
    {
        Addressables.LoadAssetAsync<GameObject>(DeliveryButtonPrefabAddress).Completed += (handle) => { _deliveryButtonPrefab = handle.Result; };
    }

    public void AddNewButton(DeliveryPoint food, DeliveryPoint dest, float limitTime, DeliveryListButton.OnStartOrderListener OnStartOrder)
    {
        GameObject gObject = Instantiate(_deliveryButtonPrefab, content);
        DeliveryListButton button = gObject.GetComponent<DeliveryListButton>();

        button.SetButton(food, dest, limitTime, OnStartOrder);
    }
}
