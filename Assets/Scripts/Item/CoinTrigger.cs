using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinTrigger : MonoBehaviour
{
    void Start()
    {
        Tween tween = transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear) 
            .SetLoops( -1, LoopType.Restart); 
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("DESTROY!!");
            other.gameObject.GetComponent<PlayerDataHelper>().coin += 100;
            Destroy(this.gameObject);
        }
    }
}
