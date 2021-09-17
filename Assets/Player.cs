using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LEGOMinifig;

public class Player : MonoBehaviour
{
    public MinifigController minifigController;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FreezePlayer() {
        minifigController.SetInputEnabled(false);
    }

    public void UnFreezePlayer()
    {
        minifigController.SetInputEnabled(true);
    }
}
