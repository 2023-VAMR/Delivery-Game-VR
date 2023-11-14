using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearState
{
    Drive,
    Reverse,
    MaxCount,
}

public class Gear : MonoBehaviour
{
    public GearState state;

    private UIManager UM;
    private InputManager IM;

    private void Start()
    {
        UM = UIManager.Instance;
        IM = InputManager.Instance;
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
        }
    }
}
