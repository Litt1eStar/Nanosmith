using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodyConstant
{
    public const string APP_VERSION = "0.0.1 Demo";

    public bool DEBUGGING_MODE = true;

    public const string LOCAL_HOST = "http://127.0.0.1/arhospital/";
    public const string DEVELOPER_HOST = "http://61.91.50.198:9090/codeIgniter/";
    public const string TEST_HOST = "http://61.91.50.198:9090/codeIgniter/";
    public const string LIVE_HOST = "http://61.91.50.198:9090/codeIgniter/";

    public const string API_HOST = "http://61.91.50.198:9090/codeIgniter/" + API_VERSION + "/";
    public const string API_VERSION = "0.1";

    public const string LANGUAGE_EN = "English";
    public const string LANGUAGE_TH = "Thai";

    public const string USER_AUthoRIZATION_KEY = "AppAuthorization";

}

public enum GameScene
{
    Gameplay
}

public enum Host
{
    Local,
    Develop,
    Test,
    Live
}
