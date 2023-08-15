using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerGameplayData currentPlayerData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Main.PlayerManager.CreatePlayerGameplayData();
            ReadPlayerData();
        }
    }
    public void Init(PlayerGroundPlatform_Controller targetStartGround)
    {
        transform.position = targetStartGround.attachNode.transform.position;       
    }

    public void ReadPlayerData()
    {
        currentPlayerData = Main.PlayerManager.CreatePlayerGameplayData();

        if (currentPlayerData != null)
        {
            Debug.Log("ReadPlayerData inventory count : " + currentPlayerData.inventory.gameItemListDict.Count);
            foreach (int index in currentPlayerData.inventory.gameItemListDict.Keys)
            {
                PlayerItemData targetItem = currentPlayerData.inventory.gameItemListDict[index];
                Debug.Log("ReadPlayerData player item : " + index + " | item name : " + targetItem.machineDataBean.machineName + " | item stack : " + targetItem.stack);
                //Debug.Log("ReadPlayerData player item : " + index + " | item name : " + targetItem.itemsDataBean.itemName + " | item stack : " + targetItem.stack);
            }
        }
        else
        {
            Debug.LogError("PlayerGameplayData is NULL");
        }
    }
}
