using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayermask;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private PlacementSystem placementSystem;

    private Camera factoryBuildingModeCamera;
    private Camera factoryGameplayModeCamera;
    public Camera currentCamera;

    private Vector3 lastPosition;
    private bool isGameplayCamActive;
    public event Action OnClicked, OnExit;

    private void Awake()
    {
        placementSystem = GetComponent<PlacementSystem>();
    }
    private void Start()
    {
        factoryBuildingModeCamera = cameraManager.FactoryBuildingCamera();
        factoryGameplayModeCamera = cameraManager.FactoryGameplayCamera();
        factoryBuildingModeCamera.enabled = true;
        isGameplayCamActive = true;
    }
    public void Update()
    {
        CameraTransition(); // Switch Camera between GameplayMode and BuildingMode
        CheckCameraState(); // Check currentCamera Value

        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }


    public void CameraTransition()
    {
        if (Input.GetKeyDown(KeyCode.L) && isGameplayCamActive) //Switch to Factory Building Mode
        {
            factoryGameplayModeCamera.enabled = false;
            factoryBuildingModeCamera.enabled = true;

            currentCamera = factoryBuildingModeCamera;


        }
        else if (Input.GetKeyDown(KeyCode.L) && !isGameplayCamActive) //Switcch to Gameplay Mode
        {
            factoryBuildingModeCamera.enabled = false;
            factoryGameplayModeCamera.enabled = true;

            currentCamera = factoryGameplayModeCamera;

        }
    }

    public void CheckCameraState()
    {
        if (currentCamera == factoryBuildingModeCamera)
        {
            isGameplayCamActive = false;
            Debug.Log("--------------------------BuildingMode is Active--------------------------");
        }
        else if(currentCamera == factoryGameplayModeCamera)
        {
            isGameplayCamActive = true;
            Debug.Log("--------------------------GameplayMode is Active--------------------------");
        }
    }

}
