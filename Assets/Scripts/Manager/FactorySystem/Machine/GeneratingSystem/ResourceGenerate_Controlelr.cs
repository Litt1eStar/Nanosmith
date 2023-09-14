using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static InventoryItem_Controller;
using static UnityEditor.Progress;

public class ResourceGenerate_Controlelr : MonoBehaviour
{
    // After Generate done Create new PlayerItemData();
    [SerializeField] private MachineItemMenu_Controller machineItemMenuCrtl;
    [SerializeField] private List<GameObject> inputSlot; // 0 1 2
    [SerializeField] private InventoryItem_Controller playerItemInventory;
    [SerializeField] GameObject inputSlotGridLayout;
    [SerializeField] GameObject inputSlotPrefab;
    [SerializeField] GameObject itemToInitPrefab;
    [SerializeField] Button startButton;
    [SerializeField] UI_Controller UICrtl;

    [SerializeField] GameObject machinePanel;
    private List<string> itemNameToInit = new();
    public GameObject _selectedSlotInput;
    private TimeModel _timeModel;
    public List<PlayerItemData> listToCheck = new();
    private bool continueGenerating = false;

    public ItemToInit_ObejctController inputObj;

    private int itemAmount = 0;
    private ItemsDataBean itemData;
    private ItemsDataBean itemOnItemInventory;
    private OutputResourceData resultItem;
    private List<PlayerItemData> _inventoryItemList = new(); // A B C
    public List<PlayerItemData> _currentInputItemList = new();
    private List<PlayerItemData> _newItemList = new();
    private Dictionary<int, Dictionary<string, int>> _recipeMappingToSlot = new();
    private List<ItemToInit_ObejctController> inputObjList = new List<ItemToInit_ObejctController>();

    private int generatedItemCount = 0;

    private bool playerStoppedGeneration = false;
    private bool isGenerating = false;
    private bool canGenerate;
    private void Start()
    {
    }


    private void Update()
    {
        Debug.Log(machineItemMenuCrtl.isOutputSlotFree());
        if (machineItemMenuCrtl.outputData == null)
        {
            DestroyInputGameObject();
        }

        MachinePanelIsOpen();
    }
    private List<PlayerItemData> RemoveDuplicates(List<PlayerItemData> itemList)
    {
        List<PlayerItemData> uniqueList = new List<PlayerItemData>();

        foreach (var item in itemList)
        {
            // Check if an item with the same itemID is already in the uniqueList
            if (!uniqueList.Exists(x => x.itemsDataBean.itemID == item.itemsDataBean.itemID))
            {
                uniqueList.Add(item); // Add the item to the uniqueList if not found
            }
        }

        return uniqueList;
    }
    public void StartGenerate()
    {
 /*       if (isGenerating)
        {
            Debug.Log($"Generation is already in progress.");
            return;    
        }*/
        if (_currentInputItemList.Count > 0)
        {
            itemData = machineItemMenuCrtl.OutputData();
            foreach (var item in _currentInputItemList)
            {
                bool addToCheckList = true;
                foreach (var amount in itemData.itemRecipe.Values.ToList())
                {
                    if (item.stack <= amount)
                    {
                        addToCheckList = false;
                        break; // Exit the inner loop
                    }
                }

                if (addToCheckList)
                {
                    listToCheck.Add(item);
                }
            }
            Debug.Log($"ListToCheck[{listToCheck.Count}] || Recipe[{itemData.itemRecipe.Count}] ");
            listToCheck = RemoveDuplicates(listToCheck);

            continueGenerating = listToCheck.Count == itemData.itemRecipe.Count;
            if (continueGenerating)
            {
                playerStoppedGeneration = false;
                StartCoroutine(GenerateItems());
            }
        }
        else
        {
            Debug.LogError("There is NO Input");
        }
    }

    private IEnumerator GenerateItems()
    {
      /*  if (isGenerating)
        {
            Debug.Log("Couroutine isGenerating");
            yield break; // Exit the coroutine if generation is already in progress.
        }*/
        isGenerating = true;
        while (playerStoppedGeneration == false)
        {
            if (CheckInputSlotMatchWithRecipe())
            {
                bool canGenerate = true;

                foreach (var item in _currentInputItemList)
                {
                    foreach (var amount in itemData.itemRecipe.Values)
                    {
                        if (item.stack < amount)
                        {
                            canGenerate = false;
                            break;
                        }
                    }
                }

                if (canGenerate == true)
                {
                    GenerateOneItem();
                    Debug.Log($"isGenerating[{isGenerating}] || GenerateItems()");
                }
                // Pause for a moment before generating the next item (adjust as needed)
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                playerStoppedGeneration = true;
            }
            yield return new WaitForSeconds(10f);
        }        
        isGenerating = false;
    }

