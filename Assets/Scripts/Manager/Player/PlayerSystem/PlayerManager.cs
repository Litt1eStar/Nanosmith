using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    
    public void AddPlayerInventory(List<CartMachineryItemController> targetItem)
    {
        List<PlayerItemData> playerItemList = new List<PlayerItemData>();
        targetItem.ForEach(itemInCart =>
        {
            playerItemList.Add(new PlayerItemData(itemInCart.itemData));
        });

    }
}
