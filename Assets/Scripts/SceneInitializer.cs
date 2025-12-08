using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    void Awake()
    {
        //Make sure the game is unfrozen and alive
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        //Rebind player reference if needed
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null)
        {
            Debug.LogError("No player found in scene!");
        }
    }
}