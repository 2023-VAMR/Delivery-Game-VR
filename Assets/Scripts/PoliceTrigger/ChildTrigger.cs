using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    public GameObject policeCar;
    public Transform policecarSpawnPoint;

    private void OnCollisionEnter(Collision collision)
    {
        policeCar.transform.position = policecarSpawnPoint.position;
        policeCar.transform.rotation = policecarSpawnPoint.rotation;
        policeCar.SetActive(true);
        policeCar.GetComponent<PoliceCarChase>().SetTarget();
    }
}
