using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputManager IM;

    [SerializeField] private WheelCollider[] wheels;
    private GameObject[] _wheelMeshs;

    private Rigidbody _rigidBody;
    private SteeringWheel _steeringWheel;

    [SerializeField] private float maxMotorPower = 1500f;
    [SerializeField] private float brakePower = 250f;
    [SerializeField] private float downForceValue;
    [SerializeField] private float radius = 6;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.centerOfMass = Vector3.down;
        _steeringWheel = GetComponentInChildren<SteeringWheel>();
        _wheelMeshs = GameObject.FindGameObjectsWithTag("WheelMesh");
    }

    private void Start()
    {
        if (InputManager.Instance)
        {
            IM = InputManager.Instance;
        }

    }

    void FixedUpdate()
    {
        AccelerateVehicle();
        SteerVehicle();

    }

    void AccelerateVehicle()
    {
        for (int i = 2; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = IM.vertical * maxMotorPower * -1;
        }

        float brakeTorque = Input.GetKey(KeyCode.Space) ? brakePower : 0;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = brakeTorque;
        }

        _rigidBody.AddForce(-transform.up * downForceValue * _rigidBody.velocity.magnitude);
    }

    void SteerVehicle()
    {
        // 애커만 조향
        float horizontal = _steeringWheel.steering;
        if (horizontal > 0)
        {   // rear tracks size is set to 1.5f          wheel base has been set to 2.55f
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        }
        else if (horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            // transform.Rotate(Vector3.up * steerHelping)
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    //void SyncWheelRot()
    //{
    //    for(int i = 0; i < wheels.Length; i++)
    //    {
    //        wheels[i].GetWorldPose(out Vector3 wheelPos, out Quaternion wheelRot);
    //        _wheelMeshs[i].transform.position = wheelPos;
    //        _wheelMeshs[i].transform.rotation = wheelRot;
    //    }
    //}
}
