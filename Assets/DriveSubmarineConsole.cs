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
    public GameObject roof;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            var dist = Vector3.Distance(player.transform.position, transform.position);

            Debug.Log("Current game state: " + GameManager._instance.currentGameState);
            Debug.Log("Dist to player: " + dist);

            var distToPlayer = Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition);

            if (distToPlayer < activationDistance && GameManager._instance.currentGameState == GameManager.GameState.walkingState)
            {
                Debug.Log("Driving mode activated!");
                player.FreezePlayer();
                GameManager._instance.currentGameState = GameManager.GameState.drivingState;
                camera.GetComponent<CameraController>().ChangeCameraMode(CameraController.CameraMode.Driving);
                roof.SetActive(true);
            }
            else if(GameManager._instance.currentGameState == GameManager.GameState.drivingState)
            {
                Debug.Log("Walking mode activated!");
                player.UnFreezePlayer();
                GameManager._instance.currentGameState = GameManager.GameState.walkingState;
                camera.GetComponent<CameraController>().ChangeCameraMode(CameraController.CameraMode.Walking);
                roof.SetActive(false);
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
