using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResource", menuName = "GameResourceBase/Resource")]
public class GameResourceBaseClass : ScriptableObject
{
    public string _itemName;
    public string _itemDescription;
    public string[] itemGenre;

    public int itemMargin;
    public float itemProductionRatePerMinute;

    
}
