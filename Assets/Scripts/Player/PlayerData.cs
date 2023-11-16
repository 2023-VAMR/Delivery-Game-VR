using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int speed = 10;
    public int coin = 0;
    public int durability = 100;
    public int reliability = 0;
    public int CalculateSalary()
    {
        return 10000 + reliability * 1000;
    }
}
