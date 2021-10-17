using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{

    [SerializeField] Word player;
    [SerializeField] AudioClip chaching;

    //VOWEL upgrades
    [SerializeField] TextMeshProUGUI vowelQuantity;
    [SerializeField] TextMeshProUGUI vowelPrice;
    int vBoosts;
    long vPrice;


    void UpdateVowelInfo()
    {
        vowelPrice.text = "cost: " + vPrice;
        vowelQuantity.text = "x " + vBoosts;
    }

    public void BuyVowelBoost()
    {
        if (player.BuyVBonus(vPrice))
        {
            vPrice *= 2;
            AudioSource.PlayClipAtPoint(chaching, new Vector3(0, 0, -10));
            vBoosts++;
            UpdateVowelInfo();

        }

    }

    //CONSONANT upgrades
    [SerializeField] TextMeshProUGUI consonantQuantity;
    [SerializeField] TextMeshProUGUI consonantPrice;
    int cBoosts;
    long cPrice;

    void UpdateConsonantInfo()
    {
        consonantPrice.text = "cost: " + cPrice;
        consonantQuantity.text = "x " + cBoosts;
    }

    public void BuyConsonantBoost()
    {
        if (player.BuyCBonus(cPrice))
        {
            cPrice *= 2;
            AudioSource.PlayClipAtPoint(chaching, new Vector3(0, 0, -10));
            cBoosts++;
            UpdateConsonantInfo();

        }

    }


    //STREAK upgrades
    [SerializeField] TextMeshProUGUI streakQuantity;
    [SerializeField] TextMeshProUGUI streakPrice;

    public int sUpgrades;
    long sPrice;

    void UpdateStreakInfo()
    {
        streakPrice.text = "cost: " + sPrice;
        streakQuantity.text = "x " + sUpgrades;
    }

    public void BuyStreakUpgrade()
    {
        if (player.BuySBonus(sPrice))
        {
            sPrice += 500;
            AudioSource.PlayClipAtPoint(chaching, new Vector3(0, 0, -10));
            sUpgrades++;
            UpdateStreakInfo();

        }

    }

    //GOLD upgrades
    [SerializeField] TextMeshProUGUI goldQuantity;
    [SerializeField] TextMeshProUGUI goldPrice;

    public int gUpgrades;
    long gPrice;

    void UpdateGoldInfo()
    {
        goldPrice.text = "cost: " + gPrice;
        goldQuantity.text = "x " + gUpgrades;
    }

    public void BuyGoldUpgrade()
    {
        if (player.BuyGBonus(gPrice))
        {
            gPrice *= 10;
            AudioSource.PlayClipAtPoint(chaching, new Vector3(0, 0, -10));
            gUpgrades++;
            UpdateGoldInfo();

        }

    }

    // GOLD LENGTH upgrades

    [SerializeField] TextMeshProUGUI lengthQuantity;
    [SerializeField] TextMeshProUGUI lengthPrice;

    public int lUpgrades;
    long lPrice;

    void UpdateLengthInfo()
    {
        lengthPrice.text = "cost: " + lPrice;
        lengthQuantity.text = "x " + lUpgrades;
    }

    public void BuyLengthUpgrade()
    {
        if (player.BuyLBonus(lPrice))
        {
            lPrice *= 10;
            AudioSource.PlayClipAtPoint(chaching, new Vector3(0, 0, -10));
            lUpgrades++;
            UpdateLengthInfo();

        }

    }


    // Start is called before the first frame update, sets starting prices and number of upgrades for each one, and their text boxes
    void Start()
    {
        vPrice = 300;
        vBoosts = 0;
        vowelPrice.text = "cost: " + vPrice;

        cPrice = 400;
        cBoosts = 0;
        consonantPrice.text = "cost : " + cPrice;

        sPrice = 1000;
        sUpgrades = 0;
        streakPrice.text = "cost : " + sPrice;

        gPrice = 10000;
        gUpgrades = 0;
        goldPrice.text = "cost : " + gPrice;

        lPrice = 10000;
        lUpgrades = 0;
        lengthPrice.text = "cost: " + lPrice;


    }

}
