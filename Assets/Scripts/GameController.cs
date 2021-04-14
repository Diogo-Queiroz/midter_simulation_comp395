using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TMP_Dropdown categoryDropdown;
    public TMP_Dropdown subCategoryDropdown;
    public TMP_Dropdown itemDropdown;
    public Button makeItem;
    public Button finishOrderButton;
    public TMP_Text freezTimerText;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text[] trayTexts;
    public TMP_Text[] orderTexts;
    public GameObject[] lifeSprite;
    public GameObject[] orderSprites;
    public GameObject[] traySprites;
    public GameObject ServicePros;
    private ServiceProcess serviceProcess;

    //Menu Items
    //sub-categoty
    private List<string> subCategoryDrinks;
    private List<string> subCategoryFood;
    private List<string> subCategoryDesert;
    //food 
    private List<string> itemHotDrinks;
    private List<string> itemColdDrinks;
    private List<string> itemBurgers;
    private List<string> itemWraps;
    private List<string> itemBagels;
    private List<string> itemMuffins;
    private List<string> itemDonuts;
    private List<string> itemIceCreams;

    public List<string> tray;
    public List<string> customerOrder;
    private int currentTrayItemNumber;

    private int currentCategory = 0;
    bool isMenuFreez;
    float freezTimer;
    bool isMenuTimer;
    private float timer;
    private int score;
    private int life;
    private float orderTime;
    private int orderType = 0;
    void Start()
    {
        score = 0;
        life = 3;
        currentTrayItemNumber = 0;
        serviceProcess = ServicePros.GetComponent<ServiceProcess>();
        finishOrderButton.interactable = false;

        subCategoryDrinks = new List<string>();
        subCategoryFood = new List<string>();
        subCategoryDesert = new List<string>();
        itemHotDrinks = new List<string>();
        itemColdDrinks = new List<string>();
        itemBurgers = new List<string>();
        itemWraps = new List<string>();
        itemBagels = new List<string>();
        itemMuffins = new List<string>();
        itemDonuts = new List<string>();
        itemIceCreams = new List<string>();
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
        Sprite temp = loadSpriteByName("Black Coffee");
        if (temp != null)
        {
            traySprites[0].GetComponent<Image>().sprite = temp;
        }
        else
        {
            Debug.Log("Null Sprite");
        }
    }

    public void receiveOrder(int oType,float oTime)
    {
        finishOrderButton.interactable = true;
        int temp1, temp2, temp3;
        timer = oTime * 60f;
        orderType = oType;
        Debug.Log("Timer:" + timer);
        if (oType == 1)
        {
            temp1 = Random.Range(1, 3);
            if(temp1 == 1)
            {
                temp2 = Random.Range(1, 3);
                if(temp2 == 1)
                {
                    temp3 = Random.Range(1, itemHotDrinks.Count);
                    customerOrder.Add(itemHotDrinks[temp3].ToString());
                }
                else
                {
                    temp3 = Random.Range(1, itemColdDrinks.Count);
                    customerOrder.Add(itemColdDrinks[temp3].ToString());
                }
            }
            else
            {
                temp2 = Random.Range(1, 4);
                if (temp2 == 1)
                {
                    temp3 = Random.Range(1, itemMuffins.Count);
                    customerOrder.Add(itemMuffins[temp3].ToString());
                }
                else if(temp2 == 2)
                {
                    temp3 = Random.Range(1, itemDonuts.Count);
                    customerOrder.Add(itemDonuts[temp3].ToString());
                }
                else
                {
                    temp3 = Random.Range(1, itemIceCreams.Count);
                    customerOrder.Add(itemIceCreams[temp3].ToString());
                }
            }
        }
        else if(oType == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                temp1 = Random.Range(1, 4);
                if (temp1 == 1)
                {
                    temp2 = Random.Range(1, 3);
                    if (temp2 == 1)
                    {
                        temp3 = Random.Range(1, itemHotDrinks.Count);
                        customerOrder.Add(itemHotDrinks[temp3].ToString());
                    }
                    else
                    {
                        temp3 = Random.Range(1, itemColdDrinks.Count);
                        customerOrder.Add(itemColdDrinks[temp3].ToString());
                    }
                }
                else if (temp1 == 2)
                {
                    temp2 = Random.Range(1, 4);
                    if (temp2 == 1)
                    {
                        temp3 = Random.Range(1, itemBurgers.Count);
                        customerOrder.Add(itemBurgers[temp3].ToString());
                    }
                    else if (temp2 == 2)
                    {
                        temp3 = Random.Range(1, itemWraps.Count);
                        customerOrder.Add(itemWraps[temp3].ToString());
                    }
                    else
                    {
                        temp3 = Random.Range(1, itemBagels.Count);
                        customerOrder.Add(itemBagels[temp3].ToString());
                    }
                }
                else
                {
                    temp2 = Random.Range(1, 4);
                    if (temp2 == 1)
                    {
                        temp3 = Random.Range(1, itemMuffins.Count);
                        customerOrder.Add(itemMuffins[temp3].ToString());
                    }
                    else if (temp2 == 2)
                    {
                        temp3 = Random.Range(1, itemDonuts.Count);
                        customerOrder.Add(itemDonuts[temp3].ToString());
                    }
                    else
                    {
                        temp3 = Random.Range(1, itemIceCreams.Count);
                        customerOrder.Add(itemIceCreams[temp3].ToString());
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                temp1 = Random.Range(1, 4);
                if (temp1 == 1)
                {
                    temp2 = Random.Range(1, 3);
                    if (temp2 == 1)
                    {
                        temp3 = Random.Range(1, itemHotDrinks.Count);
                        customerOrder.Add(itemHotDrinks[temp3].ToString());
                    }
                    else
                    {
                        temp3 = Random.Range(1, itemColdDrinks.Count);
                        customerOrder.Add(itemColdDrinks[temp3].ToString());
                    }
                }
                else if (temp1 == 2)
                {
                    temp2 = Random.Range(1, 4);
                    if (temp2 == 1)
                    {
                        temp3 = Random.Range(1, itemBurgers.Count);
                        customerOrder.Add(itemBurgers[temp3].ToString());
                    }
                    else if (temp2 == 2)
                    {
                        temp3 = Random.Range(1, itemWraps.Count);
                        customerOrder.Add(itemWraps[temp3].ToString());
                    }
                    else
                    {
                        temp3 = Random.Range(1, itemBagels.Count);
                        customerOrder.Add(itemBagels[temp3].ToString());
                    }
                }
                else
                {
                    temp2 = Random.Range(1, 4);
                    if (temp2 == 1)
                    {
                        temp3 = Random.Range(1, itemMuffins.Count);
                        customerOrder.Add(itemMuffins[temp3].ToString());
                    }
                    else if (temp2 == 2)
                    {
                        temp3 = Random.Range(1, itemDonuts.Count);
                        customerOrder.Add(itemDonuts[temp3].ToString());
                    }
                    else
                    {
                        temp3 = Random.Range(1, itemIceCreams.Count);
                        customerOrder.Add(itemIceCreams[temp3].ToString());
                    }
                }
            }
        }
        showCustomerOrder();
        isMenuTimer = true;
    }

    public Sprite loadSpriteByName(string itemName)
    {
        Sprite sprite = null;
        if (itemName == "Black Coffee")
        {
            sprite = Resources.Load<Sprite>("Sprites/Hot Drinks/BlackCoffee");
        }
        else if(itemName == "White Coffee")
        {
            sprite = Resources.Load<Sprite>("Sprites/Hot Drinks/WhiteCoffee");
        }
        else if (itemName == "Hot Chocolate")
        {
            sprite = Resources.Load<Sprite>("Sprites/Hot Drinks/HotChocolate");
        }
        else if (itemName == "French Vanilla")
        {
            sprite = Resources.Load<Sprite>("Sprites/Hot Drinks/FrenchVanilla");
        }
        else if (itemName == "Steeped Tea")
        {
            sprite = Resources.Load<Sprite>("Sprites/Hot Drinks/SteepedTea");
        }
        else if (itemName == "Green Tea")
        {
            sprite = Resources.Load<Sprite>("Sprites/Hot Drinks/GreenTea");
        }
        else if (itemName == "Cold Coffee")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/ColdCoffee");
        }
        else if (itemName == "Iced Coffee")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/IcedCoffee");
        }
        else if (itemName == "Iced Cap")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/IcedCap");
        }
        else if (itemName == "Creamy Chills")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/CreamyChills");
        }
        else if (itemName == "Lemonade")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/Lemonade");
        }
        else if (itemName == "Coke")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/Coke");
        }
        else if (itemName == "Diet-Coke")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/DietCoke");
        }
        else if (itemName == "Coke-Zero")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/CokeZero");
        }
        else if (itemName == "Pepsi")
        {
            sprite = Resources.Load<Sprite>("Sprites/Cold Drinks/Pepsi");
        }
        else if (itemName == "Big Mac")
        {
            sprite = Resources.Load<Sprite>("Sprites/Burgers/BigMac");
        }
        else if (itemName == "Cheese Burger")
        {
            sprite = Resources.Load<Sprite>("Sprites/Burgers/CheeseBurger");
        }
        else if (itemName == "Chicken Burger")
        {
            sprite = Resources.Load<Sprite>("Sprites/Burgers/ChickenBurger");
        }
        else if (itemName == "Spicy Chicken Burger")
        {
            sprite = Resources.Load<Sprite>("Sprites/Burgers/SpicyChickenBurger");
        }
        else if (itemName == "Fish Burger")
        {
            sprite = Resources.Load<Sprite>("Sprites/Burgers/FishBurger");
        }
        else if (itemName == "Junior Chicken Burger")
        {
            sprite = Resources.Load<Sprite>("Sprites/Burgers/JuniorChickenBurger");
        }
        else if (itemName == "Farmer's Wrap")
        {
            sprite = Resources.Load<Sprite>("Sprites/Wraps/FarmersWrap");
        }
        else if (itemName == "Farmer's Breakfast Wrap")
        {
            sprite = Resources.Load<Sprite>("Sprites/Wraps/FarmersBreakfastWrap");
        }
        else if (itemName == "Spicy Chicken Wrap")
        {
            sprite = Resources.Load<Sprite>("Sprites/Wraps/SpicyChickenWrap");
        }
        else if (itemName == "Crispy Chicken Wrap")
        {
            sprite = Resources.Load<Sprite>("Sprites/Wraps/CrispyChickenWrap");
        }
        else if (itemName == "Chedder Chicken Wrap")
        {
            sprite = Resources.Load<Sprite>("Sprites/Wraps/ChedderChickenWrap");
        }
        else if (itemName == "Plain Bagel")
        {
            sprite = Resources.Load<Sprite>("Sprites/Bagels/PlainBagel");
        }
        else if (itemName == "Everything Bagel")
        {
            sprite = Resources.Load<Sprite>("Sprites/Bagels/EverythingBagel");
        }
        else if (itemName == "Four-Cheese Bagel")
        {
            sprite = Resources.Load<Sprite>("Sprites/Bagels/FourCheeseBagel");
        }
        else if (itemName == "Jalapeno Bagel")
        {
            sprite = Resources.Load<Sprite>("Sprites/Bagels/JalapenoBagel");
        }
        else if (itemName == "12 Grain Bagel")
        {
            sprite = Resources.Load<Sprite>("Sprites/Bagels/12GrainBagel");
        }
        else if (itemName == "Choclate-Chip Muffins")
        {
            sprite = Resources.Load<Sprite>("Sprites/Muffines/ChoclateChipMuffine");
        }
        else if (itemName == "Blueberry Muffins")
        {
            sprite = Resources.Load<Sprite>("Sprites/Muffines/BlueberryMuffine");
        }
        else if (itemName == "Raisin Bran Muffins")
        {
            sprite = Resources.Load<Sprite>("Sprites/Muffines/RaisinBranMuffin");
        }
        else if (itemName == "Fruit Muffins")
        {
            sprite = Resources.Load<Sprite>("Sprites/Muffines/FruitMuffin");
        }
        else if (itemName == "Chocolate Dip Donut")
        {
            sprite = Resources.Load<Sprite>("Sprites/Donut/ChoclateDipDonut");
        }
        else if (itemName == "Vanilla Dip Donut")
        {
            sprite = Resources.Load<Sprite>("Sprites/Donut/VanillaDipDonut");
        }
        else if (itemName == "Honey Dip Donut")
        {
            sprite = Resources.Load<Sprite>("Sprites/Donut/HoneyDipDonut");
        }
        else if (itemName == "Sugar Loop Donut")
        {
            sprite = Resources.Load<Sprite>("Sprites/Donut/SugarLoopDonut");
        }
        else if (itemName == "Maple Dip Donut")
        {
            sprite = Resources.Load<Sprite>("Sprites/Donut/MapleDipDonut");
        }
        else if (itemName == "Double Chocolate Donut")
        {
            sprite = Resources.Load<Sprite>("Sprites/Donut/DoubleChoclateDonut");
        }
        else if (itemName == "Chocolate Softy")
        {
            sprite = Resources.Load<Sprite>("Sprites/Ice Cream/ChocolateIceCream");
        }
        else if (itemName == "Strawberry Softy")
        {
            sprite = Resources.Load<Sprite>("Sprites/Ice Cream/StrawberryIceCream");
        }
        else if (itemName == "Vanilla Softy")
        {
            sprite = Resources.Load<Sprite>("Sprites/Ice Cream/VanillaIceCream");
        }
        return sprite;
    }
    public void showCustomerOrder()
    {
        for(int i= 0; i < customerOrder.Count; i++)
        {
            orderTexts[i].text = customerOrder[i].ToString();
            orderSprites[i].GetComponent<Image>().sprite = loadSpriteByName(customerOrder[i].ToString());
            orderTexts[i].gameObject.SetActive(true);
            orderSprites[i].SetActive(true);
        }
    }
    public void resetCustomerOrder()
    {
        for (int i = 0; i < customerOrder.Count; i++)
        {
            orderTexts[i].gameObject.SetActive(false);
            orderSprites[i].SetActive(false);
        }
        customerOrder.Clear();
    } 

    public void finishOrder()
    {
        bool isError = false;
        foreach(string item in customerOrder)
        {
            if (!tray.Contains(item))
            {
                isError = true;
            }
        }
        if (isError)
        {
            removeLife();
        }
        else
        {
            if(orderType == 1)
            {
                addScore(30);
            }
            else if(orderType == 2)
            {
                addScore(50);
            }
            else
            {
                addScore(100);
            }
        }
        resetTray();
        resetCustomerOrder();
        resetTimers();
        serviceProcess.exitCar();
    }
    public void resetTimers()
    {
        freezTimer = 0;
        timer = 0;
        if (isMenuFreez)
        {
            freezeControlsToggle(true);
        }
        isMenuFreez = false;
        isMenuTimer = false;
        timerText.text = "Timer:00:00";
        freezTimerText.gameObject.SetActive(false);
        finishOrderButton.interactable = false;
    }
    public void addScore(int s)
    {
        score += s;
        scoreText.text = "Score: " + score;
    }
    public void removeLife()
    {
        if(life == 3)
        {
            life--;
            lifeSprite[2].SetActive(false);
        }
        else if(life == 2)
        {
            life--;
            lifeSprite[1].SetActive(false);
        }
        else
        {
            life--;
            lifeSprite[0].SetActive(false);
        }
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
        //Debug.Log("Toggle");
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
        traySprites[currentTrayItemNumber].GetComponent<Image>().sprite = loadSpriteByName(tray[tray.Count - 1].ToString());
        traySprites[currentTrayItemNumber].SetActive(true);
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

        if (isMenuTimer)
        {
            int seconds = Convert.ToInt32(timer) / 60;
            int miliSec = Convert.ToInt32(timer) % 60;
            if(timer<= 0f)
            {
                isMenuTimer = false;
                finishOrder();
                resetCustomerOrder();
                resetTray();
            }
            timerText.text = "Time: " + seconds.ToString("00") + ":" + miliSec.ToString("00");
            timer--;
        }
    }

    public void resetTray()
    {
        for(int i=0;i<5;i++)
        {
            trayTexts[i].gameObject.SetActive(false);
            traySprites[i].SetActive(false);
        }
        currentTrayItemNumber = 0;
        tray.Clear();
    }
    public void checkIsLost()
    {
        if(life <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
