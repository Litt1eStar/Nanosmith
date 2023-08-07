using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    
    public void AddPlayerInventory(List<CartMachineryItemController> targetItem)
    {
        Debug.Log(targetItem.Count);
        //Debug.Log("AddPlayerInventory Is ACTIVE");
        if (targetItem != null)
        {
            List<PlayerItemData> playerItemList = new List<PlayerItemData>();
            targetItem.ForEach(itemInCart =>
            {
                playerItemList.Add(new PlayerItemData(itemInCart.itemData));
                
            });
            playerInventory.AddPlayerInventory(playerItemList);
        }
        else
        {
            Debug.Log("TargetItem is NULL");
        }
    }
}
