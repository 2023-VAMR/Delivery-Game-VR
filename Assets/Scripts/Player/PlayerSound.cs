using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] float carSpeed;
    [SerializeField] float carMaxSpeed = 6;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;


    private readonly static string CarIdleSFXAddress = "Assets/Sound/SFX/Car_Idle.wav";
    private AudioClip _carIdle;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.loop = true;
    }

    private void Start()
    {
        Addressables.LoadAssetAsync<AudioClip>(CarIdleSFXAddress).Completed += handle => { _carIdle = handle.Result; };
    }

    private void FixedUpdate()
    {
        carSpeed = _rigidbody.velocity.magnitude;
        if ( _audioSource.clip != _carIdle)
        {
            _audioSource.clip = _carIdle;
            _audioSource.volume = 0.5f;
            _audioSource.Play();
        }
        _audioSource.pitch = Mathf.Lerp(1f, 2f, carSpeed / carMaxSpeed);
    }
}
