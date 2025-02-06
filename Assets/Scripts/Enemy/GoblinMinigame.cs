using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GoblinType
{
    Blocking,
    Coin,
    Heart
}

public class GoblinMinigame : MonoBehaviour
{
    [SerializeField] GameObject BlockingGoblin;
    [SerializeField] GameObject HeartGoblin;
    [SerializeField] GameObject CoinGoblin;

    float timer = 0;
    bool stopTimer = true;
    float cooldown;
    bool GoblinIsActive = false;

    Coroutine disrupt;

    private void Start()
    {
        cooldown = UnityEngine.Random.Range(15, 50);
    }
    private void Update()
    {
        stopTimer = MinigameManager.Instance.GetCurrentMinigame() == null || MinigameManager.Instance.GetPauseTimer();

        if (!stopTimer)
            timer += Time.deltaTime;

        if (timer >= cooldown && disrupt == null)
        {
            disrupt = StartCoroutine(DisruptMinigame());
        }

    }

    IEnumerator DisruptMinigame()
    {
        // disrupt ehe
        ExecuteGoblin();
        while (!GoblinIsActive)
        {
            yield return null; // Wait until the next frame
        }

        // reset timer, cooldown & goblins
        yield return new WaitForSecondsRealtime(2f);
        DisableGoblins();
        timer = 0;
        cooldown = UnityEngine.Random.Range(15, 50);
        
    }
    void ExecuteGoblin()
    {
        while (true)
        {
            GoblinType type = GetRandomEnumValue<GoblinType>();

            if (type == GoblinType.Heart)
            {
                float deduction = GetRandomFloatInSegments(0.25f, 1f, 0.25f);
                GameData playerData = SavePlayerData.Instance.LoadData<GameData>();

                if (playerData.playerHealth - deduction > 0)
                {
                    SetActiveGoblin(type);
                    HealthBar.instance.AddHearts(deduction);
                    break; 
                }
            }
            else if (type == GoblinType.Coin)
            {
                int deduction = UnityEngine.Random.Range(1, 20);
                GameData playerData = SavePlayerData.Instance.LoadData<GameData>();

                if (playerData.playerHealth - deduction > 0)
                {
                    SetActiveGoblin(type);
                    PlayerInventory.Instance.RemoveCoins(deduction);
                    break; 
                }
            }
            else
            {
                SetActiveGoblin(type);
                break; 
            }
        }
    }
    static float GetRandomFloatInSegments(float min, float max, float step)
    {
        System.Random random = new System.Random();
        int steps = (int)((max - min) / step); // Total number of steps
        int randomStep = random.Next(steps + 1); // Pick a random step
        return min + (randomStep * step); // Calculate the float value
    }
    static T GetRandomEnumValue<T>() where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        System.Random random = new System.Random();
        return (T)values.GetValue(random.Next(values.Length));
    }
    void SetActiveGoblin(GoblinType type)
    {
        BlockingGoblin.SetActive(type == GoblinType.Blocking);
        HeartGoblin.SetActive(type == GoblinType.Heart);
        CoinGoblin.SetActive(type == GoblinType.Coin);
        GoblinIsActive = true;
    }
    void DisableGoblins()
    {
        BlockingGoblin.SetActive(false);
        HeartGoblin.SetActive(false);
        CoinGoblin.SetActive(false);
        GoblinIsActive = false;
        if (disrupt != null)
        {
            StopCoroutine(disrupt);
            disrupt = null;
        }
    }
}
