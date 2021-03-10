using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        interServiceTimeInHours = 1.0f / serviceRateAsCarsPerHour;
        interServiceTimeInMinutes = interServiceTimeInHours * 60;
        interServiceTimeInSeconds = interServiceTimeInMinutes * 60;
        //queueManager = this.GetComponent<QueueManager>();
        //queueManager = new QueueManager();
        //StartCoroutine(GenerateServices());
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
                StartCoroutine(GenerateServices());
            }
        }
    }

    IEnumerator GenerateServices()
    {
        while (generateServices)
        {
            //Instantiate(carPrefab, carSpawnPlace.position, Quaternion.identity);
            float timeToNextServiceInSec = interServiceTimeInSeconds;
            switch (serviceIntervalTimeStrategy)
            {
                case ServiceIntervalTimeStrategy.ConstantIntervalTime:
                    timeToNextServiceInSec = interServiceTimeInSeconds;
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
            yield return new WaitForSeconds(timeToNextServiceInSec);

            //yield return new WaitForSeconds(interServiceTimeInSeconds);

        }
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
