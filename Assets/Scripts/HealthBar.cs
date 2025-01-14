using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    [SerializeField] private GameObject heartContainerPrefab;
    [SerializeField] private List<GameObject> heartContainers;
    private int totalHearts;
    private float currentHearts;
    private float displayedHearts;
    private Coroutine healthUpdateCoroutine;

    private HeartContainer currentContainer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        heartContainers = new List<GameObject>();
        displayedHearts = currentHearts;
    }
    //ZeldaHealthBar.instance.SetupHearts(valueIn);
    public void SetupHearts(int heartsIn)
    {
        heartContainers.Clear();
        for(int i = transform.childCount -1; i >=0; i--)
        {  
            Destroy(transform.GetChild(i).gameObject);
        }
        totalHearts = heartsIn;
        currentHearts = (float)totalHearts;
        displayedHearts = currentHearts;
        
        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartContainerPrefab, transform);
            heartContainers.Add(newHeart);
            if(currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HeartContainer>();
            }
            currentContainer = newHeart.GetComponent<HeartContainer>();
        }
        currentContainer = heartContainers[0].GetComponent<HeartContainer>();
    }
    //ZeldaHealthBar.instance.SetCurrentHealth(valueIn);
    public void SetCurrentHealth(float health)
    {
        currentHearts = health;
        if (healthUpdateCoroutine != null)
        {
            StopCoroutine(healthUpdateCoroutine);
        }
        healthUpdateCoroutine = StartCoroutine(SmoothHealthUpdate());
    }

    private IEnumerator SmoothHealthUpdate()
    {
        float updateSpeed = 0.5f; // Adjust for desired smoothness
        while (Mathf.Abs(displayedHearts - currentHearts) > 0.01f)
        {
            displayedHearts = Mathf.Lerp(displayedHearts, currentHearts, Time.deltaTime / updateSpeed);
            UpdateVisualHealth(displayedHearts);
            yield return null;
        }
        displayedHearts = currentHearts;
        UpdateVisualHealth(displayedHearts);
    }

    private void UpdateVisualHealth(float health)
    {
        currentContainer.SetHeart(health);
    }
    //ZeldaHealthBar.instance.AddHearts(valueIn);
    public void AddHearts(float healthUp)
    {
        currentHearts += healthUp;
        if (currentHearts > totalHearts)
        {
            currentHearts = (float)totalHearts;
        }
        SetCurrentHealth(currentHearts);
    }
    //ZeldaHealthBar.instance.RemoveHearts(valueIn);
    public void RemoveHearts(float healthDown)
    {
        currentHearts -= healthDown;
        if (currentHearts < 0)
        {
            currentHearts = 0f;
        }
        SetCurrentHealth(currentHearts);
    }
    //ZeldaHealthBar.instance.AddContainer(valueIn);
    public void AddContainer()
    {
        GameObject newHeart = Instantiate(heartContainerPrefab, transform);
        currentContainer = heartContainers[heartContainers.Count - 1].GetComponent<HeartContainer>();
        heartContainers.Add(newHeart);

        if (currentContainer != null)
        {
            currentContainer.next = newHeart.GetComponent<HeartContainer>();
        }

        currentContainer = heartContainers[0].GetComponent<HeartContainer>();
        totalHearts++;
        currentHearts = totalHearts;
        SetCurrentHealth(currentHearts);
    }
}
