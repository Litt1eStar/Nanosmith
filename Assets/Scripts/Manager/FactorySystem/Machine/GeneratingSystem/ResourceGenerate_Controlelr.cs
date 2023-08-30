using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ResourceGenerate_Controlelr : MonoBehaviour
{
    // After Generate done Create new PlayerItemData();
    [SerializeField] private MachineItemMenu_Controller machineItemMenuCrtl;
    [SerializeField] private List<GameObject> inputSlot; // 0 1 2
    [SerializeField] GameObject inputSlotGridLayout;
    [SerializeField] GameObject inputSlotPrefab;

    private List<string> itemNameToInit = new();

    private TimeModel _timeModel;
    private ItemsDataBean itemData;
    private OutputResourceData resultItem;
    private List<PlayerItemData> _inventoryItemList = new(); // A B C
    private Dictionary<int, Dictionary<string, int>> _recipeMappingToSlot = new();
    private void Update()
    {
        Debug.Log(machineItemMenuCrtl.isOutputSlotFree());
        if (machineItemMenuCrtl.outputData == null)
        {
            DestroyInputGameObject();
        }
    }

    public void StartGenerate()
    {
        itemData = machineItemMenuCrtl.OutputData();
        //Generate(itemData);

        _timeModel = new TimeModel(DateTime.Now.AddSeconds(30), DateTime.Now, EndTimer, StartTimer);
        resultItem = new OutputResourceData(itemData, _timeModel);

        TimeManager.Instance.AddTimeModel(_timeModel);

        //PlayerItemData newItem = new PlayerItemData(resultItem.itemsDataBean, 45);
    }

    public void Generate(ItemsDataBean _itemsData)
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
            InputSlot_ObjectController inputObjSlotA = goSlotA.GetComponent<InputSlot_ObjectController>();
            if (inputObjSlotA != null) { inputObjSlotA.Init(itemNameToInit[0]); }
            else { Debug.LogWarning("InputSlot_ObjectController component not found on GameObject."); }
        }
        else { Debug.LogWarning("goSlotA is null."); }

        GameObject goSlotB = GameObjectUtil.Instance.AddChild(inputSlot[1], inputSlotPrefab);
        if (goSlotB != null)
        {
            InputSlot_ObjectController inputObjSlotB = goSlotB.GetComponent<InputSlot_ObjectController>();
            if (inputObjSlotB != null) { inputObjSlotB.Init(itemNameToInit[1]); }
            else { Debug.LogWarning("InputSlot_ObjectController component not found on GameObject."); }
        }
        else { Debug.LogWarning("goSlotA is null."); }

        GameObject goSlotC = GameObjectUtil.Instance.AddChild(inputSlot[2], inputSlotPrefab);
        if (goSlotC != null)
        {
            InputSlot_ObjectController inputObjSlotC = goSlotC.GetComponent<InputSlot_ObjectController>();
            if (inputObjSlotC != null) { inputObjSlotC.Init(itemNameToInit[2]); }
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
    public void StartTimer()
    {
        Debug.Log("HELOOOOO");
    }

    public void EndTimer()
    {
        Debug.Log("DONE");
    }
}
