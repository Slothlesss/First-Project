using DamageNumbersPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupNotification : MonoBehaviour
{
    public static PopupNotification instance;
    public DamageNumber ErrorPrefab;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }
    public static void Notification(string TextNotification)
    {
        DamageNumber damageNumber = instance.ErrorPrefab.Spawn(Vector3.zero, TextNotification);
    }
}
 