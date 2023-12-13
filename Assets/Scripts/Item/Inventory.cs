using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Inventory : MonoBehaviour
{
    private List<GameObject> itemObjects;

    private static string playerDataAddress = "Assets/Data/Player/PlayerData.asset";
    private PlayerData _playerData;

    private void Awake()
    {
        Addressables.LoadAssetAsync<PlayerData>(playerDataAddress).Completed
            += (handle) =>
            {
                _playerData = handle.Result;
                _playerData.AddListener(SetActiveItemObjects);
                SetActiveItemObjects();
            };

        itemObjects = new List<GameObject>();
        foreach(Transform child in transform)
        {
            itemObjects.Add(child.gameObject);
        }
    }
    
    public bool TryUseItem()
    {
        if(_playerData.carrotNum > 0)
        {
            _playerData.carrotNum--;
            SetActiveItemObjects();
            return true;
        }
        return false;
    }

    private void SetActiveItemObjects()
    {
        for(int i = 0; i < itemObjects.Count; i++)
        {
            itemObjects[i].SetActive(_playerData.carrotNum > i);
        }
    }
}
