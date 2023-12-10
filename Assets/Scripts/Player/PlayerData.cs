using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public int speed = 10;
    public int coin = 0;
    public int maxDurability = 100;
    public int durability = 100;
    public int reliability = 0;

    //inventory
    public int maxCarrotNum = 4;
    public int carrotNum = 0;
}
