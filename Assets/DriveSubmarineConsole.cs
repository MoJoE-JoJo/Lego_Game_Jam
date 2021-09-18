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

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            var distToPlayer = Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition);

            if (distToPlayer < activationDistance && player.currentState == Player.PlayerState.walkingMode)
            {
                Debug.Log("Driving mode activated!");
                player.FreezePlayer();
                player.currentState = Player.PlayerState.drivingMode;
                Camera._instance.ChangeCameraMode(Camera.CameraMode.drivingMode);
            }
            else
            {
                player.UnFreezePlayer();
                player.currentState = Player.PlayerState.walkingMode;
                Camera._instance.ChangeCameraMode(Camera.CameraMode.playerMode);
            }
        }

    }

    private void FixedUpdate()
    {
        if (player.currentState == Player.PlayerState.drivingMode) {

            if (Input.GetKey(KeyCode.W))
            {
                submarine.AddForce(transform.forward * speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                submarine.AddForce(-transform.forward * speed);
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
