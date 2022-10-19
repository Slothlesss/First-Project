using System.Collections.Generic;
using UnityEngine;

    public class RegisterReponse
    {

    /*SerializeField] public GameObject GO_thongbao;*/
    public static void Response(Dictionary<byte, object> dt)
        {
            bool RegisterStatus = (bool)dt[1];
        if (RegisterStatus) Debug.Log("Client: Đăng ký thành công !");
        else
        {
            Debug.Log("Client: Đăng ký thất bại !");
        }
        }
    }


//public void thongbao()
//{
//    GameObject new_thongbao = Instantiate(GO_thongbao);
//    new_thongbao.GetComponent<PopupNotification>().set_thong_bao("thong bao moi");
//}

