using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovement : MonoBehaviour
{
    Animator animator;
    [SerializeField] float moveSpeed = 1.0f;

    [SerializeField] int moveLength = 15;

    private const string playerTag = "Player";

    void Start()
    {
        animator = GetComponent<Animator>();
        MoveToRoad(); // Move to road on instantiate

        Destroy(gameObject, 20.0f); // Destroy npc after delay
    }

    // Update is called once per frame
    void Update()
    {

    }


    private IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            animator.SetBool("RunStart", true);
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);

            // Set false inside the loop to stop animation immediately
            if (objectToMove.transform.position == end)
            {
                animator.SetBool("RunStart", false);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void MoveToRoad()
    {
        Vector3 end = transform.position + Vector3.forward * moveLength;
        animator.SetBool("RunStart", false);
        StartCoroutine(MoveOverSpeed(gameObject, end, moveSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(playerTag))
        {
            animator.SetBool("Death", true);
            Destroy(gameObject, 5); // Destroy npc after delay
        }

        // Event for hitting child
    }
}
