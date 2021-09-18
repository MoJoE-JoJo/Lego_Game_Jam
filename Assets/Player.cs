using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LEGOMinifig;

public class Player : MonoBehaviour
{
    public enum PlayerState {
        walkingMode, drivingMode, controllingMode
    }

    private bool playerFrozen;

    public PlayerState currentState;

    public MinifigController minifigController;

    public Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerFrozen)
            transform.position = oldPosition;
    }

    public void FreezePlayer() {
        oldPosition = transform.position;
        minifigController.SetInputEnabled(false);
        playerFrozen = true;
    }

    public void UnFreezePlayer()
    {
        minifigController.SetInputEnabled(true);
        playerFrozen = false;
    }
}
