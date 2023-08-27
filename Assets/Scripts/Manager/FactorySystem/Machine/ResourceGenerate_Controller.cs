using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGenerate_Controller : MonoBehaviour
{
    private TimeModel _timeModel;
    private List<GameObject> inputResourceList;
    [SerializeField] private TextMeshProUGUI timeText;
    public void StartGenerateResource()
    {
        _timeModel = new TimeModel(DateTime.Now.AddSeconds(60),
                                   DateTime.Now,
                                   GenerateComplete,
                                   StartGenerate);

        if (_timeModel != null)
        {
            TimeManager.Instance.AddTimeModel(_timeModel);
        }
        else
        {
            Debug.LogError("_timeModel is NULL");
        }
        
    }
    public void StartGenerate()
    {
        Debug.Log("Generate in Process :: ");
    }

    public void GenerateComplete ()
    {
        Debug.Log("Generate Complete");
    }

    public List<GameObject> InputResourceContainer(List<GameObject> listOfInputResource)
    {
        inputResourceList = new List<GameObject>();
        inputResourceList = listOfInputResource;
        return inputResourceList;
    }
}
