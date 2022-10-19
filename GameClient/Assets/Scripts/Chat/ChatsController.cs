using GameClient.Enums;
using UnityEngine;
using UnityEngine.UI;

public class ChatsController : MonoBehaviour
{
    public InputField MessInput;
    public Button ButtonBtnSystem, ButtonBtnInterserver, ButtonBtnWorld, ButtonBtnGuild, ButtonBtnTeam, ButtonBtnFriend, ButtonBtnSecret, ButtonSend;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (MessInput.isFocused) MessInput.Select();
        }
    }
    public void SendMessRoomBtn()
    {
        if (MessInput.text != "")
        {
            ChatServer.SendChat(ChatCode.Room, MessInput.text);
            MessInput.text = "";
        }
    }
    public void SendMessGlobalBtn()
    {
        if (MessInput.text != "")
        {
            ChatServer.SendChat(ChatCode.Global, MessInput.text);
            MessInput.text = "";
        }
    }

    public  void BtnSystem()
    {
        ButtonSend.onClick.RemoveAllListeners();
        ButtonSend.onClick.AddListener(delegate { SendMessGlobalBtn(); });
    }
    public  void BtnInterserver()
    {
        Debug.Log("inter-server");
    }
    public  void BtnWorld()
    {
        ButtonSend.onClick.RemoveAllListeners();
        ButtonSend.onClick.AddListener(delegate { SendMessRoomBtn(); });
    }
    public  void BtnGuild()
    {
        Debug.Log("Guild");
    }
    public  void BtnTeam()
    {
        Debug.Log("Team");
    }
    public  void BtnFriend()
    {
        Debug.Log("friend");
    }
    public  void BtnSecret()
    {
        Debug.Log("secret");
    }
    public  void BtnSend()
    {
        Debug.Log("Send");
    }
}
