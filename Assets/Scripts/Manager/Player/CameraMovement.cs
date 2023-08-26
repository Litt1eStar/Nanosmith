using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    [SerializeField] private float zoomLevel;
    [SerializeField] private float zoomMultiplier = 4f;
    [SerializeField] private float minZoomLevel = 2f;
    [SerializeField] private float maxZoom = 8f;
    [SerializeField] private float velocity = 0f;
    [SerializeField] private float smoothTime = 0.25f;

    private Vector3 dragOrigin;

    private void Start()
    {
        zoomLevel = mainCam.orthographicSize;
    }
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoomLevel -= scroll * zoomMultiplier;
        zoomLevel = Mathf.Clamp(zoomLevel, minZoomLevel, maxZoom);
        mainCam.orthographicSize = Mathf.SmoothDamp(mainCam.orthographicSize, zoomLevel, ref velocity, smoothTime);
        PanCamera();   
    }

    private void PanCamera()
    {
        //Get Position of first click area
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - mainCam.ScreenToWorldPoint(Input.mousePosition); 

            mainCam.transform.position += difference;
        }
    }

}
