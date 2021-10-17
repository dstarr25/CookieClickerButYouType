using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Versioning;
using UnityEngine.SceneManagement;

public class Word : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI chararray;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI streakText;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] TextAsset wordsFile;
    [SerializeField] AudioClip spaceBarSound;
    [SerializeField] AudioClip errorSound;
    [SerializeField] GameObject KeysObject;
    [SerializeField] Shop shop;
    [SerializeField] GoldBar goldBar;
    [SerializeField] GameObject additionPrefab;
    [SerializeField] Transform daCanvas;
    

    
    string[] wordArray; //holds all the words from the words.txt file
    public long score;
    long vBonus;
    long cBonus;
    long wfBonus; //this is the bonus you get when you finish a word and press space. Might add an upgrade for it later so you get more keys for finishing a word?
    int streak;
    int errorPenalty; //added this in case I ever wanted to change the error penalty for gameplay balance purposes. ATM I don't feel like it's necessary

    //gets length of golden rain based on how many times you've upgraded it
    public float GetLength() 
    {
        return (shop.lUpgrades + 1) * 5f;
    }


    // holy shoot i was so proud of myself for figuring this out cuz i just learned about recursion and i wasn't super good at it but this applied it in a super epic way!! crazy.
    // might CHANGE later to use an array of streak lengths - like {20, 60, 100} etc. - or something instead of 25 * whatever but I kinda like the linearity of it
    //basically this uses the amount of streak upgrades you have and sets the streak multiplier according to your current streak.
    int GetStreakMultiplier(int streak, int sUpgrades)
    {
        if (streak >= 25 * sUpgrades) 
            return sUpgrades + 1;

        else
            return GetStreakMultiplier(streak, sUpgrades - 1);
    }

    //returns goldenrain multiplier, only returns something other than 1 if goldenrain is on, its the upgrades+1 times 10
    int GetGoldMultiplier() {
        if (!KeysObject.GetComponent<Keys>().GetGoldenRain()) 
            return 1;
        return (shop.gUpgrades + 1) * 10;
    }

    public long GetScore()
    {
        return score;
    }

    //adds to key count and takes away from keys left on gold bar, creates the additiontext like +1 thingy and sets its number 
    public void AddToScore(long addition)
    {
        score += addition;
        if (addition > 0) goldBar.AddToBar();
        GameObject additionText = Instantiate(additionPrefab, Vector3.zero, Quaternion.identity, daCanvas);
        additionText.GetComponent<AdditionText>().SetNumber(addition);
        TextMeshProUGUI atext = additionText.GetComponent<TextMeshProUGUI>();
        if (KeysObject.GetComponent<Keys>().GetGoldenRain()) atext.color = new Color32(255, 255, 100, 255); // makes additiontext yellow if goldenrain is on!!

    }
    
    /* BUY FUNCTIONS FOR ALL UPGRADES: 
    
    if your key count is high enough to buy the thing, 
        buy it!!
        take away from ur key count
        call update text method which changes key count text and streak text
        returns true
    otherwise returns false!!
    
    */

    public bool BuyVBonus(long price)
    {
        if (score >= price)
        {
            vBonus *= 2;
            score -= price;
            UpdateText();
            return true;
        }
        return false;
    }

    public bool BuyCBonus(long price)
    {
        if (score >= price)
        {
            cBonus *= 2;
            score -= price;
            UpdateText();
            return true;
        }
        return false;
    }

    public bool BuySBonus(long price)
    {
        if (score >= price)
        {
            score -= price;
            UpdateText();
            return true;
        }
        return false;
    }

    public bool BuyGBonus(long price) 
    {
        if (score >= price)
        {
            score -= price;
            UpdateText();
            return true;
        }
        return false;
    }

    public bool BuyLBonus(long price) {
        if (score >= price) 
        {
            score -= price;
            UpdateText();
            return true;
        }
        return false;
    }

    public void BuyWFBonus()
    {
        wfBonus *= 4;
    }



    //remove last character from the end of a string - used to take off \n from each word in the list
    string NoEnd(string str) => str.Substring(0,str.Length-1);

    //remove first letter from the current word and play a press sound
    string Pop(string str)
    {
        AudioSource.PlayClipAtPoint(audioClips[UnityEngine.Random.Range(0, audioClips.Count )], new Vector3(0, 0, -10));
        return str.Substring(1);
    }

    // Start is called before the first frame update
    void Start()
    {

        score = 0;
        streak = 0;

        UpdateText();

        errorPenalty = 1;
        vBonus = 1;
        cBonus = 1;
        wfBonus = 1;

        wordArray = wordsFile.text.Split("\n"[0]); //turns the words text file into an array of words
        chararray.text = NoEnd(wordArray[UnityEngine.Random.Range(0, wordArray.Length)]); //sets the first word to a random one in the array
    }


    //sets streak and score text to the right values. 
    void UpdateText()
    {
        streakText.text = "Streak: " + streak.ToString() + " (" + GetStreakMultiplier(streak, shop.sUpgrades) + "x)";
        scoreText.text = score.ToString() + " keys";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            SceneManager.LoadScene(0);
        }
        //only does the following if there is at least one letter in the current displayed word
        if (chararray.text.Length >= 1)
        {
            //super long if - checks if you're pressing one of the letters or the spacebar
            if (Input.GetKeyDown("a") || Input.GetKeyDown("b") || Input.GetKeyDown("c") || Input.GetKeyDown("d") || Input.GetKeyDown("e") || Input.GetKeyDown("f") || Input.GetKeyDown("g") || Input.GetKeyDown("h") || Input.GetKeyDown("i") || Input.GetKeyDown("j") || Input.GetKeyDown("k") || Input.GetKeyDown("l") || Input.GetKeyDown("m") || Input.GetKeyDown("n") || Input.GetKeyDown("o") || Input.GetKeyDown("p") || Input.GetKeyDown("q") || Input.GetKeyDown("r") || Input.GetKeyDown("s") || Input.GetKeyDown("t") || Input.GetKeyDown("u") || Input.GetKeyDown("v") || Input.GetKeyDown("w") || Input.GetKeyDown("x") || Input.GetKeyDown("y") || Input.GetKeyDown("z") || Input.GetKeyDown("space"))
            {
                //if you're pressing the wrong letter or the space bar, plays error sound and takes away your error penalty from your key count and resets streak to 0, then updates text - score and streak
                if(!Input.GetKeyDown(chararray.text[0].ToString()))
                {
                    AudioSource.PlayClipAtPoint(errorSound, new Vector3(0, 0, -10));
                    score -= errorPenalty;
                    AddToScore(-errorPenalty);
                    if (score < 0) score = 0;
                    streak = 0;
                    UpdateText();
                }
                else
                // if you're pressing the right key:
                {
                    long scoreAddition;
                    // it checks if that key is a vowel or a consonant and sets the score addition accordingly.
                    if (chararray.text[0] == 'a' || chararray.text[0] == 'e' || chararray.text[0] == 'i' || chararray.text[0] == 'o' || chararray.text[0] == 'u')
                        scoreAddition = vBonus;
                    else
                        scoreAddition = cBonus;
                    // the score addition is then multiplied by the streak multiplier and the gold multiplier, which are both 1 if you don't have a streak and golden rain is off.
                    scoreAddition *= GetStreakMultiplier(streak, shop.sUpgrades);
                    scoreAddition *= GetGoldMultiplier();
                    // calls add to score method to give you more keys!! #epic
                    AddToScore(scoreAddition);

                    //deploys a key in the background based on the character pressed
                    KeysObject.GetComponent<Keys>().deployKey(chararray.text[0]);

                    //removes first letter from word, increases streak, updates text
                    chararray.text = Pop(chararray.text);
                    streak++;
                    UpdateText();
                }
            }
        }
        else
        //called if word length is zero
        {
            //if space pressed!
            if (Input.GetKeyDown("space"))
            {
                //finds a new word from wordarray and removes the \n from it, plays spacebarsound, adds to score based on word finisher bonus, updates text
                chararray.text = NoEnd(wordArray[UnityEngine.Random.Range(0, wordArray.Length)]);
                AudioSource.PlayClipAtPoint(spaceBarSound, new Vector3(0, 0, -10));
                AddToScore(wfBonus);
                UpdateText();
            }
        }
    }
}
