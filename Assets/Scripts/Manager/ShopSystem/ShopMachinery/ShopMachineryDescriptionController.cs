using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;
public class ShopMachineryDescriptionController : MonoBehaviour
{
    public GameObject itemDescriptionPanel;
    public GameObject itemGameResourceStatus;
    public GameObject inItemDescriptionPanel;

    public Image itemIcon;
    public TextMeshProUGUI mainItemName;
    public TextMeshProUGUI mainItemPrice;

    public TextMeshProUGUI itemNameUI;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI storageSize;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemFunctionality;
    public TextMeshProUGUI itemDescription;

    public void OpenDescriptionPanel(MachineryDataBean targetData)
    {
        Debug.Log("OpenDescriptionPanel [target Data] :: [" + targetData + "]");
        if (targetData != null)
        {
            itemDescriptionPanel.SetActive(true);
            itemGameResourceStatus.SetActive(true);
            inItemDescriptionPanel.SetActive(true);

            switch (targetData.itemType)
            {
                case ItemType.Machine:
                    MachineDataBean machineObj = (MachineDataBean)targetData;
                    mainItemName.text = machineObj.machineName;
                    mainItemPrice.text = "NCV$ : " + machineObj.machinePriceNVC.ToString();
                    itemNameUI.text = "Item Name : " + machineObj.machineName;
                    itemType.text = "Item Genre : " + machineObj.machineType;
                    storageSize.text = "Storage Size : " + machineObj.storageSize.ToString();
                    itemPrice.text = "Item Price : " + machineObj.machinePriceNVC.ToString() + " NVC$";
                    itemFunctionality.text = "Production Multiplier : " + machineObj.machineProductionMultiplier.ToString();
                    break;
                case ItemType.PowerDevice:
                    PowerDeviceDataBean powerObj = (PowerDeviceDataBean)targetData;
                    mainItemName.text = targetData.machineName;
                    mainItemPrice.text = "NCV$ : " + powerObj.machinePriceNVC.ToString();
                    itemNameUI.text = "Item Name : " + powerObj.machineName;
                    itemType.text = "Item Genre : " + powerObj.machineType;
                    storageSize.text = "Storage Size : " + powerObj.storageSize.ToString();
                    itemPrice.text = "Item Price : " + powerObj.machinePriceNVC.ToString() + " NVC$";
                    itemFunctionality.text = "Power Generate/Hour :: " + powerObj.powerGeneratePerHour.ToString();
                    break;
                default:
                    break;
            }

            itemDescription.text = targetData.machineDescription;
            itemIcon.sprite = SpriteSheetUtil.Instance.GetSpriteByName(targetData.itemKey + "_icon");
        }
    }

    public void CloseDescriptionPanel()
    {
        itemDescriptionPanel.SetActive(false);
        itemGameResourceStatus.SetActive(false);

        inItemDescriptionPanel.SetActive(false);
    }

}
