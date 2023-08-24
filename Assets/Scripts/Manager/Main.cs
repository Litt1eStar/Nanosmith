using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Main : MainBase
{
    public static GameplayManager GameplayManager { get; private set; }
    public static InventoryManager InventoryManager { get; private set; }

    public static MachineManager MachineManager { get; private set; }
    public static PlayerManager PlayerManager { get; private set; }
    public static CameraManager CameraManager { get; private set; }
    public static TimeManager TimeManager { get; private set; }


    [RuntimeInitializeOnLoadMethod]

    public static void Init()
    {
        BaseInit();
        DefineManager();
    }

    private static void DefineManager()
    {
        GameplayManager = GetManagerComponentFromChild(GameplayManager);
        InventoryManager = GetManagerComponentFromChild(InventoryManager);
        MachineManager = GetManagerComponentFromChild(MachineManager);
        PlayerManager = GetManagerComponentFromChild(PlayerManager);
        CameraManager = GetManagerComponentFromChild(CameraManager);
        TimeManager = GetManagerComponentFromChild(TimeManager);
    }
}