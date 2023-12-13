using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] npcPrefabs;
    [SerializeField] GameObject policecar;
    [SerializeField] Transform Policepoint;
    bool spawned = false;
    private const string playerTag = "Player";
    public int numToSpawn = 1;
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
            if (other.gameObject.CompareTag(playerTag))
            {
                for (int i = 0; i < numToSpawn; i++)
                {
                    GameObject newItem = Instantiate(npcPrefabs[0], transform); // Instantiate in local space
                    newItem.transform.parent = transform.parent; // make sibiling
                }
                spawned = true;
                if (npcPrefabs[0].gameObject.CompareTag("Child"))
                {
                    npcPrefabs[0].GetComponent<ChildTrigger>().policeCar = this.policecar;
                    npcPrefabs[0].GetComponent<ChildTrigger>().policecarSpawnPoint = this.Policepoint;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int childNum = transform.parent.childCount;
        if (childNum <= 1) spawned = false;
    }
}

