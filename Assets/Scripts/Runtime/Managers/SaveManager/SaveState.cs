using System;
using UnityEngine;

[Serializable]
public class SaveState
{
    [Header("RESOURCES")]
    public int Coin;

    [Space(20)]
    [Header("PLAYER PREFS")]
    public bool IsSoundOn;
    public bool IsVibrationOn;
    public bool IsTutorialDone;

    [Space(20)]
    [Header("LEVEL")]
    public int LevelIndex;
    public int LevelCounter;

    public int LastLevelIndex;

    public bool IsRandomLevelOn;


    public void InitSaveState( )
    {  
        //RESOURCES
        Coin =0;

        //PLAYER PREFS
        IsSoundOn = true;
        IsVibrationOn = false;
        IsTutorialDone = false;

        //LEVEL 
        LevelIndex = 0;
        LevelCounter = 1;
    }
}

