using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private UIManager UM;
    private ItemShop.PageController _pageController;
    private void Awake()
    {
        _pageController = GetComponentInChildren<ItemShop.PageController>(true);
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
}
