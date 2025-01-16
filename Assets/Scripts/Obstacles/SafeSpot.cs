using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeSpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (SceneManager.GetActiveScene().name == "GDTLevel")
        {
            if (SavePlayerData.Instance.LoadData<GDTLevelData>().RespawnTag != this.gameObject.tag)
            {
                GDTLevelData data = SavePlayerData.Instance.LoadData<GDTLevelData>();
                data.RespawnTag = this.gameObject.tag;
                SavePlayerData.Instance.SaveData(data);
            }
        }
        else if (SceneManager.GetActiveScene().name == "AGVEScene")
        {
            if (SavePlayerData.Instance.LoadData<AGVELevelData>().RespawnTag != this.gameObject.tag)
            {
                AGVELevelData data = SavePlayerData.Instance.LoadData<AGVELevelData>();
                data.RespawnTag = this.gameObject.tag;
                SavePlayerData.Instance.SaveData(data);
            }
        }
    }
}
