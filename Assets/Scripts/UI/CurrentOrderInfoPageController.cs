using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CurrentOrderInfoPageController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI foodName;
    [SerializeField] TextMeshProUGUI destName;
    [SerializeField] TextMeshProUGUI reward;
    [SerializeField] TextMeshProUGUI remainingTime;

    private readonly static string inProgressOrderDataAddress = "Assets/Data/OrderData/InProgressOrderData.asset";
    private OrderData data;
    private void Awake()
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
        foodName.text = data.foodPoint.data.pointName;
        destName.text = data.destPoint.data.pointName;
        reward.text = data.reward.ToString();
    }

    private void FixedUpdate()
    {
        DrawRemainingTime();
    }

    private void DrawRemainingTime()
    {
        if (data is null) return;
        remainingTime.text = ((int)data.limitTime - (int)data.progressTime).ToString();
    }
}
