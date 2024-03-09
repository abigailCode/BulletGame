using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private float startTime;
    private bool gameRunning;

    void Start()
    {
        startTime = Time.time;
        gameRunning = true;
    }

    void Update()
    {
        if (gameRunning)
        {
            float elapsedTime = Time.time - startTime;
            string minutes = ((int)elapsedTime / 60).ToString("00");
            string seconds = (elapsedTime % 60).ToString("00");
            GetComponent<TMP_Text>().text = minutes + ":" + seconds;
        }
    }

    public void StopTimer()
    {
        gameRunning = false;
    }
}
