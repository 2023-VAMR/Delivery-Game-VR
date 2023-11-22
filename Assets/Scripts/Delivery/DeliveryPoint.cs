using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DeliveryPoint : MonoBehaviour
{
    private GameManager GM;

    public DeliveryPointData data;

    private GameObject _triggerBox;

    private void Awake()
    {
        _triggerBox = transform.GetChild(0).gameObject;
        _triggerBox.SetActive(false);
    }

    private void Start()
    {
        GM = GameManager.Instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GM.ArriveDelivaryPoint(this);
    }

    public void EnablePoint() 
    {
        _triggerBox.SetActive(true);
    }

    public void DisablePoint()
    {
        _triggerBox.SetActive(false);
    }

    public bool IsSamePoint(DeliveryPointData other)
    {
        return data == other;
    }
}
