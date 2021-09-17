using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Camera : MonoBehaviour
{
    public enum CameraMode {
        playerMode, drivingMode
    }

    public Vector3 playerCameraOffset;
    public Vector3 submarineCameraOffset;

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
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 25, playerTransform.position.z - 10);
        }
        else if (currentMode == CameraMode.drivingMode)
        {
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 50, playerTransform.position.z - 20);
        }
    }

    public void ChangeCameraMode(CameraMode cameraMode) {
        currentMode = cameraMode;
    }
}
