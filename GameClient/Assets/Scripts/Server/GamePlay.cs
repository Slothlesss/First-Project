using System.Collections.Generic;
using UnityEngine;
using System;
using GameClient.Enums;

public class GamePlay : MonoBehaviour
{
    public static void Response(Dictionary<byte, object> dt)
    {
        switch ((int)dt[1])
        {
            case (int)GameplayCode.GetData:
                GamePlayReponse.GetData(dt);
                break;
            case (int)GameplayCode.Move:
                GamePlayReponse.Move(dt);
                break;
            case (int)GameplayCode.MoveStop:
                GamePlayReponse.MoveStop(dt);
                break;
            case (int)GameplayCode.Directions:
                GamePlayReponse.Directions(dt);
                break;
        }
    }
}