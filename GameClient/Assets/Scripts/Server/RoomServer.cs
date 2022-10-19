using GameClient.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomServer
{
    #region ================Request==============

    public static void SendJoinRoom(int roomID)                                    // Request Vào phòng
    {
        var data = new Dictionary<byte, object>();
        data[1] = RoomCode.JoinRoom;
        data[2] = roomID;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Room, data, true);
        //Debug.Log("Đã vào phòng số  " + roomID);
    }
    public static void SendGetListRoom()                                                            // Request lấy danh sách phòng
    {
        var data = new Dictionary<byte, object>();
        data[1] = RoomCode.GetListRoom;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Room, data, true);
    }

    #endregion


    #region ================Reponse==============
    public static void Response(Dictionary<byte, object> dt)
    {
        switch ((int)dt[1])
        {
            case (int)RoomCode.GetListRoom:
                RoomReponse.GetListRoom(dt);
                break;
            case (int)RoomCode.LeaveRoom:
                RoomReponse.LeaveRoom(dt);
                break;
            case (int)RoomCode.JoinRoom:
                RoomReponse.JoinRoom(dt);
                break;
            case (int)RoomCode.Kick:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
    #endregion
}

