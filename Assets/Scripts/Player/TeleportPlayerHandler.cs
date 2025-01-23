using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPlayerHandler : MonoBehaviour
{
    public string TargetScene = "";
    public string TargetSpawnTag = "";

    public void TeleportPlayer()
    {
        Debug.Log(this.gameObject.name);

        if (TargetScene == "GDTLevel")
        {
            GDTLevelData data = SavePlayerData.Instance.LoadData<GDTLevelData>();
            data.RespawnTag = TargetSpawnTag;
            SavePlayerData.Instance.SaveData(data);
        }
        else if (TargetScene == "AGVEScene")
        {
            AGVELevelData data = SavePlayerData.Instance.LoadData<AGVELevelData>();
            data.RespawnTag = TargetSpawnTag;
            SavePlayerData.Instance.SaveData(data);
        }

        if (SceneManager.GetActiveScene().name != TargetScene)
        {
            SceneLoader sceneLoader = FindAnyObjectByType<SceneLoader>();
            sceneLoader.LoadSceneWithLoading(TargetScene);
        }
        else
        {
            PlayerController.Instance.SpawnPlayer(TargetSpawnTag);
        }
    }
}
