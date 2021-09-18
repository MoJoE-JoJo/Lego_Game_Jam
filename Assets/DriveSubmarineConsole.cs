using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSubmarineConsole : MonoBehaviour
{
    public float speed;
    public float activationDistance;

    public Player player;
    public Submarine submarine;

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Current player mode: " + player.GetPlayerState());
            Debug.Log("Current game state: " + GameManager._instance.currentGameState);

            var distToPlayer = Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition);

            if (distToPlayer < activationDistance && GameManager._instance.currentGameState == GameManager.GameState.walkingState)
            {
                Debug.Log("Driving mode activated!");
                player.FreezePlayer();
                //player.SetPlayerState(Player.PlayerState.drivingMode);
                GameManager._instance.currentGameState = GameManager.GameState.drivingState;
                camera.GetComponent<Camera>().ChangeCameraMode(Camera.CameraMode.drivingMode);
            }
            else if(GameManager._instance.currentGameState == GameManager.GameState.drivingState)
            {
                Debug.Log("Walking mode activated!");
                player.UnFreezePlayer();
                //player.SetPlayerState(Player.PlayerState.walkingMode);
                GameManager._instance.currentGameState = GameManager.GameState.walkingState;
                camera.GetComponent<Camera>().ChangeCameraMode(Camera.CameraMode.playerMode);
            }
        }

    }

    private void FixedUpdate()
    {
        if (GameManager._instance.currentGameState == GameManager.GameState.drivingState) {

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
