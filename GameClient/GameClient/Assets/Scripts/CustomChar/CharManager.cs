using UnityEngine;
using Assets.HeroEditor4D.Common.CharacterScripts;

public class CharManager : MainBehaviour
{
    public static CharManager instance;
    [SerializeField] private Character4D character;
    private void Start() => instance = this;
    protected override void Awake() => base.Awake();
    public void Create_player(string CharData) => character.FromJson(CharData, silent: false);
}
