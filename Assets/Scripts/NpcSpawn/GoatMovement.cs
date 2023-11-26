using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoatMovement : MonoBehaviour
{
    Animator animator;
    [SerializeField] float moveSpeed = 1.0f;

    [SerializeField] float followSpeed = 2.0f;
    [SerializeField] float rotationSpeed = 1.5f;

    private float blockSize = 4.0f;

    private float blockOffset = 3.0f;

    private float itemTriggerSphereRadius = 50.0f;

    private const string playerTag = "Player";


    Collider curr_following = null;

    void Start()
    {
        animator = GetComponent<Animator>();

        int animalNum = transform.parent.childCount;
        int idx = transform.GetSiblingIndex()-1;
        MoveToRoad(animalNum, idx); // Move to road on instantiate
    }

    // Update is called once per frame
    void Update()
    {
        // Check if item is in the range of the goat
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, itemTriggerSphereRadius, 1<<LayerMask.NameToLayer("Item"));

        if (hitColliders.Length > 0)
        {
            Collider c = hitColliders[0];
            if (c.GetComponent<Rigidbody>().velocity.sqrMagnitude == 0) // if item has stopped bouncing or whatnot
            {
                if (curr_following == null) // code to follow one item at a time
                {
                    TurnAndMoveToTarget(c.transform.position, followSpeed, rotationSpeed, true); // Follow the carrot
                    curr_following = c; // set current following item


                    Destroy(gameObject, 10.0f); // Destroy goat after following carrot
                    Destroy(c.gameObject, 10.0f); // Destroy carrot once at target position
                }

            }

        }
    }


    void TurnAndMoveToTarget(Vector3 targetPosition, float walk_speed, float rot_speed, bool run)
    {

        Vector3 end = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

        Vector3 direction = targetPosition - transform.position;


        StartCoroutine(MoveOverSpeed(gameObject, end, walk_speed,run));
        StartCoroutine(TurnOverSpeed(gameObject, direction, rot_speed,run));
    }


    private IEnumerator MoveOverSpeed(GameObject objectToMove,  Vector3 end, float speed, bool run)
    {
        // speed should be 1 unit per second
        while(!isApproximateVector(objectToMove.transform.position, end, 0.001f))
        {
            // Set animation
            if (run)
            {
                animator.SetBool("RunStart", true);
            }
            else
            {
                animator.SetBool("WalkStart", true);
            }



            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);

            // Set false inside the loop to stop animation immediately
            if(isApproximateVector(objectToMove.transform.position, end, 0.001f))
            {
                // Set animation
                if (run)
                {
                    animator.SetBool("RunStart", false);
                }
                else
                {
                    animator.SetBool("WalkStart", false);
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator TurnOverSpeed(GameObject objectToTurn,  Vector3 direction, float speed, bool run)
    {
        Quaternion rot_angle = Quaternion.LookRotation(direction, Vector3.up); // rotate along y-axis
        Quaternion end_rotation = objectToTurn.transform.rotation * rot_angle;
        while (!isApproximateQuaternion(objectToTurn.transform.rotation,end_rotation, 0.0000004f))
        {
            // Set animation
            if (run)
            {
                animator.SetBool("RunStart", true);
            }
            else
            {
                animator.SetBool("WalkStart", true);
            }

            objectToTurn.transform.rotation = Quaternion.Slerp(objectToTurn.transform.rotation, end_rotation, speed * Time.deltaTime);

            // Set false inside the loop to stop animation immediately
            if (isApproximateQuaternion(objectToTurn.transform.rotation, end_rotation, 0.0000004f))
            {
                // Set animation
                if (run)
                {
                    animator.SetBool("RunStart", false);
                }
                else
                {
                    animator.SetBool("WalkStart", false);
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected void MoveToRoad(int total, int idx)
    {
        // Move to idx point inside the specified block
        Vector3 end = transform.position
            + new Vector3(1, 0, 1) * blockOffset
            + Vector3.forward * (idx % 3) * blockSize
            + Vector3.right * (idx / 3) * blockSize
            + new Vector3(1, 0, 1) * (blockSize / 2) * Random.Range(-1.0f, 1.0f); // Random moving inside the designated block


        //Vector3 end = transform.position + Vector3.forward * moveLength;
        animator.SetBool("WalkStart", false);
        //StartCoroutine(MoveOverSpeed(gameObject, end, moveSpeed));
        TurnAndMoveToTarget(end, moveSpeed, rotationSpeed,false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(playerTag))
        {
            animator.SetBool("Death", true);
            Destroy(gameObject, 5); // Destroy animal after delay
        }

        // Event for hitting goat
    }

    public static bool isApproximateQuaternion(Quaternion q1, Quaternion q2, float precision)
    {
        return Mathf.Abs(Quaternion.Dot(q1, q2)) >= 1 - precision;
    }

    public static bool isApproximateVector(Vector3 v1, Vector3 v2, float precision)
    {
        return Vector3.Distance(v1, v2) <= precision;
    }
}
