using GameClient.Constructor;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
public class RoomReponse
{
    public static void JoinRoom(Dictionary<byte, object> dt)                                           // Vào phòng. add người chơi mới vào phòng vào danh sách.
    {
        var player = JsonConvert.DeserializeObject<Player>((string)dt[2]);
        var curPlayer = GamePlayManager.Instance.Playerlist.Find(x => x.UserID == player.UserID);
        if (curPlayer != null) curPlayer = player;
        else GamePlayManager.Instance.Playerlist.Add(player);
        if (!GamePlayManager.Instance.Playerdict.ContainsKey(player.UserID)) GamePlayReponse.SpawnPlayer(player);
    }

    public static void LeaveRoom(Dictionary<byte, object> dt)                                          // Thoát khỏi phòng
    {
        Object.Destroy(GamePlayManager.Instance.Playerdict[(int)dt[2]]);             
        GamePlayManager.Instance.Playerdict.Remove((int)dt[2]);                                        //Remove người chơi. 
        GamePlayManager.Instance.Playerlist.RemoveAll(x => x.UserID == (int)dt[2]);                    // Xóa dữ liệu người chơi.
    }
    public static void GetListRoom(Dictionary<byte, object> dt)                                        // Lấy danh sách phòng.
    {
        LobbyController.Instance.roomInfos = JsonConvert.DeserializeObject<List<RoomInfo>>((string)dt[2]);
        LobbyController.Instance.SpawnRoom();
    }
}

