using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraConfig : MonoBehaviour
{
    private ThirdPersonCameraMovement _cam;

    private void Awake()
    {
        _cam = GetComponent<ThirdPersonCameraMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Application.isPlaying)
        {
            if(!_cam) _cam = GetComponent<ThirdPersonCameraMovement>();
            _cam.SetCameraToOrigin();
            
        }
    }
}
