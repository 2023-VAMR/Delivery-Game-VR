using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;


[RequireComponent(typeof(Rigidbody))]
public class Phone : MonoBehaviour
{
    GameManager GM;

    [SerializeField] private Transform content;

    private PageController _pageController;

    private readonly static string DeliveryButtonPrefabAddress = "Assets/Prefabs/UI/Delivery List Button.prefab";
    private GameObject _deliveryButtonPrefab;

    private bool _isGrabbed = false;

    private void Awake()
    {
        _pageController = GetComponentInChildren<PageController>();
    }

    private void Start()
    {
        Addressables.LoadAssetAsync<GameObject>(DeliveryButtonPrefabAddress).Completed += (handle) => { _deliveryButtonPrefab = handle.Result; };
        GM = GameManager.Instance;
    }

    public void AddNewButton(DeliveryPoint food, DeliveryPoint dest, float limitTime)
    {
        GameObject gObject = Instantiate(_deliveryButtonPrefab, content);
        DeliveryListButton button = gObject.GetComponent<DeliveryListButton>();

        button.SetButton(food, dest, limitTime, OnStartOrder);
    }

    private void OnStartOrder()
    {
        GoCurrentOrderInfoPage();
    }

    public void GoHomePage()
    {
        _pageController.GoHomePage();
    }

    public void GoDeliveryOrderListPage()
    {
        if (GM.isProgessOrder)
        {
            GoCurrentOrderInfoPage();
        }
        else
        {
            _pageController.GoDeliveryOrderListPage();
        }
        
    }

    public void GoCurrentOrderInfoPage()
    {
        _pageController.GoCurrentOrderInfoPage();
    }

    public void GoDeliveryResultInfoPage()
    {
        _pageController.GoDeliveryResultInfoPage();
    }

    public void CancelOrder()
    {
        GM.CancelOrder();
    }

    public void FinishOrder()
    {
        GoDeliveryResultInfoPage();
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
