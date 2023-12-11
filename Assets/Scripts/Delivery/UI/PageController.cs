using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    private GameManager GM;

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

    private void Start()
    {
        GM = GameManager.Instance;
    }

    public void GoHomePage()
    {
        GoPage(homePage.name);
    }

    public void GoDeliveryOrderListPage()
    {
        GoPage(deliveryOrderListPage.name);
    }

    public void GoCurrentOrderInfoPage()
    {
        GoPage(currentOrderInfoPage.name);
    }

    public void GoDeliveryResultInfoPage()
    {
        GoPage(deliveryResultInfoPage.name);
    }

    private void TurnOffAllPages()
    {
        foreach (Transform page in transform)
        {
            page.gameObject.SetActive(false);
        }
    }

    private void GoPage(string pageName, bool isSaveHistory = true)
    {
        TurnOffAllPages();
        foreach (Transform page in transform)
        {
            if (page.name != pageName) continue;

            GoPage(page.gameObject, isSaveHistory);
            return;
        }
    }

    private void GoPage(GameObject page, bool isSaveHistory = true)
    {
        // 페이지 이동시 조건 체크
        if (page.name == homePage.name)
        {
            isSaveHistory = false;
            _history.Clear();
        }
        else if (page.name == deliveryOrderListPage.name)
        {
            if (GM.isProgessOrder)
            {
                page = currentOrderInfoPage;
            }
        }


        page.SetActive(true);

        if (isSaveHistory)
        {
            _history.Add(_currentPage);
        }
        _currentPage = page;
    }

    public void GoExPage()
    {
        if (_history.Count == 0) return;

        var exPage = PopPageOnHistory();
        if(GM.isProgessOrder && exPage == deliveryOrderListPage)
        {
            exPage = PopPageOnHistory();
        }

        GoPage(exPage.name, false);
    }
    
    private GameObject PopPageOnHistory()
    {
        if (_history.Count == 0) return homePage;
        var exPage = _history[_history.Count - 1];
        _history.RemoveAt(_history.Count - 1);
        return exPage;
    }

}
