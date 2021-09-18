using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = GameObject.FindGameObjectWithTag("LegoController").GetComponent<LegoController>().endScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
