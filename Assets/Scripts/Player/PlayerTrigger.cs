using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PoliceCar")
        {
            this.gameObject.GetComponent<PlayerData>().coin -= 3;
        }
    }
}