    private void UpdateItemUI()
    {
        foreach (var recipeEntry in itemData.itemRecipe)
        {
            string itemName = recipeEntry.Key;
            int amountRequired = recipeEntry.Value;

            foreach (var inputItem in _currentInputItemList)
            {
                if (inputItem.itemsDataBean.itemName == itemName)
                {
                    // Ensure the stack doesn't go negative
                    if (inputItem.stack < 0)
                    {
                        inputItem.stack = 0;
                    }

                    // Update the UI for the corresponding input slot
                    var inputObj = inputObjList.FirstOrDefault(obj => obj.itemData == inputItem);
                    if (inputObj != null)
                    {
                        inputObj.UpdateUI(inputItem.stack);
                    }
                }
            }
        }
    }
    private void GenerateOneItem()
    {
        itemData = machineItemMenuCrtl.OutputData();
        if (!CheckInputSlotMatchWithRecipe())
        {
            //playerStoppedGeneration = true; // Set to stop generation
            Debug.Log("Not enough input items for the recipe.");
            playerStoppedGeneration = true;
            return;
        }

        if (listToCheck.Count == itemData.itemRecipe.Count)
        {
            _timeModel = new TimeModel(DateTime.Now.AddSeconds(5), DateTime.Now, EndTimer, StartTimer);
            resultItem = new OutputResourceData(itemData, _timeModel);
            TimeManager.Instance.AddTimeModel(_timeModel);
        }
        Debug.Log("Start Generate");
        //PlayerItemData newItem = new PlayerItemData(resultItem.itemsDataBean, 45);
    }

    private void DeductInputItem()
    {
        List<PlayerItemData> inputItems = playerItemInventory.playerItemInInputSlot();

        foreach (var recipeEntry in itemData.itemRecipe)
        {
            string itemName = recipeEntry.Key;
            int amountRequired = recipeEntry.Value;
            Debug.Log($"Recipe[{itemName}] : Amount = {amountRequired} ");
            // Deduct the required amount from the input items
            foreach (var inputItem in inputItems)
            {
                if (inputItem.itemsDataBean.itemName == itemName)
                {
                    int remainingAmount = inputItem.stack - amountRequired;
                    if (remainingAmount <= 0)
                    {
                        // Remove the item if the amount is zero or negative
                        //playerItemInventory.RemoveItemFromInputSlot(inputItem);
                        Debug.Log("Item Amount is Less than 0");
                    }
                    else
                    {
                        // Update the item amount
                        inputItem.stack = remainingAmount;
                        Debug.Log($"Current Item amount[{inputItem.stack}] | {inputItem.itemsDataBean.itemName}");
                    }
                    break; // Exit the inner loop
                }
                Debug.Log($"Item Left :: {inputItem.stack} | {inputItem.itemsDataBean.itemName}");
            }  
        }
    }

    public void StartTimer()
    {
        Debug.Log($"Output Item Name :: {itemData.itemName}");
    } 
    public void EndTimer()
    {
        foreach (var recipeEntry in itemData.itemRecipe.Values)
        {
            _currentInputItemList.ForEach(item => {
                if (item.stack <= recipeEntry)
                {
                    return;
                }
            });
        }

        startButton.interactable = true;
        DeductInputItem();  
        UpdateItemUI(); 
        Debug.Log("DONE");

        _currentInputItemList.RemoveAll(item => item.stack <= 0);

        TimeManager.Instance.RemoveTimeModel(_timeModel);

        continueGenerating = CheckInputSlotMatchWithRecipe();

        PlayerItemData existingItem = playerItemInventory.GetItemByItemData(resultItem.itemsDataBean);

        if (existingItem != null)
        {
            // Item already exists, add to its stack
            existingItem.AddToStack(1); // Add 1 unit to the stack
        }
        else
        {
            // Item doesn't exist, add it to the inventory
            PlayerItemData newItem = new PlayerItemData(resultItem.itemsDataBean, 1);
            playerItemInventory.AddItemToInventory(newItem);
        }
        playerItemInventory.inventoryItemList.ForEach(item => {
            Debug.Log($"PlayerInventory Item[{item.itemsDataBean.itemName}] | {item.stack}");
            });

        StartCoroutine(GenerateItems());


    }

