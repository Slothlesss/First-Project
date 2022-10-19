using GameClient.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatServer : MonoBehaviour
{
    #region ================Request==============

    public static void SendChat(ChatCode chatType, string mess )                                                             //Gửi nội dung chat.
    {
        var data = new Dictionary<byte, object>();
        data[1] = chatType;
        data[2] = mess;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Chat, data, true);
    }

    #endregion
    
    #region ================Reponse==============
    public static void Response(Dictionary<byte, object> dt)                                                                 // Nhận nội dung chat.                          
    {
        switch (dt[1])
        {
            case (int)ChatCode.Room:
                 ChatReponse.ChatInRoom(dt);
                 break;
            case (int)ChatCode.Global:
                 ChatReponse.ChatInGlobal(dt);
                 break;
            case (int)ChatCode.Private:
                 ChatReponse.ChatInPrivate(dt);
                 break;
        }
    }

    #endregion
}
