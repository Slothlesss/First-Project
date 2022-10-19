using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using GameClient.Constructor;
using Assets.HeroEditor4D.Common.ExampleScripts;
using Assets.HeroEditor4D.Common.CommonScripts;

public class GamePlayReponse : MonoBehaviour
{

    public static void Move(Dictionary<byte, object> data)
    {
        var pos = JsonConvert.DeserializeObject<UnityEngine.Vector3>((string)data[2]);
        var userID = (int)data[4];
        GamePlayManager.Instance.Playerdict[userID].transform.position = pos;

        var PlayerList = GamePlayManager.Instance.Playerlist;
        var Playerdict = GamePlayManager.Instance.Playerdict;
        foreach (var p in PlayerList) if (p.UserID == (int)data[4]) Playerdict[p.UserID].GetComponent<CharacterAnim>().moving = (bool)data[3];
    }

    internal static void MoveStop(Dictionary<byte, object> data)
    {
        var PlayerList = GamePlayManager.Instance.Playerlist;
        var Playerdict = GamePlayManager.Instance.Playerdict;
        foreach (var player in PlayerList) if (player.UserID == (int)data[3]) Playerdict[player.UserID].GetComponent<CharacterAnim>().moving = (bool)data[2];
    }

    public static void GetData(Dictionary<byte, object> dt)
    {
        var PlayerList = JsonConvert.DeserializeObject<List<Player>>((string)dt[2]);
        GamePlayManager.Instance.Playerlist = PlayerList;
        foreach(var p in PlayerList) SpawnPlayer(p);
    }

    internal static void Directions(Dictionary<byte, object> dt)  
    {
        Vector2 directions;
        if ((((int[])dt[2])[0] != 0)) directions = Vector2.left;
        else if (((int[])dt[2])[1] != 0) directions = Vector2.right;
        else if (((int[])dt[2])[2] != 0) directions = Vector2.up;
        else if (((int[])dt[2])[3] != 0) directions = Vector2.down;
        else return;

        var PlayerList = GamePlayManager.Instance.Playerlist;
        var Playerdict = GamePlayManager.Instance.Playerdict;
        foreach (var p in PlayerList) if (p.UserID == (int)dt[4]) Playerdict[p.UserID].GetComponent<CharacterControls>().Character.SetDirection(directions);
        
    }

    public static void SpawnPlayer(Player player)
    {
        var Playerdict = GamePlayManager.Instance.Playerdict;
        if (!Playerdict.ContainsKey(player.UserID))
        {
            GameObject newPlayer = Instantiate(GamePlayManager.Instance.playerTemplate, new UnityEngine.Vector3(player.position.x, player.position.y, player.position.z), Quaternion.identity);
            Playerdict.Add(player.UserID, newPlayer);
            newPlayer.GetComponent<CharManager>().Create_player(player.JsonChar);
            newPlayer.transform.name = "player_" + player.UserID;
            newPlayer.tag = "Player";
            if (player.UserID == UserManager.userData.UserID)
            {
                newPlayer.transform.name = "player_main";
                newPlayer.GetComponent<CharacterID>().Characterid = player.UserID;
                Playerdict[player.UserID].GetComponent<CharacterControls>().enabled = true;
                Playerdict[player.UserID].GetComponent<CharacterSkill>().enabled = true;
            }
            else
            {
                newPlayer.transform.GetChild(5).SetActive(false);
                newPlayer.GetComponent<CharacterAnim>().enabled = true;
                newPlayer.GetComponent<CharacterID>().Characterid = player.UserID;
            }
        }
    }
}