    private void AddOutputToPlayerInventory(ItemsDataBean outputItemDataBean)
    {
        List<PlayerItemData> existingItem = new();
        playerItemInventory.inventoryItemList.ForEach(itemInInventory => {
            int i = 0;
                existingItem.ForEach(existingItemData => {
                if (playerItemInventory.inventoryItemList.Contains(existingItemData))
                {
                    playerItemInventory.inventoryItemList[i].stack += 1;
                }
                else
                {
                    PlayerItemData newItem = new PlayerItemData(outputItemDataBean, 1);
                    existingItem.Add(newItem);
                    playerItemInventory.inventoryItemList.Add(newItem);
                }
                i++;
            });
        });

        playerItemInventory.inventoryItemList.ForEach(item => {
            Debug.Log($"PlayerInventory Item[{item.itemsDataBean.itemName}]");
        });
    }

    public bool CheckInputSlotMatchWithRecipe()
    {
        bool recipeMatch = true;
        if (_currentInputItemList.Count != 3)
        {
            recipeMatch = false;
            Debug.Log($"Count :: {_currentInputItemList.Count}");
            Debug.Log("Not enough input items in the slots.");
            _currentInputItemList.ForEach(item => {
                Debug.Log(item.itemsDataBean.itemName);
            });
        }
        // Assuming the input items are in the same order as itemNameToInit
        for (int i = 0; i < _currentInputItemList.Count; i++)
        {
            if (_currentInputItemList[i].itemsDataBean.itemName != itemNameToInit[i])
            {
                // The input item at index i doesn't match the expected item.
                Debug.Log($"{_currentInputItemList[i].itemsDataBean.itemName} :: {itemNameToInit[i]}");
                recipeMatch = false;
                break; // No need to continue checking.
            }
        }
        if (recipeMatch)
        {
            // You can now proceed with generating the output item.
            return true;
        }
        else
        {
            Debug.Log("ItemRecipe Doesn't Match with Input Slot");
            // Handle the case where the items don't match the recipe.
            return false;
        }
    }

    public void RecipeMapping(ItemsDataBean _itemsData)
    {
        itemData = _itemsData;
        Dictionary<string,int> _itemRecipes = new Dictionary<string,int>();
        _itemRecipes = itemData.itemRecipe;

        for (int i = 0; i < _itemRecipes.Count; i++)
        {
            // i = 1 => Add itemRecipe to Value of recipeMappingSlot , Add i to Key
            Dictionary<string, int> recipeData = new Dictionary<string, int>(_itemRecipes);
            _recipeMappingToSlot.Add(i, recipeData);
        }

        AddItemToSlot();

           
    }

