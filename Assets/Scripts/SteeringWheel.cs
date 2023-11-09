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
        //steering = IM.horizontal;
        Quaternion rot = Quaternion.Euler(0, 0, IM.horizontal * maxSteering);
        transform.localRotation = rot;
        steering = (transform.localEulerAngles.z > 180 ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z) / maxSteering;

    }

    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnwheel)
    {
        originalParent = hand.transform.parent;

        GetBestSnapPosition(hand, out Transform snapPosition);
        hand.transform.parent = snapPosition.transform;
        hand.transform.position = snapPosition.transform.position;

        handOnwheel = true;
    }

    private void ReleaseHandOnWheel(GameObject hand, Transform originalParent, ref bool handOnwheel)
    {
        hand.transform.parent = originalParent;
        hand.transform.position = originalParent.position;
        hand.transform.rotation = originalParent.rotation;
        handOnwheel = false;
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
            if(distance < minDistance)
            {
                minDistance = distance;
                bestPosition = snapPosition;
            }
        }

        position = bestPosition;
    }

    private void CalculateSteeringFromHandRotation()
    {
        if(_isRightHandOnWheel && _isLeftHandOnWheel)
        {
            Quaternion rightRot = Quaternion.Euler(0, 0, _rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion leftRot = Quaternion.Euler(0, 0, _leftHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion rot = Quaternion.Slerp(leftRot, rightRot, 0.5f);
            transform.rotation = rot;
        }
        else if (_isRightHandOnWheel)
        {
            Quaternion rot = Quaternion.Euler(0, 0, _rightHandOriginalParent.transform.rotation.eulerAngles.z);
            transform.rotation = rot;
        }
        else if (_isLeftHandOnWheel)
        {
            Quaternion rot = Quaternion.Euler(0, 0, _leftHandOriginalParent.transform.rotation.eulerAngles.z);
            transform.rotation = rot;
        }
    }
}
