using UnityEngine;
using Assets.HeroEditor4D.Common.CharacterScripts;
using TMPro;

public class UITargetFrame : MainBehaviour
{
    public static UITargetFrame instance;
    [SerializeField] private Character4D Dumy;
    [SerializeField] public GameObject targetUI;
    [SerializeField] private TMP_Text CharacterLv;
    [SerializeField] private TMP_Text CharacterName;
    int IDtarget;
    private void Start() => instance = this;

    private void Update() => Invoke(nameof(InforTarget), 0);
    public void InforTarget()
    {
        var PlayerList = GamePlayManager.Instance.Playerlist;
        foreach (var p in PlayerList)
        {
            if (p.UserID == Target.instance.FindIDtarget && Target.instance.MainTarget != null)
            {
                Dumy.FromJson(p.JsonChar, silent: false);
                CharacterName.text = "" + Target.instance.MainTarget.name;
                CharacterLv.text = "Lv.";
            }
        }
    }
}