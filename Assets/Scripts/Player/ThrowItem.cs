using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ThrowItem : MonoBehaviour
{
    
    [SerializeField] private float throwForce = 500.0f;

    private InputManager IM;
    private Rigidbody _rigidbody;
    private Camera cam;

    Vector3 itemSpawnPoint = new Vector3(0.3f, 0.4f, 0.4f);

    private GameObject itemPrefab;
    private readonly static string ItemPrefabAddress = "Assets/Prefabs/Carrot Variant.prefab";

    private GameObject grabbedItem = null;

    private Vector3 exPos;

    private float itemTriggerSphereRadius = 3.0f;

    // Start is called before the first frame update
    private void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
        Addressables.LoadAssetAsync<GameObject>(ItemPrefabAddress).Completed += (handle) => { itemPrefab = handle.Result; };

        IM = InputManager.Instance;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        ThrowByHand();
        CheckIsItemFar();
    }

    private void FixedUpdate()
    {
        if(grabbedItem != null)
        {
            exPos = grabbedItem.transform.position;
        }
        
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
            var curPos = grabbedItem.transform.position;
            var force = (curPos - exPos) * Time.deltaTime * throwForce;
            itemRigidbody.AddForce(force, ForceMode.Impulse);
            grabbedItem.transform.SetParent(null);
            grabbedItem = null;
        }
    }
    public void GrabItem(Transform hand)
    {
        if (grabbedItem != null) return;
        grabbedItem = Instantiate(itemPrefab, hand);
        grabbedItem.transform.localScale = Vector3.one * 8;
        grabbedItem.GetComponent<Rigidbody>().isKinematic = true;
        grabbedItem.GetComponent<Collider>().isTrigger = true;
    }

    private void CheckIsItemFar()
    {
        // Check if item is in the range of the goat
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, itemTriggerSphereRadius, 1 << LayerMask.NameToLayer("Item"));

        if (hitColliders.Length > 0)
        {
            foreach(Collider c in hitColliders)
            {
                if (c.GetComponent<Rigidbody>().velocity.sqrMagnitude == 0) // if item has stopped bouncing or whatnot
                {
                    c.GetComponent<BoxCollider>().enabled = false; // disable colliders for misthrown carrots
                    Destroy(c.gameObject, 1.0f);
                }
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Item")))
        {
            collision.gameObject.GetComponent<Collider>().isTrigger = false;
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
