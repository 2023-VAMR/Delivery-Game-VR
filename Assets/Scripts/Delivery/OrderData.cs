using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order Data", menuName = "Scriptable Object/Order Data", order = int.MaxValue)]
public class OrderData : ScriptableObject
{
    public enum Progress
    {
        NoOrder,
        TakeFood,
        DeliverFood,
        Done,
    }

    public enum Result
    {
        NotDecided,
        Success,
        Fail,
        Canceled,
    }

    public DeliveryPoint foodPoint;
    public DeliveryPoint destPoint;
    public float progressTime = 0;
    public float LimitTime = 120;
    public Progress progress = Progress.NoOrder;
    public Result result = Result.NotDecided;
    
}
