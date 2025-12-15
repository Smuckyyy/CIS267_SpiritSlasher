using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}