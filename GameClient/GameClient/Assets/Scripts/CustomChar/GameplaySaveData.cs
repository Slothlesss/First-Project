using UnityEngine;

public class GameplaySaveData : MainBehaviour
{
    private static GameplaySaveData instance;
    public static GameplaySaveData Instance => instance;
    [field: SerializeField] public string CharData { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        if (instance != null) return;
        instance = this;
    }
    public void SetCharData(string json) => this.CharData = json;
}
