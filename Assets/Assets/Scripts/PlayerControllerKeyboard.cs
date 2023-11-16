using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKeyboard: MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    private GameObject[] _wheelMeshs;

    private Rigidbody _rigidBody;

    [SerializeField] private float MaxMotorPower = 1500f;
    [SerializeField] private float MaxSteering = 45f;
    [SerializeField] private float brakePower = 250f;
    
    private float motorPower = 1500f;
    private float steering = 45f;
    
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        
        _wheelMeshs = GameObject.FindGameObjectsWithTag("WheelMesh");
    }

    void FixedUpdate()
    {
        for (int i = 2; i < 4; i++)
        {
            wheels[i].motorTorque = Input.GetAxis("Vertical") * motorPower * -1;
        }
        
        for (int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = Input.GetAxis("Horizontal") * steering;
            wheels[i].brakeTorque = Input.GetKey(KeyCode.Space) ? brakePower : 0;
                

        }
    }
}
