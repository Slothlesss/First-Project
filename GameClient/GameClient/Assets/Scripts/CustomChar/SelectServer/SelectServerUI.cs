using GameClient.Enums;
using System.Collections.Generic;
using UnityEngine;

public class SelectServerUI : MainBehaviour
{
    [SerializeField] protected MainMenuUI mainMenuUI;
    public static SelectServerUI instance;
    private void Start()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
    protected override void AddComponent() => LoadMainMenuUI();

    protected void LoadMainMenuUI()
    {
        if (mainMenuUI != null) return;
        this.mainMenuUI = transform.parent.GetComponent<MainMenuUI>();
    }

    // Chọn room với dữ liệu Room truyền vào
    public void SelectRoom(int RoomId , string RoomName)
    {
        if (CharacterSaveData.Instance.room.Find(i => i.roomId == RoomId) == null) CharacterSaveData.Instance.room.Add(new CharacterData() { roomId = RoomId}); 
        // Set current server
        CharacterSaveData.Instance.idServer = RoomId;
        CharacterSaveData.Instance.nameServer = RoomName;

        this.mainMenuUI.SelectCharUI.InSelectedRoom(RoomId);
        this.mainMenuUI.SelectCharUI.gameObject.SetActive(true);
        GetDataJsonChar();
        this.gameObject.SetActive(false);
    }
    
    // Chọn room hiện tại (tương tự hàm SelectRoom)
    public void SelectCurrentRoom()
    {
        var roomId = CharacterSaveData.Instance.idServer;
        if (CharacterSaveData.Instance.room.Find(i => i.roomId == roomId) == null) CharacterSaveData.Instance.room.Add(new CharacterData() { roomId = roomId });

        this.mainMenuUI.SelectCharUI.InSelectedRoom(roomId);
        this.mainMenuUI.SelectCharUI.gameObject.SetActive(true);
        GetDataJsonChar();
        this.gameObject.SetActive(false);
    }
    public void GetDataJsonChar()
    {
        //Debug.Log("Lấy dữ liệu json");
        var data = new Dictionary<byte, object>();
        data[1] = GameplayCode.GetDataJsonChar;
        data[2] = CharacterSaveData.Instance.idServer;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.CustomChar, data, true);
    }
}
