using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldBar : MonoBehaviour
{
    [SerializeField] int startingKeys; // num keys needed to trigger golden shower
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI sliderText;
    [SerializeField] GameObject goldenKeyFab;

    [SerializeField] float gMinX;
    [SerializeField] float gMaxX;
    [SerializeField] float gY;
    [SerializeField] float gZ;

    [SerializeField] AudioClip angelSounds;
    [SerializeField] Keys keys;
    



    void CreateGoldenKey(char a)
    {
        float gX = Random.Range(gMinX, gMaxX); // creates random x position for the golden key to start at
        GameObject newKey = Instantiate(goldenKeyFab, new Vector3(gX, gY, gZ), Quaternion.identity); //Quaternion.identity is just the default rotation quaternion value
        GoldKey newKeyScript = newKey.GetComponent<GoldKey>();
        newKeyScript.SetUp(Resources.Load<Sprite>("keys/gold_" + a.ToString()), a); //SetUp function in GoldKey.cs 
        AudioSource.PlayClipAtPoint(angelSounds, new Vector3(0, 0, -10)); //plays angel sound

    }

    int cooldown;
    int currentKeys;

    //  updates the textbox next to slider with the number of keys they have left before golden 
    //  shower lol and also sets slider value based on the current number of keys theyve typed
    void UpdateSlider()
    {
        sliderText.text = (startingKeys - currentKeys).ToString();
        slider.value = (float)currentKeys / startingKeys;

    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;

        currentKeys = 0;
        UpdateSlider();

    }


    //adds one to current keys theyve typed, if its high enough (startingkeys) triggers golden shower lol
    public void AddToBar()
    {
        if (!keys.GetGoldenRain())
        {
            currentKeys += 1;
            UpdateSlider();
            if (currentKeys == startingKeys)
            {
                CreateGoldenKey(Random.Range(0, 10).ToString()[0]); //[0] is used because creategoldenkey takes in a char not a string
                cooldown = startingKeys;
                sliderText.text = startingKeys.ToString();

            }

        }

    }


    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0 && !keys.GetGoldenRain()) // only cools down and stuff if 
        {
            currentKeys -= 1;
            cooldown -= 1;
            UpdateSlider(); //until cooldown is 0 it drops currentkeys by 1 every frame. if i was deditated enough i might CHANGE this so that it takes the same amount of time no matter your fps using Time.deltaTime;

        }
    }
}
