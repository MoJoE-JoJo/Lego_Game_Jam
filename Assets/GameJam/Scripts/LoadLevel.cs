using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public string sceneName = "";
    public bool readyToLoad = false;
    public bool tryToStart = false;
    public LegoController lc;
    public GameObject gettingReady;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lc.controllerInitialized) readyToLoad = true;
        if(tryToStart && readyToLoad)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadScene()
    {
        tryToStart = true;
        gettingReady.SetActive(true);
    }
}
