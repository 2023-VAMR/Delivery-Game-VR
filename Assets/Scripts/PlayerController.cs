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
        for (int i = 2; i < 4; i++)
        {
            wheels[i].motorTorque = IM.vertical * MaxMotorPower * -1;
            wheels[i].brakeTorque = IM.isBrake ? brakePower : 0;
        }
        
        for (int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = _steeringWheel.steering * MaxSteering;

        }
    }
}
