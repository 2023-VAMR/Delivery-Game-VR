using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    private InputManager IM;

    public float maxSteering = 120;
    public float steering = 0;
    public float turnDamping = 250;

    [SerializeField] private Transform snapPosParent;
    private List<Transform> _snapPositions;
    [SerializeField] private Transform directionalObject;

    private GameObject _leftHand;
    private Transform _leftHandOriginalParent;
    private bool _isLeftHandOnWheel = false;

    private GameObject _rightHand;
    private Transform _rightHandOriginalParent;
    private bool _isRightHandOnWheel = false;

    private void Start()
    {
        IM = InputManager.Instance;
        _snapPositions = new List<Transform>();
        foreach(Transform pos in snapPosParent)
        {
            _snapPositions.Add(pos);
        }
    }

    void Update()
    {
        ReleaseHandsFromWheel();
        Steering();
    }

    private void Steering()
    {
        //SteeringWithKeyboard();
        CalculateSteeringFromHandRotation();
        steering = (transform.localEulerAngles.z > 180 ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z) / maxSteering;
        
    }

    private void SteeringWithKeyboard()
    {
        //steering = IM.horizontal;
        Quaternion rot = Quaternion.Euler(0, 0, IM.horizontal * maxSteering);
        transform.localRotation = rot;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.LogWarning("Stay");
        if (other.CompareTag("RightHand") && !_isRightHandOnWheel && IM.isDownRightGrabButton)
        {
            _rightHand = other.gameObject;
            PlaceHandOnWheel(ref _rightHand, ref _rightHandOriginalParent, ref _isRightHandOnWheel);
        }

        if(other.CompareTag("LeftHand") && !_isLeftHandOnWheel && IM.isDownLeftGrabButton)
        {
            _leftHand = other.gameObject;
            PlaceHandOnWheel(ref _leftHand, ref _leftHandOriginalParent, ref _isLeftHandOnWheel);
        }
    }

    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnwheel)
    {
        originalParent = hand.transform.parent;

        GetBestSnapPosition(hand, out Transform snapPosition);
        hand.transform.parent = snapPosition.transform;
        hand.transform.position = snapPosition.transform.position;

        handOnwheel = true;
    }

    private void GetBestSnapPosition(GameObject hand, out Transform position)
    {
        var minDistance = Vector3.Distance(_snapPositions[0].position, hand.transform.position);
        var bestPosition = _snapPositions[0];
        foreach (var snapPosition in _snapPositions)
        {
            // this means there is the hand on snapPosition
            if (snapPosition.childCount != 0) continue;
            var distance = Vector3.Distance(snapPosition.position, hand.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestPosition = snapPosition;
            }
        }

        position = bestPosition;
    }

    private void ReleaseHandsFromWheel()
    {
        if(_isRightHandOnWheel && IM.isUpRightGrabButton)
        {
            ReleaseHand(ref _rightHand, _rightHandOriginalParent, ref _isRightHandOnWheel);
        }

        if (_isLeftHandOnWheel && IM.isUpLeftGrabButton)
        {
            ReleaseHand(ref _leftHand, _leftHandOriginalParent, ref _isLeftHandOnWheel);
        }

    }

    private void ReleaseHand(ref GameObject hand, Transform originalParent, ref bool handOnwheel)
    {
        hand.transform.parent = originalParent;
        hand.transform.position = originalParent.position;
        hand.transform.rotation = originalParent.rotation;
        handOnwheel = false;
    }



    private void CalculateSteeringFromHandRotation()
    {
        if (!_isRightHandOnWheel && !_isLeftHandOnWheel) return;

        Quaternion rot;
        if (_isRightHandOnWheel && _isLeftHandOnWheel)
        {
            Quaternion rightRot = Quaternion.Euler(0, 0, -_rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion leftRot = Quaternion.Euler(0, 0, -_leftHandOriginalParent.transform.rotation.eulerAngles.z);
            rot = Quaternion.Slerp(leftRot, rightRot, 0.5f);
        }
        else if (_isRightHandOnWheel)
        {
            rot = Quaternion.Euler(0, 0, -_rightHandOriginalParent.transform.rotation.eulerAngles.z);
        }
        else// if (_isLeftHandOnWheel)
        {
            rot = Quaternion.Euler(0, 0, -_leftHandOriginalParent.transform.rotation.eulerAngles.z);
            
        }
        transform.localRotation = rot;
    }
}
