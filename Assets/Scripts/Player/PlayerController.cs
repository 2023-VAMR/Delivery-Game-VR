using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerController : MonoBehaviour
{
    private InputManager IM;

    [SerializeField] private WheelCollider[] wheels;
    private GameObject[] _wheelMeshs;

    private SteeringWheel _steeringWheel;
    private Gear _gear;

    [SerializeField] private float MaxMotorPower = 15f;
    private int speedMultiplier = 0;
    [SerializeField] private float MaxSteering = 45f;
    [SerializeField] private float brakePower = 7.5f;
    [SerializeField] private float steeringRadius = 6;

    private PlayerData _playerData = null;
    private readonly static string playerDataAddress = "Assets/Data/Player/PlayerData.asset";

    void Awake()
    {
        
        _wheelMeshs = GameObject.FindGameObjectsWithTag("WheelMesh");
        _steeringWheel = GetComponentInChildren<SteeringWheel>();
        _gear = GetComponentInChildren<Gear>();

        Addressables.LoadAssetAsync<PlayerData>(playerDataAddress).Completed += (handle) =>
        {
            _playerData = handle.Result;
            UpdateSpeedMultiplier();
            _playerData.AddListener(UpdateSpeedMultiplier);
        };
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
        int gearMultiplier = _gear.state == GearState.Drive ? -1 : 1;
        for (int i = 2; i < 4; i++)
        {
            wheels[i].motorTorque = IM.vertical * MaxMotorPower * speedMultiplier * gearMultiplier;
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
            wheels[i].brakeTorque = IM.isBrake ? brakePower * speedMultiplier : 0;
        }
    }

    private void UpdateSpeedMultiplier()
    {
        speedMultiplier = _playerData.speed;
    }
}
