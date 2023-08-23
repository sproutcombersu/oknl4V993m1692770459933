using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public string urlPlayer;
    public string nameObjPlayer;
    public string urlBgMenu;
    public string urlBgGamePlay;
}

[System.Serializable]
public class RootEASettingModel
{
    public Settings Settings;
}