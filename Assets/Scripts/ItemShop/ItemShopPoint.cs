using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemShopPoint : MonoBehaviour
{
    private UIManager UM;

    private GameObject _triggerBox;

    private void Awake()
    {
        _triggerBox = transform.GetChild(0).gameObject;
        _triggerBox.SetActive(true);
    }

    private void Start()
    {
        UM = UIManager.Instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        UM.EnableShopUI();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        UM.DisableShopUI();
    }

    public void EnablePoint() 
    {
        _triggerBox.SetActive(true);
    }

    public void DisablePoint()
    {
        _triggerBox.SetActive(false);
    }
}
