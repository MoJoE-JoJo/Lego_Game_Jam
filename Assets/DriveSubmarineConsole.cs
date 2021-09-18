using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSubmarineConsole : MonoBehaviour
{
    public float fuelUsage;
    public float speed;
    public float activationDistance;

    public Player player;
    public Submarine submarine;
    public LegoController legoController;

    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        legoController = GameObject.FindGameObjectWithTag("LegoController").GetComponent<LegoController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Current game state: " + GameManager._instance.currentGameState);

            var distToPlayer = Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition);

            if (distToPlayer < activationDistance && GameManager._instance.currentGameState == GameManager.GameState.walkingState)
            {
                Debug.Log("Driving mode activated!");
                player.FreezePlayer();
                GameManager._instance.currentGameState = GameManager.GameState.drivingState;
                camera.GetComponent<Camera>().ChangeCameraMode(Camera.CameraMode.drivingMode);
            }
            else if(GameManager._instance.currentGameState == GameManager.GameState.drivingState)
            {
                Debug.Log("Walking mode activated!");
                player.UnFreezePlayer();
                GameManager._instance.currentGameState = GameManager.GameState.walkingState;
                camera.GetComponent<Camera>().ChangeCameraMode(Camera.CameraMode.playerMode);
                legoController.gameMode = GAMEMODES.MINIFIG;
            }
        }

    }

    private void FixedUpdate()
    {
        if (GameManager._instance.currentGameState == GameManager.GameState.drivingState)
        {
            var rightLeverVal = legoController.GetRightLeverValue();
            var leftLeverVal = legoController.GetLeftLeverValue();
            var steeringVal = legoController.GetSteeringWheelValue();

            if (rightLeverVal != 0 || leftLeverVal != 0 || steeringVal != 0)
            {
                var val = Mathf.Abs(rightLeverVal) + Mathf.Abs(leftLeverVal) + Mathf.Abs(steeringVal);
                val /= 10;
                GameManager._instance.SpendFuel(fuelUsage*Time.fixedDeltaTime*val);
            }
            Debug.Log(legoController.GetRightLeverValue());
            Debug.Log(legoController.GetSteeringWheelValue());
            Debug.Log(legoController.GetLeftLeverValue());
            legoController.gameMode = GAMEMODES.SUBMARINE;
            submarine.AddForce(transform.forward * speed * rightLeverVal);
            submarine.AddTorque(transform.up * speed * steeringVal);
            submarine.AddForce(transform.up * speed * leftLeverVal);
        }
    }
}
