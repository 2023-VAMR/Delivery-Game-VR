using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class HandGrabber : MonoBehaviour
{
    public enum HandType
    {
        Left,
        Right,
    }

    public HandType type;

    private InputManager IM;
    public PickUpController pickUpController
    {
        private get;
        set;
    }

    private void Start()
    {
        IM = InputManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if(IM.isDownLeftGrabButton && type == HandType.Left)
        {
            pickUpController.LeftHandGrab(other);
        }
        else if(IM.isDownRightGrabButton && type == HandType.Right)
        {
            pickUpController.RightHandGrab(other);
        }
    }
}
