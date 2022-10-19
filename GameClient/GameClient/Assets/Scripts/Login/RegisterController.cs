using System.Collections;
using ExitGames.Client.Photon;
using GameClient.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    public InputField usernameInput, passwordInput, password2Input;
    public static RegisterController Intance;

    // Start is called before the first frame update
    private void Awake()
    {
        Intance = this;
        if (PhotonServer.PhotonPeer != null && PhotonServer.PhotonPeer.PeerState == PeerStateValue.Connected)
        {
            PopupManager.instance.GetPopup("Lobby");
            gameObject.SetActive(false);
        }
    }
    IEnumerator IeLogin;
    public void RegisterBtn()
    {
        if (IeLogin != null) return;
        IeLogin = IeWaitRegister(usernameInput.text, passwordInput.text);
        StartCoroutine(IeLogin);
    }

    IEnumerator IeWaitRegister(string username, string password)                                         // Đăng ký.
    {
        float waitTime = 1f;
        while (PhotonServer.Instance.isConnect)
        {
            yield return new WaitForEndOfFrame();
            waitTime -= Time.deltaTime;
            if (waitTime <= 0) break;
        }
        if (PhotonServer.Instance.isConnect)
        {
            var data = new Dictionary<byte, object>();
            data[1] = username;
            data[2] = password;
            PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Register, data, true);
        }
        IeLogin = null;
    }
}
