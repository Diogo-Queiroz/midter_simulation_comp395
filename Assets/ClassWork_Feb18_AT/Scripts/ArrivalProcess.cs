using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//using System;
//class MyObservable : System.IObservable<float>
//{
//    public System.IDisposable Subscribe(System.IObserver<float> observer)
//    {
//        throw new System.NotImplementedException();
//    }
//}
public class ArrivalProcess : MonoBehaviour
{

    public GameObject carPrefab;
    public Transform carSpawnPlace;

    public float arrivalRateAsCarsPerHour = 20; // car/hour
    public float interArrivalTimeInHours; // = 1.0 / arrivalRateAsCarsPerHour;
    private float interArrivalTimeInMinutes;
    private float interArrivalTimeInSeconds;

    //public float arrivalRateAsCarsPerHour = 20; // car/hour
    public bool generateArrivals = true;

    //New as of Feb.23rd
    //Simple generation distribution - Uniform(min,max)
    //
    public float minInterArrivalTimeInSeconds = 3; 
    public float maxInterArrivalTimeInSeconds = 60;
    
    public Text textNextArrivalIn;
    public Text timeForNextArrivalRemaining;
    private float m_TimerForNextArrival = 0;
    //
    public enum ArrivalIntervalTimeStrategy
    {
        ConstantIntervalTime,
        UniformIntervalTime,
        ExponentialIntervalTime,
        ObservedIntervalTime
    }

    public ArrivalIntervalTimeStrategy arrivalIntervalTimeStrategy=ArrivalIntervalTimeStrategy.UniformIntervalTime;

    //New as of Feb.25th
    private QueueManager queueManager;

    // Start is called before the first frame update
    void Start()
    {
        queueManager = GameObject.FindGameObjectWithTag("DriveThruWindow").GetComponent<QueueManager>();
        interArrivalTimeInHours = 1.0f / arrivalRateAsCarsPerHour;
        interArrivalTimeInMinutes = interArrivalTimeInHours * 60;
        interArrivalTimeInSeconds = interArrivalTimeInMinutes * 60;
        StartCoroutine(GenerateArrivals());
    }

    private void Update()
    {
        
        m_TimerForNextArrival -= Time.deltaTime;
        //timeForNextArrivalRemaining.text = $"Timer: {m_TimerForNextArrival:F2}s";
    }

    IEnumerator GenerateArrivals()
    {
        while (generateArrivals)
        {
            GameObject carGO=Instantiate(carPrefab, carSpawnPlace.position, Quaternion.identity);
            //if (queueManager.Count() > 0)
            //{
            //    queueManager.Add(carGO);
            //} //The first car as added in the queue when in DriveThruWindow

            float timeToNextArrivalInSec = interArrivalTimeInSeconds;
            switch (arrivalIntervalTimeStrategy)
            {
                case ArrivalIntervalTimeStrategy.ConstantIntervalTime:
                    //timeToNextArrivalInSec = 2;
                    timeToNextArrivalInSec= interArrivalTimeInSeconds;
                    break;
                case ArrivalIntervalTimeStrategy.UniformIntervalTime:
                    timeToNextArrivalInSec = Random.Range(minInterArrivalTimeInSeconds, maxInterArrivalTimeInSeconds);
                    break;
                case ArrivalIntervalTimeStrategy.ExponentialIntervalTime:
                    float U = Random.value;
                    float Lambda = 1 / arrivalRateAsCarsPerHour;
                    timeToNextArrivalInSec = Utilities.GetExp(U,Lambda);
                    break;
                case ArrivalIntervalTimeStrategy.ObservedIntervalTime:
                    timeToNextArrivalInSec = interArrivalTimeInSeconds;
                    break;
                default:
                    print("No acceptable arrivalIntervalTimeStrategy:" + arrivalIntervalTimeStrategy);
                    break;

            }

            //New as of Feb.23rd
            //float timeToNextArrivalInSec = Random.Range(minInterArrivalTimeInSeconds,maxInterArrivalTimeInSeconds);
            //textNextArrivalIn.text = $"Time to next Arrival in Sec is -> {timeToNextArrivalInSec:F2}";
            m_TimerForNextArrival = timeToNextArrivalInSec;
            yield return new WaitForSeconds(timeToNextArrivalInSec);

            //yield return new WaitForSeconds(interArrivalTimeInSeconds);

        }

    }
}
