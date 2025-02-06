using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public enum CosmeticType
{
    Default,
    Chroma,
    SciFi,
    Retro

}

public class PlayerSkins : MonoBehaviour
{
    public static PlayerSkins Instance;

    List<SpriteResolver> resolvers = new List<SpriteResolver>();
    private void Awake()
    {
        Instance = this;
        resolvers = GetComponentsInChildren<SpriteResolver>().ToList();
    }
    public void ChangeSkin(CosmeticType type)
    {
        // update player prefs
        PlayerData data = SavePlayerData.Instance.LoadData<PlayerData>();
        data.currentCosmetic = type;
        SavePlayerData.Instance.SaveData(data);

        // set skin
        SetAllResolvers(type);
    }
    void SetAllResolvers(CosmeticType type)
    {
        foreach (var resolver in resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(), type.ToString());
        }
    }

    void SetSavedSkin()
    {
        if (SavePlayerData.Instance != null)
        {
            CosmeticType type = SavePlayerData.Instance.LoadData<PlayerData>().currentCosmetic;
            SetAllResolvers(type);
        }
    }
    private void OnEnable()
    {
        SetSavedSkin();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetSavedSkin();
    }
}
