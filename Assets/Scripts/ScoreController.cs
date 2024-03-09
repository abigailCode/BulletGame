using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    
    private string totalTime="";

    void Start()
    {
        totalTime = PlayerPrefs.GetString("TotalTime");
        GetComponent<TMP_Text>().text = "TIME: " + totalTime;

    }

   
}
