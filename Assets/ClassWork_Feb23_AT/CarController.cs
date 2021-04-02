using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;


public class CarController : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public Transform targetWindow;
    public Transform targetCar=null;
    public Transform targetExit = null;

    public bool InService { get; set; }
    public GameObject driveThruWindow;
    public QueueManager queueManager;
    private GameObject GameController;
    private GameController gameController;

    [SerializeField] private Transform carFront;
    private int m_IntLayer;

    [SerializeField] private Animator _animator;
    public enum CarState
    {
        None=-1,
        Entered,
        InService,
        Serviced
    }
    public CarState carState = CarState.None;

    private void Awake()
    {
        m_IntLayer = 1 << LayerMask.NameToLayer($"Car");
    }

    // Start is called before the first frame update
    void Start()
    {
        driveThruWindow = GameObject.FindGameObjectWithTag("DriveThruWindow");
        targetWindow = driveThruWindow.transform;
        targetExit = GameObject.FindGameObjectWithTag("CarExit").transform;
        this.GameController = GameObject.FindGameObjectWithTag("GameController");
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameController = GameController.GetComponent<GameController>();

        //
        carState = CarState.Entered;
        FSMCar();
        
        
        

    }

    void FSMCar()
    {

        switch (carState)
        {
            case CarState.None: //do nothing - shouldn't happen
                break;
            case CarState.Entered:
                DoEntered();
                break;
            case CarState.InService:
                DoInService();
                break;
            case CarState.Serviced:
                DoServiced();
                break;
            default:
                print("carState unknown!:" + carState);
                break;

        }
    }
    void DoEntered()
    {


        targetCar = targetWindow;

        queueManager = GameObject.FindGameObjectWithTag("DriveThruWindow").GetComponent<QueueManager>();
        queueManager.Add(this.gameObject);

        navMeshAgent.SetDestination(targetCar.position);
        navMeshAgent.isStopped = false;
    }
    void DoInService()
    {
        Debug.Log("Stopped: " + navMeshAgent.isStopped);
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
    }
    void DoServiced()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(targetExit.position);
    }
    public void ChangeState(CarState newCarState)
    {
        this.carState = newCarState;
        FSMCar();
    }
    
    public void SetInService(bool value)
    {
        //Chaneg
        InService = value;
    }
    
    public void ExitService(Transform target)
    {
        this.SetInService(false);
        
        queueManager.PopFirst();
        ChangeState(CarState.Serviced);
        targetExit = target;
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 5;
        navMeshAgent.SetDestination(target.position);
    }


    private void Update()
    {
        _animator.SetBool("isMoving", !navMeshAgent.isStopped);
    }

    public void FixedUpdate()
    {

        if (carState == CarState.Entered)
        {
            if (targetCar == null)
            {
                targetCar = targetWindow;
                //navMeshAgent.SetDestination(targetCar.position);
                navMeshAgent.isStopped = false;
            }
        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (other.gameObject.GetComponent<NavMeshAgent>().isStopped)
            {
                navMeshAgent.isStopped = true;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        // else if (other.gameObject.CompareTag("DriveThruWindow"))
        // {
        //     //ChangeState(CarState.InService);
        //     //SetInService(true);
        // }
        else if (other.gameObject.CompareTag("CarExit"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (other.gameObject.GetComponent<NavMeshAgent>().isStopped)
            {
                navMeshAgent.isStopped = true;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            navMeshAgent.isStopped = false;
        }
    }



}
