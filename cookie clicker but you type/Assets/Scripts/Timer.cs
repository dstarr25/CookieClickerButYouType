using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    TextMeshProUGUI timerText;
    int time;
    RectTransform rtf;

    // called by Keys.StartGOldenRain()
    public void StartTimer(int toime, Vector3 position) 
    {
        rtf = GetComponent<RectTransform>();
        rtf.anchoredPosition = position;
        time = toime;
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = time.ToString();
        StartCoroutine(TimerClock());
    }

    // coroutine that counts down depending on the time global variable
    IEnumerator TimerClock()
    {
        for (int i = time; i > 0; i--) 
        {
            yield return new WaitForSeconds(1f); // this can only be called in a coroutine so that's why I had to make it in one :P
            time--;                              // takes away from time each second. this is only used to update the text inside the timer
            timerText.text = time.ToString();    // updates timer text
            if (time == 0) Destroy(gameObject);  // destroys itself if time is 0
        }
    }


}
