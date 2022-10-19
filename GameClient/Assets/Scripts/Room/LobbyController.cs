using GameClient.Constructor;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Transform contentT;
    public GameObject templateObj;
    [SerializeField] protected MainMenuUI mainMenuUI;
    public List<RoomInfo> roomInfos = new List<RoomInfo>();
    public static LobbyController Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start() => GetListRoom();
    public void SpawnRoom()
    {
            foreach (RoomInfo room in roomInfos)
            {
                GameObject obj = Instantiate(templateObj, contentT);
                obj.transform.GetChild(0).GetComponent<Text>().text = $"S{room.RoomID} - {room.NameRoom}";
                obj.GetComponent<Button>().onClick.AddListener(() => SelectServerUI.instance.SelectRoom(room.RoomID, room.NameRoom));
            }
    }
    public void GetListRoom() => RoomServer.SendGetListRoom();
}