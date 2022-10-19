using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;
    public List<PopupInfo> PopupInfo = new List<PopupInfo>();
    // Start is called before the first frame update
    private void Awake() => instance = this;
    public void GetPopup(string name)
    {
        foreach (var popup in PopupInfo)
        {
            if (popup.name == name)
            {
                popup.obj.SetActive(true);
                return;
            }
        }
    }
    public void DisableAllPopUp()
    {
        foreach (var popup in PopupInfo)
        {
            if (popup.name == name)
            {
                popup.obj.SetActive(false);
                return;
            }
        }
    }
}

[System.Serializable]
public class PopupInfo
{
    public string name;
    public GameObject obj;
}