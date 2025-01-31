using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTest : MonoBehaviour
{
    int total;
    float amountUp;
    float amountDown;

    //public void TotalInput(string valueIn)
    // {
    //    total = int.Parse(valueIn);
    //}
    public void SubmitSetup()
    {
        int health = SavePlayerData.Instance.LoadData<GameData>().playerHealth;
        HealthBar.instance.SetupHearts(health);
    }
    private void Start()
    {

    }
    public void UpAmountInput(string valueIn)
    {
        amountUp = float.Parse(valueIn);
    }
    public void SubmitUp()
    {
        HealthBar.instance.AddHearts(amountUp);
    }
    public void DownAmountInput(string valueIn)
    {
        amountDown = float.Parse(valueIn);
    }
    public void SubmitDown()
    {
        HealthBar.instance.RemoveHearts(amountDown);
    }
    public void AddHeartContainer()
    {
        HealthBar.instance.AddContainer();
    }
}
