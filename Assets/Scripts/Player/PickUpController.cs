using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private HandGrabber leftHand;
    [SerializeField] private HandGrabber rightHand;


    private UIManager UM;

    private ThrowItem _throwItem;

    private void Awake()
    {
        _throwItem = GetComponent<ThrowItem>();
    }

    private void Start()
    {
        UM = UIManager.Instance;
        foreach (var grabber in GetComponentsInChildren<HandGrabber>())
        {
            if(grabber.type == HandGrabber.HandType.Left)
            {
                leftHand = grabber;
            }
            else
            {
                rightHand = grabber;
            }
            grabber.pickUpController = this;
        }
    }

    public void LeftHandGrab(Collider other)
    {
        if (other.CompareTag("Inventory"))
        {
            var inventory = other.GetComponent<Inventory>();
            if (inventory.TryUseItem())
            {
                _throwItem.GrabItem(leftHand.transform);
            }
        }
        else if (other.CompareTag("PhoneContainer"))
        {
            var phone = other.GetComponentInChildren<Phone>();
            if (phone is not null)
            {
                GrabPhone(phone, HandGrabber.HandType.Left);
            }
        }
    }

    public void RightHandGrab(Collider other)
    {
        if (other.CompareTag("Inventory"))
        {
            _throwItem.GrabItem(rightHand.transform);
        }
        //else if (other.CompareTag("PhoneContainer"))
        //{
        //    var phone = other.GetComponentInChildren<Phone>();
        //    GrabPhone(phone, HandGrabber.HandType.Right);
        //}
    }

    private void GrabPhone(Phone phone, HandGrabber.HandType type)
    {
        if(type == HandGrabber.HandType.Left)
        {
            phone.Grab(leftHand.transform);
        }
        //else
        //{
        //    phone.transform.SetParent(rightHand.transform, false);
        //    UM.SetInputTargetTransform(leftHand.transform);
        //}
    }
}
