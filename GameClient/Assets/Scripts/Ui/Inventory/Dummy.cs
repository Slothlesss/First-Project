using UnityEngine;
using Assets.HeroEditor4D.Common.CharacterScripts;

public class Dummy : MainBehaviour
{
    public static Dummy instance;
    [SerializeField] private Character4D dumy;
    private void Start()
    {
        instance = this;
        Invoke(nameof(Dumy), 0);
    }

    protected override void Awake() => base.Awake();
    
    public void Dumy()
    {
        var PlayerList = GamePlayManager.Instance.Playerlist;
        foreach (var p in PlayerList)
        {
            dumy.FromJson(GameplaySaveData.Instance.CharData, silent: false);
        }
    }
}
