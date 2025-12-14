using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
    public enum UpgradeType { Pierce, Return, Rapid }
    public UpgradeType upgrade;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // find a GameObject named GameManager or tagged GameManager
        GameObject gmObj = GameObject.Find("GameManager");
        if (gmObj == null)
            gmObj = GameObject.FindWithTag("GameManager");

        if (gmObj == null)
        {
            Debug.LogWarning("No GameManager GameObject found to notify about upgrade pickup.");
            Destroy(gameObject);
            return;
        }

        switch (upgrade)
        {
            case UpgradeType.Pierce:
                gmObj.SendMessage("SetPierce", SendMessageOptions.DontRequireReceiver);
                break;
            case UpgradeType.Return:
                gmObj.SendMessage("SetReturn", SendMessageOptions.DontRequireReceiver);
                break;
            case UpgradeType.Rapid:
                gmObj.SendMessage("SetRapid", SendMessageOptions.DontRequireReceiver);
                break;
        }

        Destroy(gameObject);
    }
}
