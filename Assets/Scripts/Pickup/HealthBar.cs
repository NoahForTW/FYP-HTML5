using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    [SerializeField] private GameObject heartContainerPrefab;
    [SerializeField] private List<GameObject> heartContainers;
    private int totalHearts;
    private float currentHearts;
    private float displayedHearts;
    private Coroutine healthUpdateCoroutine;

    [SerializeField] private Animator deathAnimation;

    [SerializeField] private GameObject deathPanel;

    private HeartContainer currentContainer;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        heartContainers = new List<GameObject>();
        displayedHearts = currentHearts;
    }

    private void Start()
    {
        deathPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetUpPlayerHealth()
    {
        SetupHearts(3);
    }

    private void SetupHearts(int heartsIn)
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

    private void SetCurrentHealth(float health)
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

    public void AddHearts(float healthUp)
    {
        currentHearts += healthUp;
        if (currentHearts > totalHearts)
        {
            currentHearts = (float)totalHearts;
        }

        SaveHealth(currentHearts);
        SetCurrentHealth(currentHearts);
        TriggerFlash(false);
    }

    public void RemoveHearts(float healthDown)
    {
        currentHearts -= healthDown;
        if (currentHearts < 0)
        {
            currentHearts = 0f;
        }
        SaveHealth(currentHearts);
        SetCurrentHealth(currentHearts);
        TriggerFlash(true);

        // Check for death
        if (currentHearts <= 0)
        {
            Die();
        }
    }

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

    private void TriggerFlash(bool isHealthDecreasing)
    {
        Color flashColor = isHealthDecreasing ? Color.red : Color.green;
        float flashDuration = 0.2f;

        foreach (var heart in heartContainers)
        {
            HeartContainer heartContainer = heart.GetComponent<HeartContainer>();
            heartContainer.Flash(flashColor, flashDuration);
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        PlayerController.Instance.playerAction.Invoke(PlayerAction.Die);

        // Start the coroutine to wait for the animation and show the death panel
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Wait for the death animation to finish
        if (deathAnimation != null)
        {
            // Get the length of the current animation state (assuming it's the death animation)
            float animationLength = deathAnimation.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
        }

        // Show the death panel
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0;
    }

    void SaveHealth(float newHealth)
    {
        GameData data = SavePlayerData.Instance.LoadData<GameData>();
        data.playerHealth = newHealth;
        SavePlayerData.Instance.SaveData(data);
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
        SetUpPlayerHealth();
        SetCurrentHealth(PlayerInventory.Instance.GetCurrentHearts());
    }


}
