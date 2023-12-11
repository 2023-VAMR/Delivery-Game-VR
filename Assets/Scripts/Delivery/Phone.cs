using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;


[RequireComponent(typeof(Rigidbody))]
public class Phone : MonoBehaviour
{
    GameManager GM;
    UIManager UM;


    private PageController _pageController;
    private DeliveryOrderListPageController _deliveryOrderListPageController;



    private bool _isGrabbed = false;

    private void Awake()
    {
        _pageController = GetComponentInChildren<PageController>();
        _deliveryOrderListPageController = GetComponentInChildren<DeliveryOrderListPageController>(true);
    }

    private void Start()
    {
        GM = GameManager.Instance;
        UM = UIManager.Instance;
    }

    public void OnClickRespawnButton()
    {
        GM.RespawnPlayer();
    }

    public void AddNewButton(DeliveryPoint food, DeliveryPoint dest, float limitTime)
    {
        _deliveryOrderListPageController.AddNewButton(food, dest, limitTime, OnStartOrder);
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
        _pageController.GoDeliveryOrderListPage();
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
            Release(other.transform.GetChild(0));
        }
    }

    public void Grab(Transform hand)
    {
        if (_isGrabbed) return;
        transform.SetParent(hand, false);
        UM.EnableUIInput();
        _isGrabbed = true;
    }

    public void Release(Transform other)
    {
        if (!_isGrabbed) return;
        transform.SetParent(other, false);
        UM.DisableUIInput();
        _isGrabbed = false;
    }
}
