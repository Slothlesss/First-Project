using System.Collections;
using ExitGames.Client.Photon;
using GameClient.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class LoginController : MonoBehaviour
{
    public InputField usernameInput, passwordInput;
    public static LoginController Intance;

    // Start is called before the first frame update
    private void Awake()
    {
        Intance = this;
    }
  
    IEnumerator IeLogin;
    public void LoginBtn()
    {
        if (IeLogin != null) return;
        IeLogin = IeWaitConnect(usernameInput.text,passwordInput.text);
        StartCoroutine(IeLogin);
    }

    IEnumerator IeWaitConnect(string username, string password)                                         // Đăng nhập.
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
            PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.Login, data, true);
        }
       
        IeLogin = null;
    }
}
