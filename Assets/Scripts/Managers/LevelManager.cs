using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

enum TypeOfItems
{
    Coin,
    Heart,
    Cosmetics,
    Enemy
}


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] List<GameObject> CoinsGO;
    [SerializeField] List<GameObject> HeartGO;
    [SerializeField] List<GameObject> CosmeticsGO;
    [SerializeField] List<GameObject> EnemiesGO;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        foreach (TypeOfItems type in Enum.GetValues(typeof(TypeOfItems)))
        {
            CheckIfGODeactivates(type);
        }
    }

    void CheckIfGODeactivates(TypeOfItems type)
    {
        List<GameObject> list = type switch
        {
            TypeOfItems.Coin => CoinsGO,
            TypeOfItems.Heart => HeartGO,
            TypeOfItems.Cosmetics => CoinsGO,
            TypeOfItems.Enemy => EnemiesGO,
        };

        foreach (GameObject item in list)
        {
            int index = CoinsGO.IndexOf(item);
            if (!item.activeSelf)
            {
                // add to data
                ItemsInScene itemData = new ItemsInScene();
                itemData.index = index;
                SaveItemData(type, itemData);
            }
        }
    }

    void SetItemActive(TypeOfItems type)
    {
        LevelData data = SceneManager.GetActiveScene().name == "GDTLevel"
        ? SavePlayerData.Instance.LoadData<GDTLevelData>()
        : SavePlayerData.Instance.LoadData<AGVELevelData>();

        // Get the correct list based on type
        var itemList = type switch
        {
            TypeOfItems.Coin => data.Coins,
            TypeOfItems.Heart => data.Hearts,
            TypeOfItems.Cosmetics => data.Cosmetics,
            TypeOfItems.Enemy => data.Enemies,
            _ => null
        };

        if (itemList != null)
        {
            List<GameObject> list = type switch
            {
                TypeOfItems.Coin => CoinsGO,
                TypeOfItems.Heart => HeartGO,
                TypeOfItems.Cosmetics => CoinsGO,
                TypeOfItems.Enemy => EnemiesGO,
            };
            foreach (GameObject item in list)
            {
                int index = CoinsGO.IndexOf(item);
                if (itemList.Any(data => data.index == index))
                {
                    item.SetActive(false);
                }
            }
        }

    }

    void SaveItemData<T>(TypeOfItems type, T currentData) where T : ItemsInScene, new ()
    {
        LevelData data = SceneManager.GetActiveScene().name == "GDTLevel"
                ? SavePlayerData.Instance.LoadData<GDTLevelData>()
                : SavePlayerData.Instance.LoadData<AGVELevelData>();

        // Get the correct list based on type
        var itemList = type switch
        {
            TypeOfItems.Coin => data.Coins,
            TypeOfItems.Heart => data.Hearts,
            TypeOfItems.Cosmetics => data.Cosmetics,
            TypeOfItems.Enemy => data.Enemies,
            _ => null
        };

        if (itemList != null && !itemList.Any(item => item.index == currentData.index))
        {
            itemList.Add(currentData);
            SavePlayerData.Instance.SaveData(data);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelData data = SceneManager.GetActiveScene().name == "GDTLevel"
        ? SavePlayerData.Instance.LoadData<GDTLevelData>()
        : SavePlayerData.Instance.LoadData<AGVELevelData>();

        // set active

        foreach (TypeOfItems type in Enum.GetValues(typeof(TypeOfItems)))
        {
            SetItemActive(type);
        }
    }
}
