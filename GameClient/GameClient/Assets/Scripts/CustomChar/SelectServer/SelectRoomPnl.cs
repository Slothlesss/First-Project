using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectRoomPnl : MainBehaviour
{
    private SelectServerUI selectServerUI;
    [SerializeField] private Transform roomListTranf;
    [SerializeField] private List<Room> roomButtonList;
    protected override void AddComponent()
    {
        this.LoadSelectServerUI();
        this.LoadRoomListTranf();
        this.LoadRoomButtonList();
        this.AddListenerToRoomBtn();
    }
    private void LoadSelectServerUI()
    {
        if (this.selectServerUI != null) return;
        this.selectServerUI = transform.parent.GetComponent<SelectServerUI>();
    }
    private void LoadRoomListTranf()
    {
        if (this.roomListTranf != null) return;
        this.roomListTranf = GameObject.Find("RoomList").transform;
    }
    private void LoadRoomButtonList()
    {
        if (this.roomButtonList.Count != 0) return;
        foreach (Transform room in roomListTranf)
        {
            this.roomButtonList.Add(room.GetComponent<Room>());
        }
    }
    // Gan chuc nang cho room button
    private void AddListenerToRoomBtn()
    {
        foreach (Room room in this.roomButtonList)
        {
            room.GetRoomBtn().onClick.AddListener(() =>
            {
                this.selectServerUI.SelectRoom(room.RoomId, room.name);
            });
        }
    }
}
