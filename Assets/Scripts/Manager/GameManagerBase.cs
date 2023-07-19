using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBase : MonoBehaviour
{
    public bool IsGamePaused { get; set; }
    public GameScene currentScene { get; set; }

    public virtual void ChangeScene(GameScene scene)
    {
        currentScene = scene;
        SceneManager.LoadScene(scene.ToString());
    }

    public virtual AsyncOperation ChangeSceneAsyn(GameScene scene)
    {
        currentScene = scene;
        return SceneManager.LoadSceneAsync(scene.ToString());
    }

    public virtual AsyncOperation LoadAdditiveSceneAsyn(GameScene scene)
    {
        return SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }
}
