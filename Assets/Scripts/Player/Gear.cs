using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum GearState
{
    Drive,
    Reverse,
    MaxCount,
}

[RequireComponent(typeof(AudioSource))]
public class Gear : MonoBehaviour
{
    public GearState state;

    private UIManager UM;
    private InputManager IM;

    private AudioSource _audioSource;

    private readonly string CarGearChangeSFXAddress = "Assets/Sound/SFX/Car_Gear_Change.wav";
    private AudioClip _carGearChange;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    private void Start()
    {
        UM = UIManager.Instance;
        IM = InputManager.Instance;

        Addressables.LoadAssetAsync<AudioClip>(CarGearChangeSFXAddress).Completed 
            += (handle) => { 
                _carGearChange = handle.Result;
                _audioSource.clip = _carGearChange;
            };

    }

    private void OnTriggerStay(Collider other)
    {
        bool isChangeGearByRH = other.CompareTag("RightHand") && IM.isDownRightGrabButton;
        bool isChangeGearByLH = other.CompareTag("LeftHand") && IM.isDownLeftGrabButton;
        if(isChangeGearByRH || isChangeGearByLH)
        {
            switch(state)
            {
                case GearState.Drive:
                    state = GearState.Reverse; 
                    break;
                case GearState.Reverse:
                    state = GearState.Drive;
                    break;
            }
            UM.SetGearText(state.ToString());
            _audioSource.Play();
        }
    }
}
