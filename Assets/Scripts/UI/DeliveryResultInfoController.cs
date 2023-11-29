using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DeliveryResultInfoController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI relibility;
    [SerializeField] TextMeshProUGUI remainingTime;
    [SerializeField] TextMeshProUGUI result;

    private readonly static string inProgressOrderDataAddress = "Assets/Data/OrderData/InProgressOrderData.asset";
    private OrderData data;
    private void Start()
    {
        Addressables.LoadAssetAsync<OrderData>(inProgressOrderDataAddress).Completed 
            += (handle) => 
            {
                data = handle.Result;
                LoadData();
            };
    }

    private void OnEnable()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (data is null) return;
        if (rewardText == null) Debug.LogWarning("Why?");
        rewardText.text = data.reward.ToString();
        relibility.text = data.relibility.ToString();
        remainingTime.text = ((int)data.limitTime - (int)data.progressTime).ToString();

        result.text = data.result.ToString();
    }
}
