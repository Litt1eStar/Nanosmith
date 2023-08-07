using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemData : MonoBehaviour
{
    public ItemType itemType;
    public ItemsDataBean itemDataBean;

    public MachineryDataBean machineDataBean;
    public int stack;

    public PlayerItemData(MachineryDataBean bean) 
    {
        machineDataBean = bean;
        itemType = bean.itemType;
        stack = 1;
    }
}
