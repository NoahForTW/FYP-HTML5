using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TabWindow : MonoBehaviour
{
    [SerializeField] CosmeticType type;
    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject equipButton;
    [SerializeField] GameObject unequipButton;
    [SerializeField] TMP_Text CostText;
    [SerializeField] int Cost;

    private void OnEnable()
    {
        SetButtons();
        
        CostText.text = Cost.ToString();
    }

    void SetButtons()
    {
        List<CosmeticType> owned = SavePlayerData.Instance.LoadData<PlayerData>().cosmeticsOwned;
        CosmeticType currentType = SavePlayerData.Instance.LoadData<PlayerData>().currentCosmetic;
        buyButton.SetActive(!owned.Any(skin => skin == type));

        equipButton.SetActive(owned.Any(skin => skin == type) && currentType != type);
        unequipButton.SetActive(owned.Any(skin => skin == type) && currentType == type);

    }
    public void BuySkin()
    {

        if (PlayerInventory.Instance?.GetCurrentCoins() >= Cost)
        {
            // got moneh
            PlayerInventory.Instance.RemoveCoins(Cost);

            //update player prefs for owned skins
            PlayerData data = SavePlayerData.Instance.LoadData<PlayerData>();

            if (!data.cosmeticsOwned.Any(skin => skin == type))
            {
                data.cosmeticsOwned.Add(type);
                SavePlayerData.Instance.SaveData(data);
            }

            SetButtons();
        }
        else
        {
            // notif if not enough moneh
        }
    }

    public void EquipSkin()
    {
        PlayerSkins.Instance.ChangeSkin(type);
        SetButtons();
    }

    public void UnEquipSkin() {

        // set to default
        PlayerSkins.Instance.ChangeSkin(CosmeticType.Default);
        SetButtons();
    }

}
