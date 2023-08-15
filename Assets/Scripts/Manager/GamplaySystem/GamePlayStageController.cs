using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlayStageController : MonoInstance<GamePlayStageController>
{
    public int stage = 0;
    public float positionXOffset;
    public float positionZOffset;

    public GameObject groundOriginNode;
    public GameObject playerGroundPlatformPrefab;
    public PlayerController playerController;

    private void Start()
    {
        ReadGameStageGroundPlatform();
    }
    public void ReadGameStageGroundPlatform()
    {
        StartCoroutine(LoadStageDataStreamingAsset("StageData/NanosmithStage1/" + stage + ".csv"));
    }

    public void GeneratePlayerGround(ObstacleType groundType, float positionX, float positionZ)
    {
        PlayerGroundPlatform_Controller pgpCtrl = GameObjectUtil.Instance.AddChild(groundOriginNode, playerGroundPlatformPrefab).GetComponent<PlayerGroundPlatform_Controller>();
        pgpCtrl.transform.position = new Vector3(positionX, 0, positionZ);
        pgpCtrl.Init(groundType);
        Debug.Log("GeneratePLayerGround is ACTIVE");
    }


    public void ReadStageDataFromCSV(string result)
    {
        string[] rows = result.Split('\n');
        int rowCounter = 0;

        foreach (string row in rows)
        {
            int columnCounter = 0;
            string[] columns = row.Split(",");
            foreach (string column in columns)
            {
                int groundType = int.Parse(column);
                Debug.Log("   GroundType :: " + groundType);
                //Debug.Log("Reading StageData....");
                GeneratePlayerGround((ObstacleType)groundType, columnCounter, rowCounter);
                columnCounter++;
            }
            rowCounter++;
        }
    }

    IEnumerator LoadStageDataStreamingAsset(string fileName)
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

        Debug.Log("Reading file from: " + filepath);
        //Debug.Log("Loaded File :: " + result);
        ReadStageDataFromCSV(result);
    }
}
