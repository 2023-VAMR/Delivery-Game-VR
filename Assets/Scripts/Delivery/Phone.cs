using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;


[RequireComponent(typeof(Rigidbody))]
public class Phone : MonoBehaviour
{
    [SerializeField] private Transform content;

    

    private BoxCollider _collider;

    private readonly static string DeliveryButtonPrefabAddress = "Assets/Prefabs/UI/Delivery List Button.prefab";
    private GameObject _deliveryButtonPrefab;

    private bool _isGrabbed = false;

    private void Awake()
    {
        _collider = GetComponentInChildren<BoxCollider>();
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (_isGrabbed && other.CompareTag("PhoneContainer"))
        {
            transform.SetParent(other.transform.GetChild(0), false);
            _isGrabbed = false;
        }
    }

    public void Grab(Transform hand)
    {
        _isGrabbed = true;
        transform.SetParent(hand, false);
    }
}