    public void AddItemToInputSlot(PlayerItemData dataToInit, GameObject slot)
    {
        GameObject goSlot = GameObjectUtil.Instance.AddChild(slot, itemToInitPrefab);
        if (goSlot != null)
        {
            inputObj = goSlot.GetComponent<ItemToInit_ObejctController>();
            if (inputObj != null)
            {
                inputObj.Init(dataToInit);
                _currentInputItemList.Add(dataToInit);
                inputObjList.Add(inputObj);
            }
            else { Debug.LogWarning("InputSlot_ObjectController component not found on GameObject."); }
        }
    }
    public ItemsDataBean MatchData(string itemsName)
    {
        string targetItemName = itemsName;
        List<ItemsDataBean> allItems = Main.InventoryManager.GetAllItemData();
        if (allItems != null)
        {
            ItemsDataBean targetItem = allItems.FirstOrDefault(item => item.itemName == targetItemName);

            if (targetItem != null)
            {
                //Debug.Log($"Found item: {targetItem.itemName}, Rarity: {targetItem.itemRarity}");
                return targetItem;
            }
            else
            {
                //Debug.Log($"Item with name {targetItemName} not found.");
                return null;
            }
        }
        else
        {
            //Debug.LogError("allItem is null");
            return null;
        }
    }
    private void AddItemToSlot()
    {
        foreach (int slotIndex in _recipeMappingToSlot.Keys)
        {
            Dictionary<string, int> recipeData = _recipeMappingToSlot[slotIndex];
            itemNameToInit = CollectRecipeData(recipeData, slotIndex);
        }

        #region AddSlotToGame
        GameObject goSlotA = GameObjectUtil.Instance.AddChild(inputSlot[0], inputSlotPrefab);
        if (goSlotA != null)
        {
            InputItemPreview_ObjectController inputObjSlotA = goSlotA.GetComponent<InputItemPreview_ObjectController>();
            if (inputObjSlotA != null) { 
                inputObjSlotA.Init(itemNameToInit[0], this, 0);
                inputObjSlotA.RegisterResourceGenerateController(this);
            }
            else { Debug.LogWarning("InputSlot_ObjectController component not found on GameObject."); }
        }
        else { Debug.LogWarning("goSlotA is null."); }

        GameObject goSlotB = GameObjectUtil.Instance.AddChild(inputSlot[1], inputSlotPrefab);
        if (goSlotB != null)
        {
            InputItemPreview_ObjectController inputObjSlotB = goSlotB.GetComponent<InputItemPreview_ObjectController>();
            if (inputObjSlotB != null) {
                inputObjSlotB.Init(itemNameToInit[1], this, 1);
                inputObjSlotB.RegisterResourceGenerateController(this);
            }
            else { Debug.LogWarning("InputSlot_ObjectController component not found on GameObject."); }
        }
        else { Debug.LogWarning("goSlotA is null."); }

        GameObject goSlotC = GameObjectUtil.Instance.AddChild(inputSlot[2], inputSlotPrefab);
        if (goSlotC != null)
        {
            InputItemPreview_ObjectController inputObjSlotC = goSlotC.GetComponent<InputItemPreview_ObjectController>();
            if (inputObjSlotC != null) { 
                inputObjSlotC.Init(itemNameToInit[2], this, 2);
                inputObjSlotC.RegisterResourceGenerateController(this); }
            else { Debug.LogWarning("InputSlot_ObjectController component not found on GameObject."); }
        }
        else { Debug.LogWarning("goSlotC is null."); }
        #endregion
    }

    private List<string> CollectRecipeData(Dictionary<string, int> recipeData, int slotIndex)
    {
        string recipeName = null;
        int recipeIndex = 0;
        List<string> inputItemNameList = new List<string>();
        // Assuming each recipe has a fixed length of 3
        foreach (KeyValuePair<string, int> recipeEntry in recipeData)
        {
            if (recipeEntry.Key != "null" && recipeIndex < 3)
            {
                 recipeName = recipeEntry.Key;
                // Here you can collect the recipe data, e.g., into separate variables
                switch (recipeIndex)
                {
                    case 0:
                        // Collect data for the first recipe
                        string inputSloatA_name = recipeEntry.Key;
                        inputItemNameList.Add(inputSloatA_name);
                        Debug.Log($"SlotA :: {inputSloatA_name}");
                        break;
                    case 1:
                        // Collect data for the second recipe
                        string inputSloatB_name = recipeEntry.Key;
                        inputItemNameList.Add (inputSloatB_name);
                        Debug.Log($"SlotB :: {inputSloatB_name}");
                        break;
                    case 2:
                        // Collect data for the third recipe
                        string inputSloatC_name = recipeEntry.Key;
                        inputItemNameList.Add(inputSloatC_name);
                        Debug.Log($"SlotC :: {inputSloatC_name}");
                        break;
                }

                recipeIndex++;
            }
        }

        return inputItemNameList;
    }

    private void DestroyInputGameObject()
    {
        if (machineItemMenuCrtl.isOutputSlotFree())
        {
            inputSlot.ForEach(slot => {
                GameObjectUtil.Instance.DestroyAllChildren(slot);
            });
            _recipeMappingToSlot.Clear();
        }
    }

    public void OpenPlayerInventory(ItemsDataBean focusData)
    {
        Debug.Log($"InputSlot :: {_selectedSlotInput.name}");
        itemOnItemInventory = focusData;
        _inventoryItemList = playerItemInventory.InventoryDataList();
        PlayerItemInventoryHandler();
    }

    private void PlayerItemInventoryHandler()
    {
        UICrtl.TurnOnItemInventoryPanel();
        playerItemInventory.ClearInventory();
        playerItemInventory.AddSelectedItemToPanel(itemOnItemInventory);
    }

    
    public bool MachinePanelIsOpen()
    {
        if (machinePanel.activeInHierarchy)
        {
            playerItemInventory.isMachinePanelOpening = true;
            return true;
        }
        else
        {
            playerItemInventory.isMachinePanelOpening = false;
            return false;
        }
    }
    

    public void HandleSlotClick(int slotIndex)
    {
        _selectedSlotInput = inputSlot[slotIndex];
    }
}
