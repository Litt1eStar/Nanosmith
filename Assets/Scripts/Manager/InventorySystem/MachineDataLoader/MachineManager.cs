using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{

    public MachineModelContainer machineModelContainer;
    void Start()
    {
        InitInventoryData();
    }

    public List<MachineryDataBean> GetAllItemData()
    {
        return machineModelContainer.GetAllItems();
    }


    public void InitInventoryData()
    {
        StartCoroutine(LoadItemDataStreamingAsset("CSV_Data/Nanosmith_MachineryCSVData_27072023.csv"));
    }

    IEnumerator LoadItemDataStreamingAsset(string fileName)
    {
        string filepath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        string result;
        if (filepath.Contains("://") || filepath.Contains(":///"))
        {
            WWW www = new WWW(filepath);
            yield return www;
            result = www.text;
        }
        else
        {
            //string filePathInStreamingAssets = System.IO.Path.Combine(Application.streamingAssetsPath, filepath);
            result = System.IO.File.ReadAllText(filepath);
        }

        //Debug.Log("Reading file from: " + filepath);
        //Debug.Log("Loaded File :: " + result);
        ReadItemDataFromCSV(result);


    }

    private void ReadItemDataFromCSV(string result)
    {
        string[] records = result.Split('\n');
        List<MachineryDataBean> allItemList = new List<MachineryDataBean>();

        foreach (string record in records)
        {
            string[] fields = record.Split(",");

            string itemID = fields[0];
            string machineName = fields[1];
            string machineType = fields[2];
            string itemKey = fields[3];
            UsedToType usedToType = (UsedToType)int.Parse(fields[4]);
            ItemType itemType = (ItemType)int.Parse(fields[5]);
            int machinePriceNVC = int.Parse(fields[6]);        
            string machineDescription = fields[7];
            int storageSize = int.Parse(fields[8]);
            int machineProductionMultiplier = int.Parse(fields[9]);
            int machinePowerConsumptionPerHour = int.Parse(fields[10]);
            float machineDurability = float.Parse(fields[11]);
            int powerGeneratePerHour = int.Parse(fields[12]);

            int x = int.Parse(fields[13]);
            int y = int.Parse(fields[14]);

            Vector2Int objectSize = new Vector2Int(x,y);


            MachineryDataBean item;
            switch (itemType)
            {
                case ItemType.Machine:
                    item = new MachineDataBean(itemID, machineName, machineType, itemKey, usedToType, itemType, machinePriceNVC, machineDescription, storageSize, objectSize,machineProductionMultiplier, machinePowerConsumptionPerHour, machineDurability);
                    break;
                case ItemType.PowerDevice:
                    item = new PowerDeviceDataBean(itemID, machineName, machineType, itemKey, usedToType, itemType, machinePriceNVC, machineDescription, storageSize, objectSize, powerGeneratePerHour);
                    break;
                case ItemType.Storage:
                    item = new StorageDataBean(itemID, machineName, machineType, itemKey, usedToType, itemType, machinePriceNVC, machineDescription, storageSize, objectSize);
                    break;
                case ItemType.ResearchAndDevelopDevice:
                    item = new StorageDataBean(itemID, machineName, machineType, itemKey, usedToType, itemType, machinePriceNVC, machineDescription, storageSize, objectSize);
                    break;
                case ItemType.EnvironmentalControlDevice:
                    item = new StorageDataBean(itemID, machineName, machineType, itemKey, usedToType, itemType, machinePriceNVC, machineDescription, storageSize, objectSize);
                    break;
                default:
                    item = new MachineryDataBean(itemID, machineName, machineType, itemKey, usedToType, itemType, machinePriceNVC, machineDescription, storageSize, objectSize);
                    break;
            }
            //Debug.Log("item MachineryDataBean created : " + item.itemID + " | " + item.machineName + " | " + item.itemType + " | " + item.storageSize + " | item Price :: " + item.machinePriceNVC);
            allItemList.Add(item);
        }
        machineModelContainer.SetAllItem(allItemList);
    }
}
