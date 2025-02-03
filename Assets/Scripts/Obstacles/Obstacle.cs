using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public enum CompletionEffects
{
    None,
    Deactivate,
    Activate,
    PlayAnimation
}
public class Obstacle : MonoBehaviour
{
    Animator Animator;
    public string AnimationName;
    public UnityEvent Event;
    public UnityEvent AfterEvent;
    public CompletionEffects CompletionEffect;

    public bool DoSaveData = true;
    private void Awake()
    {
        if (TryGetComponent<Animator>(out Animator animator))
            Animator = animator;

        Event.AddListener(DoCompletionEvent);
    }

    public void DoCompletionEvent()
    {
        switch (CompletionEffect)
        {
            case global::CompletionEffects.Deactivate:
                StartDeactivativeGameObject();
                break;
            
            case global::CompletionEffects.Activate:
                StartActivativeGameObject();
                break;
            case global::CompletionEffects.PlayAnimation:
                PlayAnimation();
                break;
        }
        if (!DoSaveData)
            return;
        ObstaclesData obstacles = new ObstaclesData();
        obstacles.TagName = this.tag;
        if (SceneManager.GetActiveScene().name == "GDTLevel")
        {
            GDTLevelData data = SavePlayerData.Instance.LoadData<GDTLevelData>();

            if (!SavePlayerData.Instance.LoadData<GDTLevelData>().ObstaclesData.Any(obstacle => obstacle.TagName == this.tag))
                data.ObstaclesData.Add(obstacles);
            SavePlayerData.Instance.SaveData(data);
        }
        else if (SceneManager.GetActiveScene().name == "AGVEScene")
        {
            AGVELevelData data = SavePlayerData.Instance.LoadData<AGVELevelData>();
            if (!SavePlayerData.Instance.LoadData<AGVELevelData>().ObstaclesData.Any(obstacle => obstacle.TagName == this.tag))
                data.ObstaclesData.Add(obstacles);
            SavePlayerData.Instance.SaveData(data);
        }
    }
    public void PlayAnimation()
    {
        Animator?.SetBool(AnimationName, true);
    }

    public void StartDeactivativeGameObject()
    {
        StartCoroutine(DeactivateGameObject(this.gameObject, 2f));
    }
    IEnumerator DeactivateGameObject(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        go.SetActive(false);
    }
    public void StartActivativeGameObject()
    {
        StartCoroutine(ActivateGameObject(this.gameObject, 2f));
    }
    IEnumerator ActivateGameObject(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        GetComponent<SphereCollider>().enabled = true;
    }

    public void InvokeAfterEvent()
    {
        AfterEvent?.Invoke();
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
        if (scene.name == "GDTLevel")
        {
            bool hasSpecificTag = SavePlayerData.Instance.LoadData<GDTLevelData>().ObstaclesData.Any(obstacle => obstacle.TagName == this.tag);
            if (hasSpecificTag)
            {
                DoCompletionEvent();
            }
            
        }
        else if (scene.name == "AGVEScene")
        {
            bool hasSpecificTag = SavePlayerData.Instance.LoadData<AGVELevelData>().ObstaclesData.Any(obstacle => obstacle.TagName == this.tag);
            if (hasSpecificTag)
            {
                DoCompletionEvent();
            }
        }

    }
}
