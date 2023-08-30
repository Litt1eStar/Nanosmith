using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerItemData
{
    public ItemType itemType;

    public MachineryDataBean machineDataBean;
    public ItemsDataBean itemsDataBean;
    public TimeModel timeModel;
    public int stack;

    public PlayerItemData(MachineryDataBean bean) 
    {
        machineDataBean = bean;
        itemType = bean.itemType;
        stack = 1;
    }

    public PlayerItemData(ItemsDataBean bean , int _stack)
    {
        itemsDataBean = bean;
        itemType = bean.itemType;
        stack = _stack;
    }

}
