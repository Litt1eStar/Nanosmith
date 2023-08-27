using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineUi_Controller : MonoBehaviour
{
    [SerializeField] private ResourceGenerate_Controller generateCrtl;

    [SerializeField] private List<GameObject> inputResource;
    private GameObject outputResource;

    private void Awake()
    {
        outputResource = null;
    }

    public void StartGenerate()
    {
        generateCrtl.InputResourceContainer(inputResource);
        generateCrtl.StartGenerateResource();
    }
}
