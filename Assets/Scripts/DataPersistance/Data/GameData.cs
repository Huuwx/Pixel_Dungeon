using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float musicVolume;
    public float SFXVolume;

    public GameData()
    {
        musicVolume = 1.0f;
        SFXVolume = 1.0f;
    }
}
