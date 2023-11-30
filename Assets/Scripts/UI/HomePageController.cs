using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HomePageController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] TextMeshProUGUI relibility;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI durability;

    private PlayerData _data;
    private readonly static string playerDataAddress = "Assets/Data/Player/PlayerData.asset";

    private void Awake()
    {
        Addressables.LoadAssetAsync<PlayerData>(playerDataAddress).Completed
            += (handle) =>
            {
                _data = handle.Result;
                LoadData();
            };
    }

    private void OnEnable()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (_data is null) return;
        coin.text = _data.coin.ToString();
        relibility.text = _data.reliability.ToString();
        speed.text = _data.speed.ToString();
        durability.text = _data.durability.ToString();
    }
}
