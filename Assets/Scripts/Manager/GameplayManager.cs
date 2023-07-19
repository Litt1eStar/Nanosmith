using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : GameManagerBase
{
    public bool isDebug = true;
    void Awake()
    {
        Debug.Log("[GameManager] is arrive");
    }

    public override void ChangeScene(GameScene scene)
    {
        base.ChangeScene(scene);
    }

    public override AsyncOperation ChangeSceneAsyn(GameScene scene)
    {
        return base.ChangeSceneAsyn(scene);
    }

    public override AsyncOperation LoadAdditiveSceneAsyn(GameScene scene)
    {
        return base.LoadAdditiveSceneAsyn(scene);
    }
}
