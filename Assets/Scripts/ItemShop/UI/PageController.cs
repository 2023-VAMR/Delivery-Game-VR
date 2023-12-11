using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ItemShop
{

    public class PageController : MonoBehaviour
    {
        [Header("Pages")]
        [SerializeField] Transform pageContainer;
        [Space]
        [SerializeField] GameObject homePage;
        [SerializeField] GameObject carUpgradeShopPage;
        [SerializeField] GameObject itemShopPage;
        [Header("Header")]
        [SerializeField] GameObject closeIcon;
        [SerializeField] GameObject backIcon;
        [SerializeField] TextMeshProUGUI coin;

        private List<GameObject> _history;
        private GameObject _currentPage;

        private PlayerData _playerData = null;
        private readonly static string playerDataAddress = "Assets/Data/Player/PlayerData.asset";


        private void Awake()
        {
            _history = new List<GameObject>();
            GoHomePage();
        }

        private void Start()
        {
            Addressables.LoadAssetAsync<PlayerData>(playerDataAddress).Completed += (handle) => 
            {
                _playerData = handle.Result;
                _playerData.AddListener(SetHeader);
                SetHeader();
            };
        }

        public void GoHomePage()
        {
            GoPage(homePage.name);
        }

        public void GoCarUpgradeShopPage()
        {
            GoPage(carUpgradeShopPage.name);
        }

        public void GoItemShopPage()
        {
            GoPage(itemShopPage.name);
        }

        private void TurnOffAllPages()
        {
            foreach (Transform page in pageContainer)
            {
                page.gameObject.SetActive(false);
            }
        }

        private void GoPage(string pageName, bool isSaveHistory = true)
        {
            TurnOffAllPages();
            foreach (Transform page in pageContainer)
            {
                if (page.name != pageName) continue;

                GoPage(page.gameObject, isSaveHistory);
                return;
            }
        }

        private void GoPage(GameObject page, bool isSaveHistory = true)
        {
            if(page.name == homePage.name)
            {
                isSaveHistory = false;
                _history.Clear();
            }

            page.SetActive(true);

            if (isSaveHistory)
            {
                _history.Add(_currentPage);
            }
            _currentPage = page;
            SetHeader();
        }

        public void GoExPage()
        {
            if (_history.Count == 0) return;

            var exPage = _history[_history.Count - 1];
            _history.RemoveAt(_history.Count - 1);
            GoPage(exPage.name, false);
        }

        private void SetHeader()
        {
            //Set Header Button
            closeIcon.SetActive(_history.Count == 0);
            backIcon.SetActive(_history.Count != 0);

            //Set CoinData
            if(_playerData is not null)
                coin.text = _playerData.coin.ToString();
        }
    }
}



