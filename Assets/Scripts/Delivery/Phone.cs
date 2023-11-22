using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Phone : MonoBehaviour
{
    [SerializeField] private Transform content;
    private readonly static string DeliveryButtonPrefabAddress = "Assets/Prefabs/UI/Delivery List Button.prefab";
    private GameObject _deliveryButtonPrefab;

    private void Start()
    {
        Addressables.LoadAssetAsync<GameObject>(DeliveryButtonPrefabAddress).Completed += (handle) => { _deliveryButtonPrefab = handle.Result; };
    }

    public void AddNewButton(DeliveryPoint food, DeliveryPoint dest, float limitTime)
    {
        GameObject gObject = Instantiate(_deliveryButtonPrefab, content);
        DeliveryListButton button = gObject.GetComponent<DeliveryListButton>();
        button.SetButton(food, dest, limitTime);

    }
}
