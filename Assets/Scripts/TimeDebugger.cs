using UnityEngine;

public class TimeDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("TIMESCALE = " + Time.timeScale);
        }
    }
}