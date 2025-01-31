using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    public string TargetScene = "";
    public string TargetObstacleTag = "";

    public void SetObstacle()
    {
        ObstaclesData obstacles = new ObstaclesData();
        obstacles.TagName = TargetObstacleTag;
        if (TargetScene == "GDTLevel")
        {
            GDTLevelData data = SavePlayerData.Instance.LoadData<GDTLevelData>();

            if (!data.ObstaclesData.Contains(obstacles))
                data.ObstaclesData.Add(obstacles);
            SavePlayerData.Instance.SaveData(data);
        }
        else if (TargetScene == "AGVEScene")
        {
            AGVELevelData data = SavePlayerData.Instance.LoadData<AGVELevelData>();
            if (!data.ObstaclesData.Contains(obstacles))
                data.ObstaclesData.Add(obstacles);
            SavePlayerData.Instance.SaveData(data);
        }
    }
}
