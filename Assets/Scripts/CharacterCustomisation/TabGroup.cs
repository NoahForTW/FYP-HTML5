using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabsButton> tabButtons;
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;
    [SerializeField] private TabsButton selectedTab;
    [SerializeField] private List<GameObject> objectToSwap;

    public void Subscribe(TabsButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabsButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabsButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;  
        }
    }

    public void OnTabExit(TabsButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabsButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i=0; i < objectToSwap.Count; i++)
        {
            if(i == index)
            {
                objectToSwap[i].SetActive(true);
            }
            else
            {
                objectToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabsButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab)
            {
                continue;
            }
            button.background.sprite = tabIdle;
        }
    }
}
