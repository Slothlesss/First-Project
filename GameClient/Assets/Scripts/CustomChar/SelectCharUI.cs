using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharUI : MainBehaviour
{
    [SerializeField] protected MainMenuUI mainMenuUI;
    [SerializeField] protected List<CharacterSlot> charSlotList;
    public MainMenuUI GetMainMenuUI()
    {
        return this.mainMenuUI;
    }
    protected override void AddComponent()
    {
        LoadMainMenuUI();
        LoadCharSlotList();
    }
    
    protected void LoadMainMenuUI()
    {
        if (mainMenuUI != null) return;
        this.mainMenuUI = transform.parent.GetComponent<MainMenuUI>();
    }
    protected void LoadCharSlotList()
    {
        if (this.charSlotList.Count != 0) return;
        foreach (Transform transform in transform.Find("CharSlotList"))
        {
            this.charSlotList.Add(transform.GetComponent<CharacterSlot>());
        }
    }

    public void InSelectedRoom(int roomId)
    {

        for (int i = 0; i < charSlotList.Count; i++)
        {
            charSlotList[i].LoadCharSaveData(roomId); // Load hết dữ liệu trong room hiện tại vào các slot
        }
       
    }

    // Chọn nhân vật và vào game
    public void InSelectChar(int slotNumber)
    {
        var charSaveData = CharacterSaveData.Instance;
        RoomServer.SendJoinRoom(charSaveData.idServer);
        GameplaySaveData.Instance.SetCharData(charSaveData.room[charSaveData.idServer].charDataList[slotNumber]);
    }
}
