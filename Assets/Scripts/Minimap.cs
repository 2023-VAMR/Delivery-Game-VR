using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [Header("Components")]
    [SerializeField] private GameObject targetIcon;
    [SerializeField] private GameObject arrowIcon;
    private Camera minimapCamera;

    private void Start()
    {
        minimapCamera = GetComponentInChildren<Camera>();
    }
    void FixedUpdate()
    {
        if (_target != null)
        {
            PointToTarget();
        }
        else
        {
            DisableNavigation();
        }
    }

    private void DisableNavigation()
    {
        targetIcon.SetActive(false);
        arrowIcon.SetActive(false);
    }

    private void PointToTarget()
    {
        var viewPos = minimapCamera.WorldToViewportPoint(targetIcon.transform.position);
        bool onScreen = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;
        if (onScreen)
        {
            arrowIcon.SetActive(false);
        }
        else
        {
            arrowIcon.SetActive(true);
            arrowIcon.transform.LookAt(targetIcon.transform.position);
        }
    }


    public void SetTarget(Transform target)
    {
        _target = target;
        targetIcon.SetActive(true);
        targetIcon.transform.parent = _target;
        targetIcon.transform.position 
            = new Vector3(
                targetIcon.transform.position.x,
                arrowIcon.transform.position.y,
                targetIcon.transform.position.z
            );
        targetIcon.transform.localPosition = new Vector3(0, targetIcon.transform.localPosition.y, 0);
    }

    public void RemoveTarget()
    {
        _target = null;
    }
}
