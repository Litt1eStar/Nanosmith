using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderBuyAmount_Controller : MonoBehaviour
{

    [SerializeField] private Slider sliderObj;
    [SerializeField] private TextMeshProUGUI sliderValueText;
    private int sliderCurrentValue = 0;

    private void Awake()
    {
        sliderValueText.text = "0";
    }
    void Start()
    {
        sliderObj.onValueChanged.AddListener( (value) =>
        {
            sliderValueText.text = value.ToString("0");
            Debug.Log(sliderValueText.text);
        });
    }

    public int SliderValueReturn()
    {
        sliderCurrentValue = int.Parse(sliderValueText.text);
        return sliderCurrentValue;
    }
}
