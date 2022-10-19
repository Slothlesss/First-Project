using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatReponse : MonoBehaviour
{
    public static void ChatInRoom(Dictionary<byte, object> dt)
    {
        Debug.Log($"[Room] - {dt[4]}: " + dt[2]);
    }

    internal static void ChatInGlobal(Dictionary<byte, object> dt)
    {
        Debug.Log($"[Global] - {dt[4]}: " + dt[2]);
    }

    internal static void ChatInPrivate(Dictionary<byte, object> dt)
    {
        Debug.Log($"[Private] - {dt[4]}: " + dt[2]);
    }
}
