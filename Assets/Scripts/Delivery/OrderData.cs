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
    // progress info
    public DeliveryPoint foodPoint;
    public DeliveryPoint destPoint;
    public float progressTime = 0;
    public float limitTime = 120;
    public Progress progress = Progress.NoOrder;

    // result info
    public int reward = 0;
    public int relibility = 0;
    public Result result = Result.NotDecided;
    
}
