using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputResourceData
{
    public ItemsDataBean itemsDataBean;
    public TimeModel timeModel;

    public OutputResourceData(ItemsDataBean bean, TimeModel _timeModel) 
    {
        itemsDataBean = bean;
        timeModel = _timeModel;
    }
}


