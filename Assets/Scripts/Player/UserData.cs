using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoSingleton<UserData>
{
    public int CurrentLevel
    {
        get { return PlayerPrefs.GetInt("CurrentLevel", 1); }
        set { PlayerPrefs.SetInt("CurrentLevel", value); }
    }

    public int HighScore
    {
        get { return PlayerPrefs.GetInt("HighScore", 0); }
        set { PlayerPrefs.SetInt("HighScore", value); }
    }
}
