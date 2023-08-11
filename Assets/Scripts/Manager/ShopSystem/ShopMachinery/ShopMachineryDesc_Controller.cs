using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;
public class ShopMachineryDesc_Controller : MonoBehaviour
{
    public GameObject itemDescriptionPanel;
    public GameObject itemGameResourceStatus;
    public GameObject inItemDescriptionPanel;

    public Image itemIcon;

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
                    itemNameUI.text = "Item Name : " + machineObj.machineName;
                    itemType.text = "Item Genre : " + machineObj.machineType;
                    storageSize.text = "Storage Size : " + machineObj.storageSize.ToString();
                    itemPrice.text = "Item Price : " + FormatPrice(machineObj.machinePriceNVC) + " NVC$";
                    itemFunctionality.text = "Production Multiplier : " + machineObj.machineProductionMultiplier.ToString();
                    break;
                case ItemType.PowerDevice:
                    PowerDeviceDataBean powerObj = (PowerDeviceDataBean)targetData;
                    itemNameUI.text = "Item Name : " + powerObj.machineName;
                    itemType.text = "Item Genre : " + powerObj.machineType;
                    storageSize.text = "Storage Size : " + powerObj.storageSize.ToString();
                    itemPrice.text = "Item Price : " + FormatPrice(powerObj.machinePriceNVC) + " NVC$";
                    itemFunctionality.text = "Power Generate/Hour :: " + powerObj.powerGeneratePerHour.ToString();
                    break;
                case ItemType.Storage:
                    StorageDataBean storageObj = (StorageDataBean)targetData;
                    itemNameUI.text = "Item Name : " + storageObj.machineName;
                    itemType.text = "Item Genre : " + storageObj.machineType;
                    storageSize.text = "Storage Size : " + storageObj.storageSize.ToString();
                    itemPrice.text = "Item Price : " + FormatPrice(storageObj.machinePriceNVC) + " NVC$";
                    itemFunctionality.text = "Production Multiplier : " + storageObj.storageSize;
                    break;
                case ItemType.ResearchAndDevelopDevice:
                    StorageDataBean rAndDObj = (StorageDataBean)targetData;
                    itemNameUI.text = "Item Name : " + rAndDObj.machineName;
                    itemType.text = "Item Genre : " + rAndDObj.machineType;
                    storageSize.text = "Storage Size : " + rAndDObj.storageSize.ToString();
                    itemPrice.text = "Item Price : " + FormatPrice(rAndDObj.machinePriceNVC) + " NVC$";
                    itemFunctionality.text = "Production Multiplier : " + rAndDObj.storageSize;
                    break;
                case ItemType.EnvironmentalControlDevice:
                    StorageDataBean envDeviceObj = (StorageDataBean)targetData;
                    itemNameUI.text = "Item Name : " + envDeviceObj.machineName;
                    itemType.text = "Item Genre : " + envDeviceObj.machineType;
                    storageSize.text = "Storage Size : " + envDeviceObj.storageSize.ToString();
                    itemPrice.text = "Item Price : " + FormatPrice(envDeviceObj.machinePriceNVC) + " NVC$";
                    itemFunctionality.text = "Production Multiplier : " + envDeviceObj.storageSize;
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

    private string FormatPrice(int price)
    {
        return price.ToString("N0");
    }

}
