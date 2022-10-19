using GameClient.Constructor;
using Newtonsoft.Json;
using System.Collections.Generic;

public class LoginResponse
{
    public static void Response(Dictionary<byte, object> dt)
    {
        if ((bool)dt[1]) return;
        else
        {
            LoginController.Intance.gameObject.SetActive(false);
            PopupManager.instance.GetPopup("SelectServerUI");
            UserManager.userData = JsonConvert.DeserializeObject<UserData>((string)dt[2]);
            //Debug.Log("Client: Đăng nhập thành công !");
        }
    }
}
