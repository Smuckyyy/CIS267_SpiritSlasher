using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }
    private bool pierceUpgrade = false;
    private bool ReturnUpgrade = false;
    private bool rapidUpgrade = false;
    // these are upgrades player would collect


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetPierce()
    {
        pierceUpgrade = true;
    }
    public bool GetPierce()
    {
        return pierceUpgrade;
    }
    public void SetReturn()
    {
        ReturnUpgrade = true;
    }
    public bool GetReturn()
    {
        return ReturnUpgrade;
    }
    public void SetRapid()
    {
        rapidUpgrade = true;
    }
    public bool GetRapid()
    {
        return rapidUpgrade;
    }

}
