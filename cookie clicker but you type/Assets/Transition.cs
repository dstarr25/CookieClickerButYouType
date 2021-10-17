using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Transition : MonoBehaviour
{


    [SerializeField] Image background;

    [SerializeField] float transitionTime;

    private float time;

    public bool transitioning;

    // Start is called before the first frame update
    void Start()
    {

        time = transitionTime;
        background.color = new Color(1,1,1,1);

    }

    // Update is called once per frame
    // causes the alpha of the background image to linearly drop to zero after {time} seconds
    void Update()
    {
        float c = time / transitionTime;
        if (c < 0) Destroy(gameObject);
        background.color = new Color(1,1,1,c); 
        time -= Time.deltaTime;
    }
}
