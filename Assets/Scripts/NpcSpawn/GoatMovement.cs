using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoatMovement : MonoBehaviour
{
    Animator animator;
    [SerializeField] float moveSpeed = 1.0f;

    [SerializeField] float followSpeed = 0.7f;
    [SerializeField] float rotationSpeed = 1.5f;

    [SerializeField] int moveLength = 4;

    private const string carTag = "Car";

    bool itemFollowing = false;

    Collider curr_following = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        MoveToRoad(); // Move to road on instantiate
    }

    // Update is called once per frame
    void Update()
    {
        // Change this to random spawn 
        // Or spawn when a car is coming near
        //if (Input.GetKeyDown(KeyCode.P)) 
        //{
        //    Vector3 end = transform.position + Vector3.forward * blockSize;
        //    animator.SetBool("RunStart", false);
        //    StartCoroutine(MoveOverSpeed(gameObject, end, moveSpeed));

        //}

        
        // Check if item is in the range of the goat
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f, 1<<LayerMask.NameToLayer("Item"));

        if (hitColliders.Length > 0)
        {
            Collider c = hitColliders[0];
            if (c.GetComponent<Rigidbody>().velocity.sqrMagnitude == 0) // if item has stopped bouncing or whatnot
            {
                if (curr_following == null) // code to follow one item at a time
                {
                    FollowItem(c.transform, followSpeed, rotationSpeed); // Follow the carrot
                    itemFollowing = true;
                    curr_following = c; // set current following item
                }

            }
            if (c.transform.position.x == transform.position.x && c.transform.position.z == transform.position.z)
            {
                animator.SetBool("Eating", true);
                Destroy(c.gameObject, 2.0f); // Destroy carrot once at target position
            }
        }

        if (curr_following==null)
        {
            itemFollowing = false;
            animator.SetBool("Eating", false);
        }
    }


    void FollowItem(Transform target, float walk_speed, float rot_speed)
    {

        Vector3 end = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        Vector3 direction = target.position - transform.position;

        StartCoroutine(MoveOverSpeed(gameObject, end, walk_speed));
        StartCoroutine(TurnOverSpeed(gameObject, direction, rot_speed));
    }


    private IEnumerator MoveOverSpeed(GameObject objectToMove,  Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            animator.SetBool("RunStart", true);
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            
            // Set false inside the loop to stop animation immediately
            if(objectToMove.transform.position == end)
            {
                animator.SetBool("RunStart", false);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator TurnOverSpeed(GameObject objectToTurn,  Vector3 direction, float speed)
    {
        Quaternion rot_angle = Quaternion.LookRotation(direction, Vector3.up); // rotate along y-axis
        Quaternion end_rotation = objectToTurn.transform.rotation * rot_angle;
        while (!isApproximate(objectToTurn.transform.rotation,end_rotation, 0.0000004f))
        {
            animator.SetBool("RunStart", true);
            objectToTurn.transform.rotation = Quaternion.Slerp(objectToTurn.transform.rotation, end_rotation, speed * Time.deltaTime);

            // Set false inside the loop to stop animation immediately
            if (isApproximate(objectToTurn.transform.rotation, end_rotation, 0.0000004f))
            {
                animator.SetBool("RunStart", false);
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected void MoveToRoad()
    {
        Vector3 end = transform.position + Vector3.forward * moveLength;
        animator.SetBool("RunStart", false);
        StartCoroutine(MoveOverSpeed(gameObject, end, moveSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(carTag))
        {
            animator.SetBool("Death", true);
            Destroy(gameObject, 5); // Destroy animal after delay
        }

        // Event for hitting goat
    }

    public static bool isApproximate(Quaternion q1, Quaternion q2, float precision)
    {
        return Mathf.Abs(Quaternion.Dot(q1, q2)) >= 1 - precision;
    }
}
