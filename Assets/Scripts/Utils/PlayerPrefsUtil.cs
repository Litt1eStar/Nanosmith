using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class PlayerPrefsUtil
{
    private const string PARAMETERS_SEPERATOR = ";";
    private const string KEY_VALUE_SEPERATOR = ":";
    private static readonly Hashtable playerPrefsHashtable = new Hashtable();
    private static readonly string fileName;
    private static readonly string secureFileName;

    private static bool hashTableChanged = false;
    private static string serializedOutput = "";
    private static string serializedInput = "";
    private static string[] seperators = new string[] { PARAMETERS_SEPERATOR, KEY_VALUE_SEPERATOR };
    //NOTE modify the iw3q part to an arbitrary string for your project, as this determines the encryption
    private static byte[] keyBytes;

    private static bool wasEncrypted = false;
    private static bool securityModeEnabled = false;

    static PlayerPrefsUtil()
    {
        fileName = Application.persistentDataPath + "/PlayerPrefs.txt";
        secureFileName = Application.persistentDataPath + "/AdvancedPlayerPrefs.txt";
        Debug.Log("[PlayerPrefsUtil] is on started");
        Init();
    }

    public static void Init()
    {
#if UNITY_WEBPLAYER
		byte[] keyBytes = Encoding.UTF8.GetBytes("Ecryption-" + SystemInfo.deviceUniqueIdentifier.Substring(0, 4));
#else

        string key = "Enc-" + SystemInfo.deviceUniqueIdentifier.Substring(0, 4) + GetProjectName().Substring(0, 4) + "-Key";

        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
#endif

        Init(keyBytes);
    }

    public static void Init(string key)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        Init(keyBytes);
    }

    public static void Init(byte[] _keyBytes)
    {
        keyBytes = _keyBytes;
#if !UNITY_WEBPLAYER
        //load previous settings
        StreamReader fileReader = null;

        if (File.Exists(secureFileName))
        {
            fileReader = new StreamReader(secureFileName);
            wasEncrypted = true;
            serializedInput = Decrypt(fileReader.ReadToEnd());
        }
        else if (File.Exists(fileName))
        {
            fileReader = new StreamReader(fileName);
            serializedInput = fileReader.ReadToEnd();
        }
#else
		
		if(UnityEngine.PlayerPrefs.HasKey("encryptedData"))
		{
			securityModeEnabled = bool.Parse(UnityEngine.PlayerPrefs.GetString("encryptedData"));
			serializedInput = (securityModeEnabled?Decrypt(UnityEngine.PlayerPrefs.GetString("data")):UnityEngine.PlayerPrefs.GetString("data"));
		}
		
#endif

        if (!string.IsNullOrEmpty(serializedInput))
        {
            //In the old PlayerPrefs, a WriteLine was used to write to the file.
            if (serializedInput.Length > 0 && serializedInput[serializedInput.Length - 1] == '\n')
            {
                serializedInput = serializedInput.Substring(0, serializedInput.Length - 1);

                if (serializedInput.Length > 0 && serializedInput[serializedInput.Length - 1] == '\r')
                {
                    serializedInput = serializedInput.Substring(0, serializedInput.Length - 1);
                }
            }

            Deserialize();
        }

#if !UNITY_WEBPLAYER
        if (fileReader.IsNotNull())
        {
            fileReader.Close();
        }
#endif
    }

    public static bool HasKey(string key)
    {
        return playerPrefsHashtable.ContainsKey(key);
    }

    public static void SetString(string key, string value)
    {
        if (!playerPrefsHashtable.ContainsKey(key))
        {
            playerPrefsHashtable.Add(key, value);
        }
        else
        {
            playerPrefsHashtable[key] = value;
        }

        hashTableChanged = true;
    }

    public static void SetInt(string key, int value)
    {
        if (!playerPrefsHashtable.ContainsKey(key))
        {
            playerPrefsHashtable.Add(key, value);
        }
        else
        {
            playerPrefsHashtable[key] = value;
        }

        hashTableChanged = true;
    }

    public static void SetFloat(string key, float value)
    {
        if (!playerPrefsHashtable.ContainsKey(key))
        {
            playerPrefsHashtable.Add(key, value);
        }
        else
        {
            playerPrefsHashtable[key] = value;
        }

        hashTableChanged = true;
    }

    public static void SetBool(string key, bool value)
    {
        if (!playerPrefsHashtable.ContainsKey(key))
        {
            playerPrefsHashtable.Add(key, value);
        }
        else
        {
            playerPrefsHashtable[key] = value;
        }

        hashTableChanged = true;
    }

    public static void SetLong(string key, long value)
    {
        if (!playerPrefsHashtable.ContainsKey(key))
        {
            playerPrefsHashtable.Add(key, value);
        }
        else
        {
            playerPrefsHashtable[key] = value;
        }

        hashTableChanged = true;
    }

    public static string GetString(string key)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return playerPrefsHashtable[key].ToString();
        }

        return null;
    }

    public static string GetString(string key, string defaultValue)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return playerPrefsHashtable[key].ToString();
        }
        else
        {
            playerPrefsHashtable.Add(key, defaultValue);
            hashTableChanged = true;
            return defaultValue;
        }
    }

    public static int GetInt(string key)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return (int)playerPrefsHashtable[key];
        }

        return 0;
    }

    public static int GetInt(string key, int defaultValue)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return (int)playerPrefsHashtable[key];
        }
        else
        {
            playerPrefsHashtable.Add(key, defaultValue);
            hashTableChanged = true;
            return defaultValue;
        }
    }

    public static long GetLong(string key)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return (long)playerPrefsHashtable[key];
        }

        return 0;
    }

    public static long GetLong(string key, long defaultValue)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return (long)playerPrefsHashtable[key];
        }
        else
        {
            playerPrefsHashtable.Add(key, defaultValue);
            hashTableChanged = true;
            return defaultValue;
        }
    }

    public static float GetFloat(string key)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return (float)playerPrefsHashtable[key];
        }

        return 0.0f;
    }

    public static float GetFloat(string key, float defaultValue)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return (float)playerPrefsHashtable[key];
        }
        else
        {
            playerPrefsHashtable.Add(key, defaultValue);
            hashTableChanged = true;
            return defaultValue;
        }
    }

    public static bool GetBool(string key)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return Convert.ToBoolean(playerPrefsHashtable[key]);
        }

        return false;
    }

    public static bool GetBool(string key, bool defaultValue)
    {
        if (playerPrefsHashtable.ContainsKey(key))
        {
            return Convert.ToBoolean(playerPrefsHashtable[key]);
        }
        else
        {
            playerPrefsHashtable.Add(key, defaultValue);
            hashTableChanged = true;
            return defaultValue;
        }
    }

    public static void DeleteKey(string key)
    {
        playerPrefsHashtable.Remove(key);
    }

    public static void DeleteAll()
    {
        playerPrefsHashtable.Clear();
    }

    //This is important to check to avoid a weakness in your security when you are using encryption to avoid the users from editing your playerprefs.
    public static bool WasReadPlayerPrefsFileEncrypted()
    {
        return wasEncrypted;
    }

    public static void EnableEncryption(bool enabled)
    {
        securityModeEnabled = enabled;
    }

    public static void Commit()
    {
        if (hashTableChanged)
        {
            Serialize();

            string output = (securityModeEnabled ? Encrypt(serializedOutput) : serializedOutput);
#if !UNITY_WEBPLAYER
            StreamWriter fileWriter = null;

            fileWriter = File.CreateText((securityModeEnabled ? secureFileName : fileName));

            File.Delete((securityModeEnabled ? fileName : secureFileName));

            if (fileWriter.IsNull())
            {
                Debug.LogWarning("PlayerPrefs::Flush() opening file for writing failed: " + fileName);
                return;
            }

            fileWriter.Write(output);

            fileWriter.Close();

#else
			UnityEngine.PlayerPrefs.SetString("data", output);
			UnityEngine.PlayerPrefs.SetString("encryptedData", securityModeEnabled.ToString());
			
			UnityEngine.PlayerPrefs.Save();
#endif

            serializedOutput = "";
        }
    }

    private static void Serialize()
    {
        IDictionaryEnumerator myEnumerator = playerPrefsHashtable.GetEnumerator();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool firstString = true;
        while (myEnumerator.MoveNext())
        {
            if (!firstString)
            {
                sb.Append(" ");
                sb.Append(PARAMETERS_SEPERATOR);
                sb.Append(" ");
            }
            sb.Append(EscapeNonSeperators(myEnumerator.Key.ToString(), seperators));
            sb.Append(" ");
            sb.Append(KEY_VALUE_SEPERATOR);
            sb.Append(" ");
            sb.Append(EscapeNonSeperators(myEnumerator.Value.ToString(), seperators));
            sb.Append(" ");
            sb.Append(KEY_VALUE_SEPERATOR);
            sb.Append(" ");
            sb.Append(myEnumerator.Value.GetType());
            firstString = false;
        }
        serializedOutput = sb.ToString();
    }

    private static void Deserialize()
    {
        string[] parameters = serializedInput.Split(new string[] { " " + PARAMETERS_SEPERATOR + " " }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string parameter in parameters)
        {
            string[] parameterContent = parameter.Split(new string[] { " " + KEY_VALUE_SEPERATOR + " " }, StringSplitOptions.None);

            playerPrefsHashtable.Add(DeEscapeNonSeperators(parameterContent[0], seperators), GetTypeValue(parameterContent[2], DeEscapeNonSeperators(parameterContent[1], seperators)));

            if (parameterContent.Length > 3)
            {
                Debug.LogWarning("PlayerPrefs::Deserialize() parameterContent has " + parameterContent.Length + " elements");
            }
        }
    }

    public static string EscapeNonSeperators(string inputToEscape, string[] seperators)
    {
        inputToEscape = inputToEscape.Replace("\\", "\\\\");

        for (int i = 0; i < seperators.Length; ++i)
        {
            inputToEscape = inputToEscape.Replace(seperators[i], "\\" + seperators[i]);
        }

        return inputToEscape;
    }

    public static string DeEscapeNonSeperators(string inputToDeEscape, string[] seperators)
    {

        for (int i = 0; i < seperators.Length; ++i)
        {
            inputToDeEscape = inputToDeEscape.Replace("\\" + seperators[i], seperators[i]);
        }

        inputToDeEscape = inputToDeEscape.Replace("\\\\", "\\");

        return inputToDeEscape;
    }

    public static string Encrypt(string originalString)
    {
        if (String.IsNullOrEmpty(originalString))
        {
            return "";
        }

        ICryptoTransform cryptor;
#if UNITY_WEBPLAYER
		cryptor = new DESCryptoServiceProvider().CreateEncryptor(keyBytes, keyBytes);
#else
        cryptor = new AesManaged().CreateEncryptor(keyBytes, keyBytes);
#endif
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptor, CryptoStreamMode.Write);
        StreamWriter writer = new StreamWriter(cryptoStream);
        writer.Write(originalString);
        writer.Flush();
        cryptoStream.FlushFinalBlock();
        writer.Flush();
        return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
    }

    public static string Decrypt(string cryptedString)
    {
        if (String.IsNullOrEmpty(cryptedString))
        {
            return "";
        }

        ICryptoTransform cryptor;
#if UNITY_WEBPLAYER
		cryptor = new DESCryptoServiceProvider().CreateDecryptor(keyBytes, keyBytes);
#else
        cryptor = new AesManaged().CreateDecryptor(keyBytes, keyBytes);
#endif
        MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
        CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptor, CryptoStreamMode.Read);
        StreamReader reader = new StreamReader(cryptoStream);
        return reader.ReadToEnd();
    }

    private static object GetTypeValue(string typeName, string value)
    {
        if (typeName == "System.String")
        {
            return (object)value.ToString();
        }
        if (typeName == "System.Int32")
        {
            return Convert.ToInt32(value);
        }
        if (typeName == "System.Boolean")
        {
            return Convert.ToBoolean(value);
        }
        if (typeName == "System.Single") //float
        {
            return Convert.ToSingle(value);
        }
        if (typeName == "System.Int64") //long 
        {
            return Convert.ToInt64(value);
        }
        else
        {
            Debug.LogError("Unsupported type: " + typeName);
        }

        return null;
    }

    public static string GetProjectName()
    {
        string[] s = Application.persistentDataPath.Split('/');
        string projectName = s[s.Length - 3];
        return projectName;
    }
}