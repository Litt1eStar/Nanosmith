using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemData
{
    public ItemType itemType;

    public MachineryDataBean machineDataBean;
    public ItemsDataBean itemsDataBean;
    public int stack;

    public PlayerItemData(MachineryDataBean bean) 
    {
        machineDataBean = bean;
        itemType = bean.itemType;
        stack = 1;
    }

    public PlayerItemData(ItemsDataBean bean)
    {
        itemsDataBean = bean;
        itemType = bean.itemType;
        stack = 1;
    }
}
