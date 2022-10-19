using UnityEngine;

public class CharacterID : MonoBehaviour
{
    public static CharacterID instance;
    public int Characterid;
    private void Awake() => instance = this;
}
