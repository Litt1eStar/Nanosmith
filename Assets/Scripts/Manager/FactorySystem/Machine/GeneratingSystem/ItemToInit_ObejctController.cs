using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToInit_ObejctController : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI stack;
    public PlayerItemData itemData;
    public void Init(PlayerItemData dataToInit)
    {
        itemData = dataToInit;
        if (itemData != null)
        {
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(itemData.itemsDataBean.itemKey + "_icon");
            stack.text = itemData.stack.ToString();
            //selection.SetActive(false);
        }
    }

    public void UpdateUI(int _stack)
    {
        stack.text = _stack.ToString();
    }

}
