using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPicker : MonoBehaviour
{
    public float fuelPickup = 2.5f;
    public LegoController legoController;
    public AudioSource pickup;
    // Start is called before the first frame update
    void Start()
    {
        legoController = GameObject.FindGameObjectWithTag("LegoController").GetComponent<LegoController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("YOLO");
        if (other.CompareTag("Trash"))
        {
            pickup.Play();
            legoController.BlinkLights();
            Destroy(other.gameObject);
            GameManager._instance.AddToScore(1);
            GameManager._instance.fuel += fuelPickup;
        }
    }
}
