using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Camera : MonoBehaviour
{
    public enum CameraMode
    {
        playerMode, drivingMode, controllingMode
    }

    public Vector3 playerCameraOffset;
    public Vector3 driveSubmarineCameraOffset;
    public Vector3 controlArmsCameraOffset;

    public Quaternion playerCameraRotation;
    public Quaternion driveSubmarineCameraRotation;
    public Quaternion controlArmsCameraRotation;

    public Transform playerTransform;
    public Transform submarineTransform;

    private CameraMode currentMode;

    public static Camera _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMode = CameraMode.playerMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMode == CameraMode.playerMode)
        {
            transform.position = new Vector3(submarineTransform.position.x + playerCameraOffset.x, submarineTransform.position.y + playerCameraOffset.y, submarineTransform.position.z + playerCameraOffset.z);
            //transform.rotation = new Quaternion(submarineTransform.rotation.x + playerCameraRotation.x, submarineTransform.rotation.y + playerCameraRotation.y, submarineTransform.rotation.z + playerCameraRotation.z, submarineTransform.rotation.w + playerCameraRotation.w);
        }
        else if (currentMode == CameraMode.drivingMode)
        {
            transform.position = new Vector3(submarineTransform.position.x + driveSubmarineCameraOffset.x, submarineTransform.position.y + driveSubmarineCameraOffset.y, submarineTransform.position.z + driveSubmarineCameraOffset.z);
            transform.rotation = new Quaternion(submarineTransform.rotation.x + driveSubmarineCameraRotation.x, submarineTransform.rotation.y + driveSubmarineCameraRotation.y, submarineTransform.rotation.z + driveSubmarineCameraRotation.z, submarineTransform.rotation.w + driveSubmarineCameraRotation.w);
        }
        else if (currentMode == CameraMode.controllingMode)
        {
            transform.position = new Vector3(submarineTransform.position.x + controlArmsCameraOffset.x, submarineTransform.position.y + controlArmsCameraOffset.y, submarineTransform.position.z + controlArmsCameraOffset.z);
            transform.rotation = new Quaternion(submarineTransform.rotation.x + controlArmsCameraRotation.x, submarineTransform.rotation.y + controlArmsCameraRotation.y, submarineTransform.rotation.z + controlArmsCameraRotation.z, submarineTransform.rotation.w + controlArmsCameraRotation.w);
        }
    }

    public void ChangeCameraMode(CameraMode cameraMode)
    {
        currentMode = cameraMode;
    }
}
