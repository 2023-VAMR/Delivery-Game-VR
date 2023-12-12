using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PoliceCar")
        {
            this.gameObject.GetComponent<PlayerDataHelper>().coin -= 3;
        }
        else if(other.gameObject.tag == "Animal")
        {
            this.gameObject.GetComponent<PlayerDataHelper>().durability -= 20;
            Debug.Log("Animal hit");
        }
    }
}
