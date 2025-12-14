using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Backing field for the singleton instance
    private static GameManager _instance;

    // Public accessor with lazy-find fallback so other scripts can access instance even if
    // the GameManager wasn't explicitly placed in the scene.
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                // Try to find an existing GameManager in the scene
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    // If none found, create a new GameObject with this component so calls don't fail.
                    var go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }

    private bool trippleUpgrade = false;
    private bool ReturnUpgrade = false;
    private bool rapidUpgrade = false;
    // these are upgrades player would collect


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void Settriple()
    {
        trippleUpgrade = true;
    }
    public bool Gettriple()
    {
        return trippleUpgrade;
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
