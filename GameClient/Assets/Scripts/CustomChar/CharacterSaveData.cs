using System.Collections.Generic;
using UnityEngine;

public class CharacterSaveData : MainBehaviour
{
    private static CharacterSaveData instance;
    public static CharacterSaveData Instance => instance;

    [Header("Current Server")]
    public int idServer = 0;
    public string nameServer = "Room1";
    [Space(10)] public List<CharacterData> room;
    protected override void Awake() => instance = this;

}
