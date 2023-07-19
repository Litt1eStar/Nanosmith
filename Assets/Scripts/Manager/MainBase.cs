using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase
{
    public static GameObject MainGameObject {  get; private set; }

    protected static void BaseInit()
    {
        CreateMain();
        LoadAllManger();
    }

    private static void CreateMain()
    {
        GameObject mainPref = new GameObject();
        mainPref.name = "Main";
        MainGameObject = mainPref;
        Object.DontDestroyOnLoad(MainGameObject);
    }

    #region CreateMain

    protected static T GetManagerComponentFromChild<T>(T member) where T : Component
    {
        return MainGameObject.GetComponentInChildren<T>();
    }

    private static void LoadAllManger()
    {
        GameObject[] manageArray = Resources.LoadAll<GameObject>("Prefabs/Manager");
        foreach (GameObject x in manageArray) 
        {
            GameObjectUtil.Instance.AddChild(MainGameObject, x);
        }
    }
    #endregion
}
