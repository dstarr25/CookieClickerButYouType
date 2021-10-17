using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI topWord;
    [SerializeField] TextMeshProUGUI bottomWord;
    [SerializeField] TextMeshProUGUI underWord;
    [SerializeField] float totalTime;
    [SerializeField] float timeInbetween;
    private float fadeLength;
    private float time;
    private bool transitioning;
    private float transitionStartTime;
    [SerializeField] float transitionTime;

    // Start is called before the first frame update
    void Start()
    {
        time = totalTime;
        fadeLength = (totalTime - 2f * timeInbetween) / 3f;
        topWord.color = new Color(1,1,1,0);
        bottomWord.color = new Color(1,1,1,0);
        underWord.color = new Color(1,1,1,0);
        transitioning = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) Application.Quit();

        //fades the three different text objects in one at a time
        if (time > totalTime - fadeLength) 
        {
            float c = (fadeLength - (time - 2*fadeLength - 2*timeInbetween)) / fadeLength;
            topWord.color = new Color(1,1,1,c);
        } 
        else if (time > fadeLength + timeInbetween && time <= totalTime - fadeLength - timeInbetween) 
        {
            float c = (fadeLength - (time - fadeLength - timeInbetween)) / fadeLength;
            bottomWord.color = new Color(1,1,1,c);
        } 
        else if (time > 0 && time <= fadeLength) 
        {
            float c = (fadeLength - time) / fadeLength;
            underWord.color = new Color(0.905f,0.898f,0.373f,c);
        }



        //if all the words have already faded in and player presses space, starts transition to normal game scene
        if (time <= 0 && Input.GetKeyDown("space") && !transitioning)
        {
            transitioning = true;
            transitionStartTime = time;
        }

        //everything fades away after transitionTime seconds if the transition bool is true meaning that space has been pressed
        if (transitioning) 
        {
            float c = (transitionTime - (transitionStartTime - time)) / transitionTime;
            if (c <= 0)
            {
                SceneManager.LoadScene(1);
            }
            topWord.color = new Color(1,1,1,c);
            bottomWord.color = new Color(1,1,1,c);
            underWord.color = new Color(0.905f,0.898f,0.373f,c);
        }

        time -= Time.deltaTime; //subtracts how much time went by in a single frame so that no matter your fps the time variable will go down by 1 every second.
    }
}
