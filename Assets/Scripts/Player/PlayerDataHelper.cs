using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerDataHelper : MonoBehaviour
{
    public int speed
    {
        get => data.speed;
        set => data.speed = value;
    }
    public int coin
    {
        get => data.coin;
        set => data.coin = value;
    }
    public int durability
    {
        get => data.durability;
        set => data.durability = value;
    }
    public int reliability
    {
        get => data.reliability;
        set => data.reliability = value;
    }

    private PlayerData data;
    private readonly static string playerDataAddress = "Assets/Data/Player/PlayerData.asset";

    private void Awake()
    {
        Addressables.LoadAssetAsync<PlayerData>(playerDataAddress).Completed
            += (handle) =>
            {
                data = handle.Result;
            };
    }

    public static int CalculateSalary(int reliability)
    {
        return 10000 + reliability * 1000;
    }
}
