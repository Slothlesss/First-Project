using UnityEngine;

public class PlayerFrame : MonoBehaviour
{
    string txtNamePlayer;
    string txtLerver;
    // Start is called before the first frame update
    void Start()
    {
        var PlayerList = GamePlayManager.Instance.Playerlist;
        var Playerdict = GamePlayManager.Instance.Playerdict;
    }

}
