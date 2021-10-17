using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] GameObject keyFab;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] Word word;
    [SerializeField] Canvas dacanv;
    [SerializeField] GameObject timerPreFab;
    bool GoldenRain;

    public bool GetGoldenRain() { return GoldenRain; }

    //makes boolean goldenrain true for an amount of time
    IEnumerator GoldenRainCoroutine()
    {
        GoldenRain = true;
        yield return new WaitForSeconds(word.GetLength()); // Word.GetLength() returns (the number of length upgrades you have plus 1) times 5.
        GoldenRain = false;
    }

    public void StartGoldenRain()
    {
        GameObject timer = Instantiate(timerPreFab, Vector3.zero, Quaternion.identity, dacanv.transform); //instantiates timer object. the reason dacanv.transform is used is to make the timer a child of the 
        Timer timerscript = timer.GetComponent<Timer>();
        timerscript.StartTimer((int)word.GetLength(), new Vector3(-100, -260, 0)); //starts the timer for a golden rain using the same amount of time as the length of the goldenrain coroutine
        StartCoroutine(GoldenRainCoroutine());
    }

    void Start()
    {
        GoldenRain = false; 
    }

    public void deployKey(char a)
    {
        //loads the sprite image based on the name of the picture using the character passed in to the function
        string path = "keys/key_";
        if (GoldenRain) path = "keys/gold_"; // damn this is kinda sick so basically u got this path thing so like all the key images are in the 
                                             // same directory but they have very slightly different names and based on the char passed into this function it chooses that image
        Sprite newSprite = Resources.Load<Sprite>(path + a.ToString()); // the resources.load thing took me so long to figure out but it goes into the /resources directory in the unity project and looks for the image with that name. not really sure what happens when it can't find one...

        //randomly selects one of the waypoints to spawn the key at // THROWBACK!! doesn't do that anymore it was weird to do that when I could just choose a random x value and a known y value above the screen lol
        Vector3 waypoint = new Vector3(Random.Range(-7.0f, 7.0f), 6f, 1f);

        //actually instantiates the key prefab at the location of the waypoint chosen
        GameObject newKey = Instantiate(keyFab, waypoint, Quaternion.identity);

        //sets the sprite of the new key object to the sprite selected earlier
        newKey.GetComponent<SpriteRenderer>().sprite = newSprite;

        //generates and sets random size for each key
        float scale = UnityEngine.Random.Range(minScale, maxScale);
        newKey.transform.localScale = new Vector3(scale, scale, newKey.transform.localScale.z);

    }

}
