using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopMachineryController : MonoBehaviour
{
    public GameObject ShopPanel;
    public GameObject itemObjectListPrefab;
    public GameObject shopItemGridLayout;
    public ShopMachineryDescriptionController shopMachineryDescriptionController;

    public delegate void SelectShopItemsObject(MachineryDataBean machineryData);
    public SelectShopItemsObject selectShopItemsObject;

    private MachineryDataBean currentData;
    public int openCounter = 0;
    private void Awake()
    {
        SpriteSheetUtil.Instance.Init();
    }

    private void Start()
    {
        ShopPanel.SetActive(false);
        shopMachineryDescriptionController.CloseDescriptionPanel();
    }

    private void Update()
    {
        if (openCounter == 0)
        {
            //Debug.Log("AddItemPanel is in this Method");
            AddItemToPanel();
            openCounter += 1;
        }

    }
    public void OnClickSelectedObject(MachineryDataBean itemsDataBean)
    {
        currentData = itemsDataBean;
        selectShopItemsObject?.Invoke(currentData);

        Debug.Log("Selected Item ID : " + currentData.itemID + " | name : " + currentData.machineName);
    }

    public void OnclickSeeMoreInformation(MachineryDataBean itemsDataBean)
    {

        shopMachineryDescriptionController.OpenDescriptionPanel(itemsDataBean);
        Debug.Log("OnclickSeeMoreInformation is WORKING ");

    }

    public void CloseShopPanel()
    {
        ShopPanel.SetActive(false);
        Debug.Log("CloseShopPanel is WORKING :: ShopPanel Name ::" + ShopPanel.name);
    }
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
    }

    public void AddItemToPanel()
    {
        List<MachineryDataBean> allItems = Main.MachineManager.GetAllItemData();
        Debug.Log("Machine allItems :: " + allItems.Count);

        /*for (int i = 0; i < allItems.Count; i++)
        {
            Debug.Log("Machine Member Index[" + i + "] | " + allItems[i].machineName);
        }*/

        allItems.ForEach(data =>
        {
            GameObject go = GameObjectUtil.Instance.AddChild(shopItemGridLayout, itemObjectListPrefab);
            switch (data.itemType)
            {
                case ItemType.Machine:
                    MachineryItemController machineObj = go.GetComponent<MachineryItemController>();
                    machineObj.Init(data);
                    machineObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.PowerDevice:        
                    MachineryItemController powerObj = go.GetComponent<MachineryItemController>();
                    powerObj.Init(data);
                    powerObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.Storage:
                    MachineryItemController storageObj = go.GetComponent<MachineryItemController>();
                    storageObj.Init(data);
                    storageObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.ResearchAndDevelopDevice:
                    MachineryItemController rdObj = go.GetComponent<MachineryItemController>();
                    rdObj.Init(data);
                    rdObj.RegisterShopGeneratedController(this);
                    break;
                case ItemType.EnvironmentalControlDevice:
                    MachineryItemController environmentControlObj = go.GetComponent<MachineryItemController>();
                    environmentControlObj.Init(data);
                    environmentControlObj.RegisterShopGeneratedController(this);
                    break;
            }
        });


    }
}
