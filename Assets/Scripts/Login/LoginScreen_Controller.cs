using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoginScreen_Controller : MonoBehaviour
{
    public TMP_InputField _username;
    public TMP_InputField _password;
    
    public void OnClickLoginButton()
    {
        string usernameText = _username.text;
        string passwordText = _password.text;

        Debug.Log($"Username => {usernameText} | Password => {passwordText}");
        StartCoroutine(Login(usernameText, passwordText));

    }

    public IEnumerator Login(string username, string password)
    {
        string targetUrl = "http://localhost:3000/api/login?username=" + username + "&password=" + password;
        Debug.Log("Login :: " + targetUrl);
        UnityWebRequest www = UnityWebRequest.Get(targetUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error.ToString());
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            JSONObject result = new JSONObject(www.downloadHandler.text);
            Debug.Log("Result is " + result["data"].ToString());

            JSONObject data = new JSONObject(result["data"].ToString());
            int memberId = data["id"].GetAsInteger();
            string memberInventory = data["inventory"].GetAsString();
            Debug.Log("memberID is " + memberId.ToString() + " | memberInventory is " + memberInventory);
        }
    }

    public void ParseAndCreateLoadedPlayerItem(string memberInventory)
    {
        string[] savedItems = memberInventory.Split(',');
        List<PlayerItemData> loadedItemList = new List<PlayerItemData>();
        foreach (string item in savedItems)
        {
            string[] itemInfo = item.Split(':');
            int itemID = int.Parse(itemInfo[0]);
            int stack = int.Parse(itemInfo[1]);
            PlayerItemData itemData = new PlayerItemData(Main.InventoryManager.GetItemDataByID(itemID), stack);
            itemData.stack = stack;

            loadedItemList.Add(itemData);
        }
    }
}
