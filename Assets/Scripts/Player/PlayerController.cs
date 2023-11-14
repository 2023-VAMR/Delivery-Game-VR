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

    [SerializeField] private float MaxMotorPower = 1500f;
    [SerializeField] private float MaxSteering = 45f;
    [SerializeField] private float brakePower = 250f;
    [SerializeField] private float steeringRadius = 6;
    
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        
        _wheelMeshs = GameObject.FindGameObjectsWithTag("WheelMesh");
        _steeringWheel = GetComponentInChildren<SteeringWheel>();
    }

    private void Start()
    {
        IM = InputManager.Instance;
    }

    void FixedUpdate()
    {
        Accelerate();
        Brake();
        Steer();
    }

    private void Accelerate()
    {
        for (int i = 2; i < 4; i++)
        {
            wheels[i].motorTorque = IM.vertical * MaxMotorPower * -1;
        }
    }

    private void Steer()
    {

        float steering = _steeringWheel.steering;
        if (steering > 0)
        {   // rear tracks size is set to 1.5f          wheel base has been set to 2.55f
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (steeringRadius + (1.5f / 2))) * steering;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (steeringRadius - (1.5f / 2))) * steering;
        }
        else if (steering < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (steeringRadius - (1.5f / 2))) * steering;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (steeringRadius + (1.5f / 2))) * steering;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    private void Brake()
    {
        for(int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = IM.isBrake ? brakePower : 0;
        }
    }
}
