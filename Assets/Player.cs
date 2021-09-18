using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LEGOMinifig;

public class Player : MonoBehaviour
{

    private bool playerFrozen;

    public MinifigController minifigController;
    public CharacterController charController;

    public Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerFrozen)
            //transform.localPosition = oldPosition;
    }

    public void FreezePlayer() {
        oldPosition = transform.position;
        minifigController.SetInputEnabled(false);
        charController.enabled = false;
        playerFrozen = true;
    }

    public void UnFreezePlayer()
    {
        minifigController.SetInputEnabled(true);
        playerFrozen = false;
        charController.enabled = true;
    }
}
