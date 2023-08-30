using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputItemMappingInput
{
    Dictionary<PlayerItemData, int> _inputItem = new();
    OutputResourceData _outputItem;

    public OutputItemMappingInput(OutputResourceData outputItem, Dictionary<PlayerItemData, int> inputItem)
    {
        _inputItem = inputItem;
        _outputItem = outputItem;
    }
}
