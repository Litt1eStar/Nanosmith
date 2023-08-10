using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    
    public void AddPlayerInventory(List<CartMachineryItemController> targetItem)
    {
        if (targetItem != null)
        {
            List<PlayerItemData> playerItemList = new List<PlayerItemData>();
            targetItem.ForEach(itemInCart =>
            {
                if (itemInCart != null )
                {
                    if (itemInCart.itemData != null)
                    {
                        playerItemList.Add(new PlayerItemData(itemInCart.itemData));
                        //Debug.Log("Type of targetItem :: " + itemInCart); // In this line, type of data = CartMachineryItemController
                    }
                    else
                    {
                        Debug.LogError("Item in Cart has null itemData");
                    }
                }
                else
                {
                    Debug.LogError("Null itemInCart in targetItem");
                }
            });
            if (playerItemList != null)
            {
                playerInventory.AddPlayerInventoryMachinery(playerItemList);
            }
            else
            {
                Debug.LogError("[ playerItemList is null ]");
            }      
        }    
        else
        {
            Debug.Log("TargetItem is NULL");
        }

        playerInventory.allMachineryResourceDict.Values.ToList().ForEach(itemInCart =>
        {
            Debug.Log("[PlayerManager] Player Item :: " + itemInCart.machineDataBean.machineName + " | Item Type :: [" + itemInCart + "]");
        });
        Debug.Log("Amount of PlayerItemData :: " + playerInventory.allMachineryResourceDict.Count);
    }



    public void AddPlayerInventoryFromItemShop(List<CartGenerateItemController> targetItem)
    {
        if (targetItem != null)
        {
            List<PlayerItemData> playerItemList = new List<PlayerItemData>();

            targetItem.ForEach(itemInCart =>
            {
                if (itemInCart != null)
                {
                    if (itemInCart.itemData != null)
                    {
                        playerItemList.Add(new PlayerItemData(itemInCart.itemData));
                        Debug.Log("Type of targetItem :: " + itemInCart); // In this line, type of data = CartMachineryItemController
                    }
                    else
                    {
                        Debug.LogError("Item in Cart has null itemData");
                    }
                }
                else
                {
                    Debug.LogError("Null itemInCart in targetItem");
                }
            });
            if (playerItemList != null)
            {
                playerInventory.AddPlayerInventoryGenerateItem(playerItemList);
            }
            else
            {
                Debug.LogError("[ playerItemList is null ]");
            }
        }
        else
        {
            Debug.Log("TargetItem is NULL");
        }


        playerInventory.allItemResourceDict.Values.ToList().ForEach(itemInCart =>
        {
            Debug.Log("[PlayerManager] Player Item :: " + itemInCart.itemsDataBean.itemName + " | Item Type :: [" + itemInCart + "]");
        });
        Debug.Log("Amount of PlayerItemData :: " + playerInventory.allItemResourceDict.Count);
    }

    
}
