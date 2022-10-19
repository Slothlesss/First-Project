using ExitGames.Client.Photon;
using GameClient.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonServer : MonoBehaviour, IPhotonPeerListener
{
    public static PhotonServer Instance;
    public string serverName= "GameServer";
    public string host;
    public string tcpPort;
    public string udpPort;
    public ConnectionProtocol protocol;
    public static PhotonPeer PhotonPeer;
    public bool isConnect = false;
    Dictionary<string, GameObject> playDict = new Dictionary<string, GameObject>(); //dict lưu những player đang trong room.

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            PhotonPeer = new PhotonPeer(this, protocol);
            PhotonPeer.Connect(host + ":" + tcpPort, serverName);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (PhotonPeer != null) PhotonPeer.Service();
    }
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("Debug level " + level + " " + message);
    }

    public void OnEvent(EventData eventData)                             // Nhận dữ liệu từ server.         
    {                                                                    // Thông báo từ máy chủ.
        Debug.Log("Debug eventdata ");
        switch ((byte)eventData.Code)
        {
            case (byte)RequestCode.Login:
                LoginResponse.Response(eventData.Parameters);
                break;
            case (byte)RequestCode.Register:
                RegisterReponse.Response(eventData.Parameters);
                break;
            case (byte)RequestCode.Notification:
                PopupManager.instance.GetPopup("NotificationUI");
                PopupNotification.Notification((string)eventData.Parameters[1]);
                break;
            case (byte)RequestCode.LoadGamePlay:
                SceneLoader.instance.LoadScene(1);
                break;
            case (byte)RequestCode.GamePlay:
                GamePlay.Response(eventData.Parameters);
                break;
            case (byte)RequestCode.Room:
                RoomServer.Response(eventData.Parameters);
                break;
            case (byte)RequestCode.Chat:
                ChatServer.Response(eventData.Parameters);
                break;
            case (byte)RequestCode.GetDataJsonChar:
                CustomCharUI.Response(eventData.Parameters);
                break;
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)  // Nhận dữ liệu từ server.
    {                                                                     // Dữ liệu trả về từ máy chủ.
        Debug.Log("Debug OnOperationResponse ");
        //throw new System.NotImplementedException();
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connect status: Connect");
                isConnect = true;
                break;
            case StatusCode.Disconnect:
                SceneManager.LoadScene("MainGame");
                Debug.Log("Connect status: Disconnect");
                break;
            case StatusCode.DisconnectByServer:
                Debug.Log("Connect status: DisconnectByServer");
                break;
            case StatusCode.DisconnectByServerLogic:
                Debug.Log("Connect status: DisconnectByServerLogic");
                break;
            case StatusCode.DisconnectByServerUserLimit:
                Debug.Log("Connect status: DisconnectByServerUserLimit");
                break;
            case StatusCode.TimeoutDisconnect:
                Debug.Log("Connect status: TimeoutDisconnect");
                break;
        }
        if (statusCode != StatusCode.Connect) isConnect = false;
    }

}

