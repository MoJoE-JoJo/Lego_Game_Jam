using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera drivingModeCamera, walkingModeCamera;

    public enum CameraMode
    {
        Walking, Driving, Controlling
    }

    public Transform playerTransform;
    public Transform submarineTransform;

    private CameraMode currentMode;

    // Start is called before the first frame update
    void Start()
    {
        ChangeCameraMode(CameraMode.Walking);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCameraMode(CameraMode cameraMode)
    {
        currentMode = cameraMode;

        if (currentMode == CameraMode.Walking)
        {
            walkingModeCamera.m_Priority = 100;
            drivingModeCamera.m_Priority = 0;
        }
        else if (currentMode == CameraMode.Driving)
        {
            walkingModeCamera.m_Priority = 0;
            drivingModeCamera.m_Priority = 100;
        }
    }
}
