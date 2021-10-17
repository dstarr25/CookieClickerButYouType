using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdditionText : MonoBehaviour
{
    RectTransform rct;
    TextMeshProUGUI text;
    Rigidbody2D rbd;
    long number;

    [SerializeField] float speed;
    [SerializeField] float slowdown;


    //waits 2 seconds then destroys this additiontext object
    IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    //this is public so that when the additiontext is initialized the num can be specified right after. 
    // ^^ that happens in the Word.cs script when Word.AddToScore(long addition) is called
    public void SetNumber(long num)
    {
        number = num;
    }

    // Start is called before the first frame update
    void Start()
    {
        //moves the additiontext to a random location on the screen in a range -> (UnityEngine.Random.Range(-450f, 450f), UnityEngine.Random.Range(-210f, 210f)) 
        // maybe should CHANGE to not use explicit values?
        // sets text to +2 or +{number} ya know and sets the rigidbody2d's velocity to speed, which is a serialized variable that can be edited in unity
        rct = GetComponent<RectTransform>();
        rct.localPosition = new Vector3(UnityEngine.Random.Range(-450f, 450f), UnityEngine.Random.Range(-210f, 210f), -0.5f);
        text = GetComponent<TextMeshProUGUI>();
        //if the addition number is negative, don't add a plus sign in front of it and make it red, otherwise put the plus sign and keep it white.
        if (number < 0) 
        {
            text.text = "";
            text.color = new Color(1,0,0,1);
        }
        else 
            text.text = "+";
        
        text.text += number.ToString();
        rbd = GetComponent<Rigidbody2D>();
        rbd.velocity = new Vector2(0, speed);
        //waits 2s then destroys this additiontext object
        StartCoroutine(DestroyLater());

    }

}
