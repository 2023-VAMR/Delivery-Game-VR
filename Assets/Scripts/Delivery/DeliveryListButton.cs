using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryListButton : MonoBehaviour
{
    [SerializeField] private Image foodImage;
    [SerializeField] private Image destImage;
    [SerializeField] private TextMeshProUGUI limitTimeText;

    private DeliveryPoint _foodPoint;
    private DeliveryPoint _destPoint;
    private float _limitTime;

    public delegate void OnStartOrderListener();
    OnStartOrderListener _listener;


    public void SetButton(DeliveryPoint foodPoint, DeliveryPoint destPoint, float limitTime, OnStartOrderListener listener)
    {
        _foodPoint = foodPoint;
        _destPoint = destPoint;
        _limitTime = limitTime;
        _listener = listener;
        if(_foodPoint.data.pointImage is not null)
        {
            foodImage.sprite = _foodPoint.data.pointImage;
        }

        if(_destPoint.data.pointImage is not null)
        { 
            destImage.sprite = _destPoint.data.pointImage;
        }
        limitTimeText.text = $"{(int)_limitTime} sec";
    }

    public void OnButtonClick()
    {
        bool isSuccess = GameManager.Instance.TryStartOrder(_foodPoint, _destPoint, _limitTime);
        if (isSuccess)
        {
            _listener();
            Destroy(gameObject);
        }
    }
}
