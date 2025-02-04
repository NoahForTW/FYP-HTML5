using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public int NoOfCoins { get; private set; }
    public int NoOfHearts { get; private set; }

    public UnityEvent CoinCollected;
    public UnityEvent HeartCollected;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        CoinCollected.AddListener(OnCoinCollected);
        HeartCollected.AddListener(OnHeartCollected);
    }
    public void OnCoinCollected()
    {
        //NoOfCoins++;
        AddCoins(1);
    }
    public void OnHeartCollected()
    {
        HealthBar.instance.AddHearts(1);
    }

    public void AddCoins(int amount)
    {
        GameData data = SavePlayerData.Instance.LoadData<GameData>();
        data.Coins += amount;
        SavePlayerData.Instance.SaveData(data);
    }

    public void RemoveCoins(int amount)
    {
        GameData data = SavePlayerData.Instance.LoadData<GameData>();
        data.Coins += amount;
        if (data.Coins < 0)
            data.Coins = 0;
        SavePlayerData.Instance.SaveData(data);
    }

    public int GetCurrentCoins()
    {
        return SavePlayerData.Instance.LoadData<GameData>().Coins;
    }

    public float GetCurrentHearts()
    {
        return SavePlayerData.Instance.LoadData<GameData>().playerHealth;
    }
    public void CosmeticCollected()
    {

    }
}
