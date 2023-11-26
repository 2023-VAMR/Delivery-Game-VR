using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ThrowItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float throwForce = 500.0f;

    private InputManager IM;
    private Rigidbody _rigidbody;

    Vector3 itemSpawnPoint = new Vector3(0.3f, 0.4f, 0.4f);

    private GameObject itemPrefab;
    private readonly static string ItemPrefabAddress = "Assets/Prefabs/Carrot Variant.prefab";

    private GameObject grabbedItem = null;

    private Vector3 exPos;

    // Start is called before the first frame update
    void Start()
    {
        //cam = gameObject.GetComponentInChildren<Camera>();
        Addressables.LoadAssetAsync<GameObject>(ItemPrefabAddress).Completed += (handle) => { itemPrefab = handle.Result; };

        IM = InputManager.Instance;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        ThrowByHand();
    }

    private void FixedUpdate()
    {
        exPos = transform.position;
    }


    private void ThrowByMouse()
    {
        if (Input.GetMouseButtonDown(0)) // Left click: Throw carrot
        {

            GameObject newItem = Instantiate(itemPrefab, transform.position + itemSpawnPoint, transform.rotation); // Instantiate in local space
            newItem.transform.parent = transform;

            Vector3 direction = cam.ScreenPointToRay(Input.mousePosition).direction;
            newItem.GetComponent<Rigidbody>().AddForce(direction * throwForce);
        }
    }

    private void ThrowByHand()
    {
        if (grabbedItem != null && IM.isUpRightGrabButton)
        {
            var itemRigidbody = grabbedItem.GetComponent<Rigidbody>();
            var itemCollider = grabbedItem.GetComponent <Collider>();
            itemRigidbody.isKinematic = false;
            itemCollider.isTrigger = false;
            itemRigidbody.AddForce((transform.position - exPos) * Time.deltaTime * throwForce, ForceMode.Impulse);
            grabbedItem.transform.SetParent(null);
            grabbedItem = null;



        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Item")))
        {
            collision.gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Inventory")) return;
        if (IM.isDownRightGrabButton)
        {
            grabbedItem = Instantiate(itemPrefab, transform);
            grabbedItem.transform.localScale = Vector3.one * 8;
            grabbedItem.GetComponent<Rigidbody>().isKinematic = true;
            grabbedItem.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Item")))
        {
            other.isTrigger = false;
        }
    }
}
