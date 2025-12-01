using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Collectable Settings")]
    public int totalCollectablesNeeded = 3;
    private int goldenCollected = 0;

    [Header("Next Level Name")]
    public string nextLevelName;

    void Awake()
    {
        instance = this;
    }

    public void AddCollectable()
    {
        goldenCollected++;

        Debug.Log("Golden collected: " + goldenCollected);

        if (goldenCollected >= totalCollectablesNeeded)
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Next level name not set!");
        }
    }
}
