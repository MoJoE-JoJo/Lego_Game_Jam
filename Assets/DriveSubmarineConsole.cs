using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSubmarineConsole : MonoBehaviour
{
    private bool currentlyDriving;

    public float speed;

    public GameObject player;
    public GameObject submarine;

    // Start is called before the first frame update
    void Start()
    {
        currentlyDriving = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            var distToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
            if (distToPlayer < 20.0f)
            {
                //Freeze player and move camera;
                if (!currentlyDriving)
                {
                    Debug.Log("You clicked the drive sub button!");
                    player.GetComponent<Player>().FreezePlayer();
                    currentlyDriving = true;
                    Camera._instance.ChangeCameraMode(Camera.CameraMode.drivingMode);
                }
                else
                {
                    Debug.Log("Not driving submarine anymore");
                    player.GetComponent<Player>().UnFreezePlayer();
                    currentlyDriving = false;
                    Camera._instance.ChangeCameraMode(Camera.CameraMode.playerMode);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentlyDriving) {

            var rb = submarine.GetComponent<Rigidbody>();

            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.forward * speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddTorque(-transform.up * speed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rb.AddTorque(transform.up * speed);
            }
            if (Input.GetKey(KeyCode.Q)) {
                rb.AddForce(transform.up * speed);
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                rb.AddForce(-transform.up * speed);
            }

        }
    }
}
