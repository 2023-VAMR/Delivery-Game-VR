using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] float throwForce = 500.0f;
    
    Vector3 itemSpawnPoint = new Vector3(0.3f, 0.4f, 0.4f);

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C)) // spawn carrot
        //{
        //    GameObject newItem = Instantiate(itemPrefab, transform.position + itemSpawnPoint, transform.rotation); // Instantiate in local space
        //    newItem.transform.parent = transform;
        //    itemSpawned = newItem;
        //}

        if (Input.GetMouseButtonDown(0)) // Left click: Throw carrot
        {

            GameObject newItem = Instantiate(itemPrefab, transform.position + itemSpawnPoint, transform.rotation); // Instantiate in local space
            newItem.transform.parent = transform;

            Vector3 direction = cam.ScreenPointToRay(Input.mousePosition).direction;
            newItem.GetComponent<Rigidbody>().AddForce(direction * throwForce);
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
