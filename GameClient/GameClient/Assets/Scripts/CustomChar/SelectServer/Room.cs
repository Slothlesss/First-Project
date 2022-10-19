using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MainBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Text roomNameTxt;
    [field: SerializeField] public int RoomId { get; private set; }
    [field: SerializeField] public string RoomName { get; private set; }

    public Button GetRoomBtn()
    {
        return this.btn;
    }
    protected override void Awake()
    {
        base.Awake();
        //btn.GetComponent<Button>().onClick.AddListener(InvokeEvent);
    }
    protected override void AddComponent()
    {
        btn = transform.GetComponent<Button>();
        this.LoadRoomNameTxt();
        this.LoadRoomName();
    }
    private void LoadRoomNameTxt()
    {
        if (this.roomNameTxt != null) return;
        this.roomNameTxt = transform.GetChild(0).GetComponent<Text>();
    }
    private void LoadRoomName() => this.RoomName = this.roomNameTxt.text;
}
