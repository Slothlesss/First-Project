using UnityEngine;
using Assets.HeroEditor4D.Common.CharacterScripts;
using UnityEngine.UI;
using Assets.HeroEditor4D.Common.EditorScripts;

public class CharacterSlot : MainBehaviour
{
    [field: SerializeField] public int SlotNumber { get; private set; }
    [field: SerializeField] public Character4D Char { get; private set; }
    [field: SerializeField] public Button CreateBtn { get; private set; }
    [field: SerializeField] public Button SelectBtn { get; private set; }
    protected override void AddComponent()
    {
        LoadSlotNumber();
        LoadCreateBtn();
        LoadCharacterBtn();
        LoadSelectBtn();
    }
    protected void LoadSlotNumber()
    {
        var name = transform.gameObject.name.Replace(this.GetType().Name, "");
        this.SlotNumber = int.Parse(name) ;
    }
    protected void LoadCreateBtn()
    {
        if (this.CreateBtn != null) return;
        this.CreateBtn = transform.Find("CreateBtn").GetComponent<Button>();
    }
    protected void LoadCharacterBtn()
    {
        if (this.Char != null) return;
        this.Char = transform.Find("Char").GetComponent<Character4D>();
    }

    protected void LoadSelectBtn()
    {
        if (this.SelectBtn != null) return;
        this.SelectBtn = transform.Find("SelectBtn").GetComponent<Button>();
    }
    
    protected override void Awake()
    {
        /// <summary>
        /// Gán chức năng cho nút CreateBtn và SelectBtn của slot hiện tại
        /// </summary>
        /// <returns></returns>
        CreateBtn.onClick.AddListener(() => CreateChar());
        SelectBtn.onClick.AddListener(() => SelectChar());
    }
    public void LoadCharSaveData(int roomId)
    {
        var chardata = CharacterSaveData.Instance.room.Find(i => i.roomId == roomId); // Lấy các dữ liệu nhân vật của roomId 
        if (chardata.charDataList.Count <= this.SlotNumber) // Kiểm tra số lượng data của nhân vật đang có trong roomId <= slotNumber của class này
        {
            NotExistCharacter();
        }
        else
        {
            if (chardata.charDataList[this.SlotNumber].Length == 0) return; // Kiểm tra slot hiện tại có dữ liệu ko? (nếu ko thì return)
            /*
             * Nếu có thì bật slot trên UI và gán dữ liệu nhân vật cho slot đó
             */
            ExistCharacter();
            this.Char.FromJson(chardata.charDataList[this.SlotNumber], silent: false);
        }
    }
    // Tắt slot trên UI khi ko sỡ hữu nhân vật
    protected void NotExistCharacter()
    {
        this.Char.gameObject.SetActive(false);
        this.SelectBtn.gameObject.SetActive(false);
        this.CreateBtn.gameObject.SetActive(true);
    }
    // Bật slot trên UI khi đang sỡ hữu nhân vật
    protected void ExistCharacter()
    {
        this.Char.gameObject.SetActive(true);
        this.SelectBtn.gameObject.SetActive(true);
        this.CreateBtn.gameObject.SetActive(false);
    }
    // Tạo mới nhân vật
    protected void CreateChar()
    {
        var selectCharUI = transform.parent.parent.GetComponent<SelectCharUI>();
        selectCharUI.GetMainMenuUI().CustomCharUI.SlotToSave = this.SlotNumber;
        selectCharUI.GetMainMenuUI().CustomCharUI.gameObject.SetActive(true);
        selectCharUI.GetMainMenuUI().CustomCharUI.gameObject.GetComponent<CharacterEditor>().Reset();
        selectCharUI.gameObject.SetActive(false);
    }
    // Chọn nhân vật đang có
    protected void SelectChar()
    {
        var selectCharUI = transform.parent.parent.GetComponent<SelectCharUI>();
        selectCharUI.InSelectChar(SlotNumber);
    }
}
