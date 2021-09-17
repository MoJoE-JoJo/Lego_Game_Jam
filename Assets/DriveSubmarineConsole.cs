using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveSubmarineConsole : MonoBehaviour
{
    private bool currentlyDriving;

    public GameObject player;
    public GameObject submarine;

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
            var distToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
            if (distToPlayer < 20.0f)
            {
                //Freeze player and move camera;
                if (!currentlyDriving)
                {
                    Debug.Log("You clicked the drive sub button!");
                    player.GetComponent<Player>().FreezePlayer();
                    currentlyDriving = true;
                }
                else
                {
                    Debug.Log("Not driving submarine anymore");
                    player.GetComponent<Player>().UnFreezePlayer();
                    currentlyDriving = false;
                }
            }
        }
    }
}
