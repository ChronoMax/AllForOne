using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitConfig : MonoBehaviour
{
    public static UnitConfig Instance;

    [HideInInspector]
    public Text[] valueText;
    [HideInInspector]
    public Slider[] sliders;

    public GameObject unitToSpawn;

    private int currentMoney, minCost = 10, price;
    float sliderAverage = 0;
    private Text moneyText, priceText, spawnBtnText, headerText;

    
    //Checksif there already is one instance of the script.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //Sets the default value for the text.
        ResetValues();
        UpdateConfig();
        SetValues();
        UpdatePrice();
    }

    private void UpdateConfig()
    {
        currentMoney = Gamemanager.Instance.currentMoney[Gamemanager.Instance.teamSelected];
        UIManager.Instance.UpdateUI();
    }


    //Changes the value if the value of the slider is changed.
    public void SliderChanged()
    {
        SetValues();
        UpdatePrice();
    }

    //Sets thew values of the sliders.
    private void SetValues()
    {
       //Sets all the values of the sliders.
        for (int i = 0; i < sliders.Length; i++)
        {
            valueText[i].text = sliders[i].value.ToString();
        }

        //Gets the average of all the sliders.
        sliderAverage = Map((int)sliders[0].value, 1, 100, 3,30) + Map((int)sliders[1].value, 1, 100, 2,20) + Map((int)sliders[2].value, 1, 100, 3,30) + Map((int)sliders[3].value, 1, 100, 2,20);
    }

    //User can click the spawn button.
    //When the button is pressed the price of the unit wil be subtracted of the current money of the player.
    //Turns of the UI element so the player can spawn the unit.
    public void SpawnButtonClicked()
    {
        if (currentMoney >= price)
        {
            currentMoney = currentMoney - price;
            //updates the money in the gamemanager.
            Gamemanager.Instance.currentMoney[Gamemanager.Instance.teamSelected] = currentMoney;
            UpdatePrice();

            UIManager.Instance.SwitchUnitSUI();
        }
    }

    //Aets the price of the object based on the values of the sliders.
    //Gets the spawnBtn text.
    private void UpdatePrice()
    {
        if (Gamemanager.Instance.unitConfig)
        {
            price = (int)sliderAverage;

            if (sliderAverage <= 10)
            {
                price = minCost;
            }

            spawnBtnText = GameObject.Find("SpawnBtnText").GetComponent<Text>();
            ChangeBtnText();

            //Sets all the text and vars like they should;
            moneyText = GameObject.Find("CurrentMoneyText").GetComponent<Text>();
            moneyText.text = "Money: $" + currentMoney;

            priceText = GameObject.Find("PriceText").GetComponent<Text>();
            priceText.text = "Price: $" + price;
        }
    }

    //This will change the text of the spawn button.
    //Also checks your ballance and the current price in order to change or not.
    private void ChangeBtnText()
    {
        if (currentMoney >= price)
        {
            spawnBtnText.text = "Spawn";
        }
        else
        {
            spawnBtnText.text = "Not enough money!";
        }
    }

    //This will reset the values when an object has been spawned.
    public void ResetValues()
    {
        UpdateConfig();

        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = Random.Range(1, 100);
        }
    }

    float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
