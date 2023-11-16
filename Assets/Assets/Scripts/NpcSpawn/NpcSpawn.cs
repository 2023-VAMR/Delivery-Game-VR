using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] npcPrefabs;
    bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!spawned)
        {
            if (other.gameObject.CompareTag("Car"))
            {
                GameObject newItem = Instantiate(npcPrefabs[0], transform); // Instantiate in local space
                newItem.transform.parent = transform.parent; // make sibiling
                spawned = true;
            }
        }
    }
}

