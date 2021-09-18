using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject trash1;
    public GameObject trash2;
    public GameObject trash3;

    public int amountOfTrash;

    public float borderX, borderY, borderZ;
    public float deadZoneX, deadZoneY, deadZoneZ;

    public static GameManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        //Spawn trash in front
        for (int i = 0; i < amountOfTrash; i++) {
            var randomX = Random.Range(-borderX, borderX);
            var randomY = Random.Range(-borderY, borderY);
            var randomZ = Random.Range(-borderZ, borderZ);

            while (WithinSubmarine(randomX, randomY, randomZ)) {
                randomX = Random.Range(-borderX, borderX);
                randomY = Random.Range(-borderY, borderY);
                randomZ = Random.Range(-borderZ, borderZ);
            }

            var rotationX = Random.Range(0, 360);
            var rotationY = Random.Range(0, 360);
            var rotationZ = Random.Range(0, 360);
            var rotationW = Random.Range(0, 360);

            var trashType = (int)Random.Range(1, 4);

            GameObject trashToSpawn;

            if (trashType == 1)
            {
                trashToSpawn = Instantiate(trash1);
            }
            else if (trashType == 2)
            {
                trashToSpawn = Instantiate(trash2);
            }
            else
            {
                trashToSpawn = Instantiate(trash3);
            }

            trashToSpawn.transform.position = new Vector3(randomX, randomY, randomZ);

            trashToSpawn.transform.rotation = new Quaternion(rotationX, rotationY, rotationZ, rotationW);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool WithinSubmarine(float x, float y, float z)
    {
        if (-deadZoneX <= x && x <= deadZoneX)
            return true;
        if (-deadZoneY <= y && y <= deadZoneY)
            return true;
        if (-deadZoneZ <= z && z <= deadZoneZ)
            return true;
        return false;
    }
}
