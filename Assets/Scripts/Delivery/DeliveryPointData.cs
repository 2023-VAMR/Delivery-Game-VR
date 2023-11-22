using UnityEngine;

[CreateAssetMenu(fileName = "Delivary Point Data", menuName = "Scriptable Object/Delivary Point Data", order = int.MaxValue)]
public class DeliveryPointData : ScriptableObject
{
    public int id;
    public string pointName;
    public Sprite pointImage;
}
