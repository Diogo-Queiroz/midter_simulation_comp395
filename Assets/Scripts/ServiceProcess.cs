using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//New as of Feb.25rd

public class ServiceProcess : MonoBehaviour
{
    public GameObject carInService;
    public Transform carExitPlace;

    [Tooltip("Car per hour")]
    public float serviceRateAsCarsPerHour = 25; // car/hour
    public float interServiceTimeInHours; // = 1.0 / ServiceRateAsCarsPerHour;
    private float interServiceTimeInMinutes;
    private float interServiceTimeInSeconds;

    private GameObject GameController;
    private GameController gameController;

    //public float ServiceRateAsCarsPerHour = 20; // car/hour
    public bool generateServices = false;

    //New as of Feb.23rd
    //Simple generation distribution - Uniform(min,max)
    //
    public float minInterServiceTimeInSeconds = 3;
    public float maxInterServiceTimeInSeconds = 60;
    //

    //New as Feb.25th
    //CarController carController;
    QueueManager queueManager; //=new QueueManager();

    // UI Texts
    //public Text serviceStrategy;
    //public Text serviceIntervalTime;
    //public Text serviceIntervalTimeRemaining;
    private float m_TimerForNextService;

    private int orderType;

    public enum ServiceIntervalTimeStrategy
    {
        ConstantIntervalTime,
        UniformIntervalTime,
        ExponentialIntervalTime,
        ObservedIntervalTime
    }

    public ServiceIntervalTimeStrategy serviceIntervalTimeStrategy = ServiceIntervalTimeStrategy.UniformIntervalTime;

    // Start is called before the first frame update
    void Start()
    {
        //serviceStrategy.text = $"Service Strategy = {serviceIntervalTimeStrategy.ToString()}";
        interServiceTimeInHours = 1.0f / serviceRateAsCarsPerHour;
        interServiceTimeInMinutes = interServiceTimeInHours * 60;
        interServiceTimeInSeconds = interServiceTimeInMinutes * 60;
        //queueManager = this.GetComponent<QueueManager>();
        //queueManager = new QueueManager();
        //StartCoroutine(GenerateServices());

        this.GameController = GameObject.FindGameObjectWithTag("GameController");
        gameController = GameController.GetComponent<GameController>();
    }

    private void Update()
    {
        m_TimerForNextService -= Time.deltaTime;
        //serviceIntervalTimeRemaining.text = $"Timer: {m_TimerForNextService:F2}s";
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ServiceProcess.OnTriggerEnter:other=" + other.gameObject.name);

        if (other.gameObject.CompareTag("Car"))
        {
            if (carInService == null)
            {
                carInService = other.gameObject;
                CarController car = other.GetComponent<CarController>();

                if (car.carState == CarController.CarState.Entered)
                {
                    orderType = Random.Range(1, 4);
                    if (orderType == 1)
                    {
                        interServiceTimeInSeconds = 30f;
                    }
                    else if (orderType == 2)
                    {
                        interServiceTimeInSeconds = 40f;
                    }
                    else
                    {
                        interServiceTimeInSeconds = 60f;
                    }
                    gameController.receiveOrder(orderType, interServiceTimeInSeconds);
                    car.SetInService(true);
                    car.ChangeState(CarController.CarState.InService);
                    car.GetComponent<NavMeshAgent>().isStopped = true;
                    Rigidbody rb = car.GetComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
                

                //if (queueManager.Count() == 0)
                //{
                //    queueManager.Add(carInService);
                //}
                
                generateServices = true;
                //carController = carInService.GetComponent<CarController>();
                GenerateServices();
            }
        }
    }

    public void GenerateServices()
    {
        while (generateServices)
        {
            //Instantiate(carPrefab, carSpawnPlace.position, Quaternion.identity);
            if (orderType == 3)
            {
                interServiceTimeInSeconds += 3;
            }
            float timeToNextServiceInSec = interServiceTimeInSeconds;
            switch (serviceIntervalTimeStrategy)
            {
                case ServiceIntervalTimeStrategy.ConstantIntervalTime:
                    timeToNextServiceInSec = interServiceTimeInSeconds + 7f;
                    break;
                case ServiceIntervalTimeStrategy.UniformIntervalTime:
                    timeToNextServiceInSec = Random.Range(minInterServiceTimeInSeconds, maxInterServiceTimeInSeconds);
                    break;
                case ServiceIntervalTimeStrategy.ExponentialIntervalTime:
                    float U = Random.value;
                    float Lambda = 1 / serviceRateAsCarsPerHour;
                    timeToNextServiceInSec = Utilities.GetExp(U, Lambda);
                    break;
                case ServiceIntervalTimeStrategy.ObservedIntervalTime:
                    timeToNextServiceInSec = interServiceTimeInSeconds;
                    break;
                default:
                    print("No acceptable ServiceIntervalTimeStrategy:" + serviceIntervalTimeStrategy);
                    break;

            }

            //New as of Feb.23rd
            //float timeToNextServiceInSec = Random.Range(minInterServiceTimeInSeconds,maxInterServiceTimeInSeconds);
            generateServices = false;
            //serviceIntervalTime.text = $"Time to next Service in Sec > {timeToNextServiceInSec:F2}";
            m_TimerForNextService = timeToNextServiceInSec;

            //yield return new WaitForSeconds(interServiceTimeInSeconds);

        }
    }
    public void exitCar()
    {
        carInService.GetComponent<CarController>().ExitService(carExitPlace);
        carInService = null;
    }
    private void OnDrawGizmos()
    {
        //BoxCollidercarInService.GetComponent<BoxCollider>
        if (carInService)
        {
//            Renderer r = carInService.GetComponent<Renderer>();
//            r.material.color = Color.green;

        }


    }

}
