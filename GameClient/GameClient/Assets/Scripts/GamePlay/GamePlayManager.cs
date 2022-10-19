using GameClient.Constructor;
using GameClient.Enums;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    public List<Player> Playerlist = new List<Player>();                                          // Lưu vật phẩm,... user (dữ liệu ngươi dùng.)
    public Dictionary<int, GameObject> Playerdict = new Dictionary<int, GameObject>();            // Lưu hình dáng của user.
    public Dictionary<string, GameObject> Playerdict_name = new Dictionary<string, GameObject>(); // luu danh sanh palyer tren srollview
    public GameObject playerTemplate;
    public int Id;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void OnDestroy() => Instance = null;
    private void Start() => GetData();
    void GetData()
    {
        var data = new Dictionary<byte, object>();
        data[1] = GameplayCode.GetData;
        PhotonServer.PhotonPeer.OpCustom((byte)RequestCode.GamePlay, data, true);
    }
}
