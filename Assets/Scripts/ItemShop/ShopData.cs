using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Data", menuName = "Scriptable Object/Shop Data", order = int.MaxValue)]
public class ShopData : ScriptableObject
{
    public int carrotPrice = 1000;
    public int speedUpgradePrice = 5000;
    public int durabilityUpgradePrice = 3000;
}
