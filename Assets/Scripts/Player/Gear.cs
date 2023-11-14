using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearState
{
    Drive,
    Reverse,
    MaxCount,
}

public class Gear : MonoBehaviour
{
    public GearState state;

    private void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
