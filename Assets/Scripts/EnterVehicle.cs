using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Vehicles.Car;

public class EnterVehicle : MonoBehaviour
{
    public bool inCar = false;
    
    [SerializeField]private CinemachineVirtualCamera Playercamera;
    [SerializeField]private CinemachineVirtualCamera Carcamera;
    [SerializeField]private GameObject player;
    [SerializeField]private GameObject car;
    [SerializeField]private CarController carInput;
    [SerializeField] private CarUserControl carController = null;
    [SerializeField] private GameObject playerInput = null;
    [SerializeField] private Rigidbody carBody;
    
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerInput = GameObject.FindWithTag("Player");
        car = gameObject;
        Playercamera = GameObject.FindWithTag("playerCam").GetComponent<CinemachineVirtualCamera>();
        Carcamera = GameObject.FindWithTag("carCam").GetComponent<CinemachineVirtualCamera>();
        carBody = GetComponent<Rigidbody>();
        carInput = GetComponent<CarController>();
        carController = GetComponent<CarUserControl>();
        

    }
        
    private void Start()
    {
        inCar = car.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inCar)
            {
                GetOutOfCar();
            }
            else if(Vector3.Distance(car.transform.position,player.transform.position)<10f)
            {
                getInToCar();
            }


        }
    }

    void GetOutOfCar()
    {
        inCar = false;
        player.SetActive(true);
        player.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left);
        carController.enabled = false;
        carInput.Move(0,0,1,1);
        playerInput.SetActive(true);
        Playercamera.enabled = true;
        Carcamera.enabled = false;
        carBody.isKinematic = true;
        
        var gridPos = WorldGrid.GridPositionFromWorldPosition(car.transform.position, 1.0f);
        car.transform.rotation = new Quaternion(0, 0, 0, 0);
        car.transform.position = gridPos;
        car.transform.position += new Vector3(0, -0.3f, 0);

    }

    void getInToCar()
    {
        Carcamera.Follow = car.transform;
        inCar = true;
        player.SetActive(false);
        carController.enabled = true;
        playerInput.SetActive(false);
        Playercamera.enabled = false;
        Carcamera.enabled = true;
        carBody.isKinematic = false;
    }
}
