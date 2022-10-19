using UnityEngine;
using UnityEngine.UI;

public class SelectSVPnl : MainBehaviour
{
    private SelectServerUI selectServerUI;
    [SerializeField] private Text idServerTxt;
    [SerializeField] private Text nameServerTxt;
    [SerializeField] private Button enterGameBtn;
    protected void Start()
    {
        idServerTxt.text = CharacterSaveData.Instance.idServer.ToString();
        nameServerTxt.text = CharacterSaveData.Instance.nameServer.ToString();
    }
    protected override void Awake()
    {
        base.Awake();
        this.AddListenerEnterGame();
    }
    protected override void AddComponent()
    {
        this.LoadSelectServerUI();
        this.LoadIdServerTxt();
        this.LoadNameServerTxt();
        this.LoadEnterGameBtn();
    }
    private void LoadSelectServerUI()
    {
        if (this.selectServerUI != null) return;
        this.selectServerUI = transform.parent.GetComponent<SelectServerUI>();
    }
    private void LoadIdServerTxt()
    {
        if (this.idServerTxt != null) return;
        this.idServerTxt = transform.Find("IdServerText").GetComponent<Text>();
    }
    private void LoadNameServerTxt()
    {
        if (this.nameServerTxt != null) return;
        this.nameServerTxt = transform.Find("NameServerText").GetComponent<Text>();
    }
    private void LoadEnterGameBtn()
    {
        if(this.enterGameBtn != null) return;
        this.enterGameBtn = transform.Find("EnterGameBtn").GetComponent<Button>();
    }

    // Gán chức năng cho nút EnterGameBtn
    private void AddListenerEnterGame()
    {
        this.enterGameBtn.onClick.AddListener(() =>
        {
            // Vào room hiện tại
            this.selectServerUI.SelectCurrentRoom();
        });
    }
}
