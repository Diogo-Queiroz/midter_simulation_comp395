using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    public enum OrderType
    {
        SmallOrder,
        MediumOrder,
        LargeOrder
    }

    public TMP_Dropdown categoryDropdown;
    public TMP_Dropdown subCategoryDropdown;
    public TMP_Dropdown itemDropdown;
    public Button makeItem;
    public TMP_Text freezTimerText;
    public TMP_Text[] trayTexts;

    //Menu Items
    //sub-categoty
    public List<string> subCategoryDrinks;
    public List<string> subCategoryFood;
    public List<string> subCategoryDesert;
    //food 
    public List<string> itemHotDrinks;
    public List<string> itemColdDrinks;
    public List<string> itemBurgers;
    public List<string> itemWraps;
    public List<string> itemBagels;
    public List<string> itemMuffins;
    public List<string> itemDonuts;
    public List<string> itemIceCreams;

    public List<string> tray;
    private int currentTrayItemNumber;

    private int currentCategory = 0;

    bool isMenuFreez;
    float freezTimer;

    OrderType orderType = OrderType.SmallOrder;
    private float timer;
    private int score;
    private int life;
    private float orderTime;
    void Start()
    {
        score = 0;
        life = 3;
        populateSubCategoryDrinkList();
        populateSubCategoryFoodList();
        populateSubCategoryDesertList();
        populateHotDrinks();
        populateColdDrinks();
        populateBurgers();
        populateWraps();
        populateBagels();
        populateMuffins();
        populateDonuts();
        populateIceCreams();
        currentTrayItemNumber = 0;
    }

    public void receiveOrder(int oType,float oTime)
    {

    }
    public void finishOrder()
    {

    }
    public void generateOrder()
    {

    }
    public void addToTray()
    {   
        Debug.Log(itemDropdown.options[itemDropdown.value].text);
        tray.Add(itemDropdown.options[itemDropdown.value].text);
        if(currentCategory == 1)
        {
            freezTimer = 300f;
        }
        else if(currentCategory == 2)
        {
            freezTimer = 540f;
        }
        else if(currentCategory == 3)
        {
            freezTimer = 420f;
        }
        isMenuFreez = true;
        freezeControlsToggle(false);
    }

    public void populateSubCategoryDrinkList()
    {
        subCategoryDrinks.Add("Hot Drinks");
        subCategoryDrinks.Add("Cold Drinks");
    }

    public void populateSubCategoryFoodList()
    {
        subCategoryFood.Add("Burgers");
        subCategoryFood.Add("Wraps");
        subCategoryFood.Add("Bagels");
    }

    public void populateSubCategoryDesertList()
    {
        subCategoryDesert.Add("Muffins");
        subCategoryDesert.Add("Donuts");
        subCategoryDesert.Add("Ice Creams");
    }

    public void populateHotDrinks()
    {
        itemHotDrinks.Add("Black Coffee");
        itemHotDrinks.Add("White Coffee");
        itemHotDrinks.Add("Hot Chocolate");
        itemHotDrinks.Add("French Vanilla");
        itemHotDrinks.Add("Steeped Tea");
        itemHotDrinks.Add("Green Tea");
    }
    public void populateColdDrinks()
    {
        itemColdDrinks.Add("Cold Coffee");
        itemColdDrinks.Add("Iced Coffee");
        itemColdDrinks.Add("Iced Cap");
        itemColdDrinks.Add("Creamy Chills");
        itemColdDrinks.Add("Lemonade");
        itemColdDrinks.Add("Coke");
        itemColdDrinks.Add("Diet-Coke");
        itemColdDrinks.Add("Coke-Zero");
        itemColdDrinks.Add("Pepsi");
    }
    public void populateBurgers()
    {
        itemBurgers.Add("Big Mac");
        itemBurgers.Add("Cheese Burger");
        itemBurgers.Add("Chicken Burger");
        itemBurgers.Add("Spicy Chicken Burger");
        itemBurgers.Add("Fish Burger");
        itemBurgers.Add("Junior Chicken Burger");
    }
    public void populateWraps()
    {
        itemWraps.Add("Farmer's Wrap");
        itemWraps.Add("Farmer's Breakfast Wrap");
        itemWraps.Add("Spicy Chicken Wrap");
        itemWraps.Add("Crispy Chicken Wrap");
        itemWraps.Add("Chedder Chicken Wrap");
    }
    public void populateBagels()
    {
        itemBagels.Add("Plain Bagel");
        itemBagels.Add("Everything Bagel");
        itemBagels.Add("Four-Cheese Bagel");
        itemBagels.Add("Jalapeno Bagel");
        itemBagels.Add("12 Grain Bagel");
    }
    public void populateMuffins()
    {
        itemMuffins.Add("Choclate-Chip Muffins");
        itemMuffins.Add("Blueberry Muffins");
        itemMuffins.Add("Raisin Bran Muffins");
        itemMuffins.Add("Fruit Muffins");
    }
    public void populateDonuts()
    {
        itemDonuts.Add("Chocolate Dip Donut");
        itemDonuts.Add("Vanilla Dip Donut");
        itemDonuts.Add("Honey Dip Donut");
        itemDonuts.Add("Sugar Loop Donut");
        itemDonuts.Add("Maple Dip Donut");
        itemDonuts.Add("Double Chocolate Donut");
    }
    public void populateIceCreams()
    {
        itemIceCreams.Add("Chocolate Softy");
        itemIceCreams.Add("Strawberry Softy");
        itemIceCreams.Add("Vanilla Softy");
    }
    public void populateItem(int val)
    {
        //Debug.Log("Categoy:"+currentCategory+"val:"+val);
        itemDropdown.ClearOptions();
        if(currentCategory == 1)
        {
            if (val == 0)
            {
                foreach(string option in itemHotDrinks)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
            else if(val == 1)
            {
                foreach (string option in itemColdDrinks)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
        }
        else if(currentCategory == 2)
        {
            if(val == 0)
            {
                foreach (string option in itemBurgers)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
            else if(val == 1)
            {
                foreach (string option in itemWraps)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
            else if (val == 2)
            {
                foreach (string option in itemBagels)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
        }
        else if(currentCategory == 3)
        {
            if (val == 0)
            {
                foreach (string option in itemMuffins)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
            else if (val == 1)
            {
                foreach (string option in itemDonuts)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
            else if (val == 2)
            {
                foreach (string option in itemIceCreams)
                {
                    itemDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
            }
        }
    }
    public void populateSubCategory(int val)
    {
        //Debug.Log(val);
        subCategoryDropdown.ClearOptions();
        switch (val)
        {
            case 0:
                foreach (string option in subCategoryDrinks)
                {
                    subCategoryDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
                currentCategory = 1;
                break;
            case 1:
                foreach (string option in subCategoryFood)
                {
                    subCategoryDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
                currentCategory = 2;
                break;
            case 2:
                foreach (string option in subCategoryDesert)
                {
                    subCategoryDropdown.options.Add(new TMP_Dropdown.OptionData(option));
                }
                currentCategory = 3;
                break;
            default:
                //subCategoryDropdown.ClearOptions();
                break;
        }
        
    }

    public void freezeControlsToggle(bool toggle)
    {
        Debug.Log("Toggle");
        categoryDropdown.interactable = toggle;
        subCategoryDropdown.interactable = toggle;
        itemDropdown.interactable = toggle;
        makeItem.interactable = toggle;
        freezTimerText.gameObject.SetActive(!toggle);
    }

    void addLastTrayItem()
    {
        //Debug.Log(tray[tray.Count - 1].ToString());
        trayTexts[currentTrayItemNumber].text  = tray[tray.Count - 1].ToString();
        trayTexts[currentTrayItemNumber].gameObject.SetActive(true);
        currentTrayItemNumber++;

    }
    // Update is called once per frame
    void Update()
    {
        checkIsLost();
    }
    void FixedUpdate()
    {
        //Debug.Log("Fixed Update");
        if (isMenuFreez)
        {
            int seconds = Convert.ToInt32(freezTimer) / 60;
            int miliSec = Convert.ToInt32(freezTimer) % 60;
            if (freezTimer <= 0f)
            {
                isMenuFreez = false;
                freezeControlsToggle(true);
                addLastTrayItem();
            }
            freezTimerText.text = "Preparing Item:" + seconds.ToString("00") + ":" + miliSec.ToString("00");
            freezTimer--;
        }
    }
    public void checkIsLost()
    {
        if(life <= 0)
        {
            // Player Lost
        }
    }
}
