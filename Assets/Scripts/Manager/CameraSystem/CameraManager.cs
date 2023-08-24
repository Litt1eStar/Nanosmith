using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _factoryGameplayCamera;
    [SerializeField] private Camera _factoryBuildingGameplayCamera;
    private Camera _currentCamera;

    public Camera FactoryBuildingCamera()
    {
        if (_currentCamera != null)
        {
            _currentCamera.enabled = false;
        }

        _currentCamera = _factoryBuildingGameplayCamera;
        return _currentCamera;
    }

    public Camera FactoryGameplayCamera()
    {
        if (_currentCamera != null)
        {
            _currentCamera.enabled = false;
        }

        _currentCamera = _factoryGameplayCamera;
        return _currentCamera;
    }

    public bool IsGameplayCameraEnabled(Camera currentCamera) 
    {
        if (currentCamera == _factoryGameplayCamera) { return true; }
        else { return false; }
    }

    public bool IsBuildingCameraEnabled(Camera currentCamera)
    {
        if (currentCamera == _factoryBuildingGameplayCamera) { return true; }
        else { return false; }
    }
}
