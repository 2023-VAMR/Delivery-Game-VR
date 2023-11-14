using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public float vertical;
    public float horizontal;

    public bool isDownLeftGrabButton;
    public bool isDownRightGrabButton;

    public bool isUpLeftGrabButton;
    public bool isUpRightGrabButton;

    public bool isBrake;

    [Header("For Debug")]
    public bool isInputByKeyboard = false;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {


        if (isInputByKeyboard)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            isBrake = Input.GetKey(KeyCode.Space);
        }
        else
        {
            vertical = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
            isBrake = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

            isDownLeftGrabButton = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
            isDownRightGrabButton = OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);
            isUpLeftGrabButton = OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger);
            isUpRightGrabButton = OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);
        }

    }
}
