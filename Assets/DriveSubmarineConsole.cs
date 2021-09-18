using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSubmarineConsole : MonoBehaviour
{
    public float speed;
    public float activationDistance;

    public Player player;
    public Submarine submarine;

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
            var distToPlayer = Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition);

            if (distToPlayer < activationDistance && !player.currentlyDriving && !player.currentlyControllingArms)
            {
                player.FreezePlayer();
                player.currentlyDriving = true;
                Camera._instance.ChangeCameraMode(Camera.CameraMode.drivingMode);
            }
            else
            {
                player.UnFreezePlayer();
                player.currentlyDriving = false;
                Camera._instance.ChangeCameraMode(Camera.CameraMode.playerMode);
            }
        }

    }

    private void FixedUpdate()
    {
        if (currentlyDriving) {

            var rb = submarine.GetComponent<Rigidbody>();

            if (Input.GetKey(KeyCode.W))
            {
                submarine.AddForce(transform.forward * speed);
                //rb.AddForce(transform.forward * speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                submarine.AddForce(-transform.forward * speed);
                //rb.AddForce(-transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                submarine.AddTorque(-transform.up * speed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                submarine.AddTorque(transform.up * speed);
            }
            if (Input.GetKey(KeyCode.Q)) {
                submarine.AddForce(transform.up * speed);
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                submarine.AddForce(-transform.up * speed);
            }

        }
    }
}
