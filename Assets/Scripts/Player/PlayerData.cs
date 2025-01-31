using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    int Coins = 0;
}
[Serializable]
public class GameData
{
    int Coins = 0;
    public int playerHealth = 3;
}
[Serializable]
public class LevelData
{
    public string RespawnTag = "";
    public List<SceneGameObjects> SceneGameObjects = new List<SceneGameObjects>();
    public List<MinigameNPCData> MinigameNPCData = new List<MinigameNPCData>();
    public List<ObstaclesData> ObstaclesData = new List<ObstaclesData>();
    public List<SceneGameObjects> Coins = new List<SceneGameObjects>();
    public List<SceneGameObjects> HealthPoints = new List<SceneGameObjects>();
    public List<SceneGameObjects> Enemies = new List<SceneGameObjects>();
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
public class MinigameNPCData : SceneGameObjects
{
    public bool isMinigameCompleted = false;
}
[Serializable]
public class ObstaclesData : SceneGameObjects
{

}
