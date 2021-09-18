using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbine : MonoBehaviour
{

    private Vector3 angularVelocity;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 300;
        angularVelocity = (transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager._instance.currentGameState == GameManager.GameState.drivingState)
        {
            transform.Rotate(angularVelocity * Time.deltaTime);
        }
    }
}
