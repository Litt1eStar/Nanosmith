using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayermask;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private PlacementSystem placementSystem;
    [SerializeField] private LayerMask UIlayer;

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
        UIlayer = LayerMask.NameToLayer("UI");

        factoryBuildingModeCamera = cameraManager.FactoryBuildingCamera();
        factoryGameplayModeCamera = cameraManager.FactoryGameplayCamera();
        factoryBuildingModeCamera.enabled = true;
        isGameplayCamActive = true;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }

        Debug.Log(IsPointerOverUIElement() ? "Over UI" : "Not Over UI");
    }


    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UIlayer)
                return true;
        }
        return false;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }



/*    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();*/

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


  
}
