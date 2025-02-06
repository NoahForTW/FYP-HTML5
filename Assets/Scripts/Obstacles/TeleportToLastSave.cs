using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToLastSave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerFeet"))
        {
            AudioManager.instance.PlaySoundOneShot(SoundType.Hurt);
            HealthBar.instance.RemoveHearts(1);

            if (SceneManager.GetActiveScene().name == "GDTLevel")
            {
                if (!string.IsNullOrEmpty(SavePlayerData.Instance.LoadData<GDTLevelData>().RespawnTag))
                {
                    PlayerController.Instance.SpawnPlayer(SavePlayerData.Instance.LoadData<GDTLevelData>().RespawnTag);
                }
            }
            else if (SceneManager.GetActiveScene().name == "AGVEScene")
            {
                if (!string.IsNullOrEmpty(SavePlayerData.Instance.LoadData<AGVELevelData>().RespawnTag))
                {
                    PlayerController.Instance.SpawnPlayer(SavePlayerData.Instance.LoadData<AGVELevelData>().RespawnTag);

                }
            }
        }
        
    }
}
