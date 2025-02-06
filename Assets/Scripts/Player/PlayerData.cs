using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int Coins = 0;
    public CosmeticType currentCosmetic = CosmeticType.Default;
    public List<CosmeticType> cosmeticsOwned = new List<CosmeticType>();
    public AudioSettings audioSettings= new AudioSettings();
}
[Serializable]
public class GameData
{
    public float playerHealth = 3f;
    public List<SettingsActive> SettingsActive = new List<SettingsActive>();
    public MinigameManagerData minigameManagerData = new MinigameManagerData(); 
}
public class SettingsActive
{
    public string settingsName;
    public bool isActive = true;
}

public class AudioSettings
{
    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;
}

[Serializable]
public class LevelData
{
    public string RespawnTag = "";
    public List<SceneGameObjects> SceneGameObjects = new List<SceneGameObjects>();
    public List<ObstaclesData> ObstaclesData = new List<ObstaclesData>();
    public List<ItemsInScene> Coins = new List<ItemsInScene>();
    public List<ItemsInScene> Hearts = new List<ItemsInScene>();
    public List<ItemsInScene> Cosmetics = new List<ItemsInScene>();
    public List<ItemsInScene> Enemies = new List<ItemsInScene>();
}
[Serializable]
public class GDTLevelData : LevelData
{

}
public class AGVELevelData : LevelData
{

}

[Serializable]
public class SceneGameObjects
{
    public string TagName = "";
    public bool isActive = true;
}
[Serializable]
public class ItemsInScene
{
    public int index;
    public bool isActive = true;
}
[Serializable]
public class ObstaclesData : SceneGameObjects
{

}

[Serializable]
public class MinigameManagerData
{
    public int ClueCostMultiplier = 1;
    public List<MinigameData> minigameDatas = new List<MinigameData>();

}

public class MinigameData
{
    public MinigameType MinigameType;
    public bool isCompleted = true;
}


