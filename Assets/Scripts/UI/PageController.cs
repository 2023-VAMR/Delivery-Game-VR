using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] GameObject homePage;
    [SerializeField] GameObject deliveryOrderListPage;
    [SerializeField] GameObject currentOrderInfoPage;
    [SerializeField] GameObject deliveryResultInfoPage;

    [SerializeField] private List<GameObject> _history;
    private GameObject _currentPage;

    private void Awake()
    {
        _history = new List<GameObject>();
        GoHomePage();
    }

    public void GoHomePage()
    {
        _history.Clear();

        TurnOffAllPages();
        homePage.SetActive(true);
        _currentPage = homePage;
    }

    public void GoDeliveryOrderListPage()
    {
        _history.Add(_currentPage);

        TurnOffAllPages();
        deliveryOrderListPage.SetActive(true);
        _currentPage = deliveryOrderListPage;
    }

    public void GoCurrentOrderInfoPage()
    {
        _history.Add(_currentPage);

        TurnOffAllPages();
        currentOrderInfoPage.SetActive(true);
        _currentPage = currentOrderInfoPage;

    }

    public void GoDeliveryResultInfoPage()
    {
        _history.Add(_currentPage);

        TurnOffAllPages();
        deliveryResultInfoPage.SetActive(true);
        _currentPage = deliveryResultInfoPage;

    }

    private void TurnOffAllPages()
    {
        foreach (Transform page in transform)
        {
            page.gameObject.SetActive(false);
        }
    }

    public void GoExPage()
    {
        if (_history.Count == 0) return;

        var exPage = _history[_history.Count - 1];
        _history.RemoveAt(_history.Count - 1);
        TurnOffAllPages();
        exPage.SetActive(true);
    }
}
