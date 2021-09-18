using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{
    private float leftExtend;
    private float rightExtend;

    public float activationDistance;
    public Player player;

    //public GameObject leftArm;
    public GameObject rightArm;

    //private Vector3 initialPosLeftArm;
    private Vector3 initialPosRightArm;
    //private Vector3 extendedPosLeftArm;
    private Vector3 extendedPosRightArm;

    // Start is called before the first frame update
    void Start()
    {
        //initialPosLeftArm = leftArm.transform.position;
        initialPosRightArm = rightArm.transform.position;

        //extendedPosLeftArm = leftArm.transform.forward * 10;
        extendedPosRightArm = rightArm.transform.forward * 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            var distToPlayer = Vector3.Distance(player.transform.localPosition, gameObject.transform.localPosition);

            if (distToPlayer < activationDistance && player.currentState == Player.PlayerState.walkingMode)
            {
                Debug.Log("ControllingMode activated!");
                player.FreezePlayer();
                player.currentState = Player.PlayerState.controllingMode;
                Camera._instance.ChangeCameraMode(Camera.CameraMode.controllingMode);
            }
            else
            {
                player.UnFreezePlayer();
                player.currentState = Player.PlayerState.walkingMode;
                Camera._instance.ChangeCameraMode(Camera.CameraMode.playerMode);
            }
        }
        /*
        if (player.currentState == Player.PlayerState.controllingMode)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rightExtend += Time.deltaTime / 4;
                if (rightExtend >= 1.0f)
                {
                    rightExtend = 1.0f;
                }
                rightArm.transform.position = Vector3.Lerp(extendedPosRightArm, initialPosRightArm, rightExtend);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rightExtend -= Time.deltaTime / 4;
                if (rightExtend <= 0.0f)
                {
                    rightExtend = 0.0f;
                }
                rightArm.transform.position = Vector3.Lerp(extendedPosRightArm, initialPosRightArm, rightExtend);
            }
        }
        */
    }
}
