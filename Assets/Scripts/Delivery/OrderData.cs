using System;
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
    [NonSerialized]
    public DeliveryPoint foodPoint;
    [NonSerialized]
    public DeliveryPoint destPoint;
    [NonSerialized]
    public float progressTime = 0;
    [NonSerialized]
    public float limitTime = 120;
    [NonSerialized]
    public Progress progress = Progress.NoOrder;

    // result info
    [NonSerialized]
    public int reward = 0;
    [NonSerialized]
    public int relibility = 0;
    [NonSerialized]
    public Result result = Result.NotDecided;
    
}
