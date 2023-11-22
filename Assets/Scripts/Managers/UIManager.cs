using Meta.WitAi.Windows;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI gearStateUI;
    [SerializeField] private Minimap minimap;
    [SerializeField] private Phone phone;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void SetGearText(string text)
    {
        gearStateUI.text = text;
    }

    public void AddOrderInList(DeliveryPoint food, DeliveryPoint dest, float limitTime)
    {
        phone.AddNewButton(food, dest, limitTime);
    }

    public void SetMinimapTarget(Transform target)
    {
        minimap.SetTarget(target);
    }

    public void DisableNavigation()
    {
        minimap.RemoveTarget();
    }
}
