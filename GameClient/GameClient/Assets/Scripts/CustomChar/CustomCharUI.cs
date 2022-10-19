using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.CharacterScripts;
using GameClient.Enums;
using System;

public class CustomCharUI : MainBehaviour
{
    [field: SerializeField] public int SlotToSave { get; set; } 
    [SerializeField] protected MainMenuUI mainMenuUI;
    [SerializeField] protected Character4D characterCreate;
    public static CustomCharUI instance;
    public static List<int> List_slot = new List<int>();

    private void Start()
    {
        if (instance != null ) Destroy(instance);
        instance = this;
    }
    protected override void AddComponent()
    {
        LoadMainMenuUI();
        LoadCharCreate();
    }

    public static void Response(Dictionary<byte, object> dt)
    {
        switch (dt[1])
        {
            case (int)GameplayCode.GetDataJsonChar:
                SlotToSaveDataJsonChar(dt);
                break;
        }
    }

    private static void SlotToSaveDataJsonChar(Dictionary<byte, object> dt)
    {
        var charData = CharacterSaveData.Instance.room.Find(i => i.roomId == CharacterSaveData.Instance.idServer);
        if (!charData.charDataList.Contains((string)dt[2])) charData.charDataList.Add((string)dt[2]);
        else return;
        instance.mainMenuUI.SelectCharUI.InSelectedRoom(CharacterSaveData.Instance.idServer);
        if (!List_slot.Contains(Convert.ToInt32(dt[3]))) List_slot.Add(Convert.ToInt32(dt[3]));
    }

    protected void LoadMainMenuUI()
    {
        if (mainMenuUI != null) return;
        this.mainMenuUI = transform.parent.GetComponent<MainMenuUI>();
    }
    protected void LoadCharCreate()
    {
        if (characterCreate != null) return;
        this.characterCreate = transform.Find("CharacterCreate").GetComponent<Character4D>();
    }
    public void ApplyCustomChar()
    {
        var charData = CharacterSaveData.Instance.room.Find(i => i.roomId == CharacterSaveData.Instance.idServer);
        var data = new Dictionary<byte, object>();
        data[1] = GameplayCode.CustomjsonChar;
        data[2] = CharacterSaveData.Instance.idServer;
        data[3] = charData.charDataList.Count;
        data[4] = characterCreate.ToJson();
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.CustomChar, data, true);
        CloseCustomChar();
    }

    public void CloseCustomChar()
    {
        var data = new Dictionary<byte, object>();
        data[1] = GameplayCode.GetDataJsonChar;
        data[2] = CharacterSaveData.Instance.idServer;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.CustomChar, data, true);
        mainMenuUI.SelectCharUI.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
        
}
