using GameClient.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Select_char : MonoBehaviour
{
    // Update is called once per frame
    public  void select_slot()
    {
        if(this.name == "Select_1")
        {
            if (CustomCharUI.List_slot[0] != 0)
            {
                //Debug.Log("slot 1:" + CustomCharUI.List_slot[0]);
                var data = new Dictionary<byte, object>();
                data[1] = RoomCode.CharJoinRoom;
                data[2] = CustomCharUI.List_slot[0];
                PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Room, data, true);
            }
        }

        if (this.name == "Select_2")
        {
            if(CustomCharUI.List_slot[1] != 0)
            {
                //Debug.Log("slot 2:" + CustomCharUI.List_slot[1]);
                var data = new Dictionary<byte, object>();
                data[1] = RoomCode.CharJoinRoom;
                data[2] = CustomCharUI.List_slot[1];
                PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Room, data, true);
            }
        }

        if (this.name == "Select_3")
        {
            if (CustomCharUI.List_slot[2] != 0)
            {
                //Debug.Log("slot 3: " + CustomCharUI.List_slot[2]);
                var data = new Dictionary<byte, object>();
                data[1] = RoomCode.CharJoinRoom;
                data[2] = CustomCharUI.List_slot[2];
                PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Room, data, true);
            }
        }
    }
}
