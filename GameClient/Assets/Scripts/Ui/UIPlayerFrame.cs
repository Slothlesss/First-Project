using UnityEngine;
using Assets.HeroEditor4D.Common.CharacterScripts;
using TMPro;

public class UIPlayerFrame : MainBehaviour
{
    public static UIPlayerFrame instance; 
    [SerializeField] private Character4D Imgcharacter;
    [SerializeField] private TMP_Text CharacterLv;
    [SerializeField] private TMP_Text CharacterName;
    private void Start()
    {
        instance = this;
        CharacterLv.text = "Lv." ;
        CharacterName.text = "player" ;
    }

    protected override void Awake() => Imgcharacter.FromJson(GameplaySaveData.Instance.CharData, silent: false);
    
    public void Addchar() 
    {
        var PlayerList = GamePlayManager.Instance.Playerlist;
        foreach (var p in PlayerList)
        {
            CharacterLv.text = "Lv." + p.Lerver;
            CharacterName.text = "" + p.UserID;
        }
    }
}