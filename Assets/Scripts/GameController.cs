using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum OrderType
    {
        SmallOrder,
        MediumOrder,
        LargeOrder
    }
    public enum MenuItems
    {
        BlackCoffe,
        WhiteCoffe,
        HotChoclate
    }
    OrderType orderType = OrderType.SmallOrder;
    private float timer;
    private int score;
    private int life;
    private float orderTime;
    void Start()
    {
        score = 0;
        life = 3;
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

    }
    // Update is called once per frame
    void Update()
    {
        checkIsLost();
    }

    public void checkIsLost()
    {
        if(life <= 0)
        {
            // Player Lost
        }
    }
}
