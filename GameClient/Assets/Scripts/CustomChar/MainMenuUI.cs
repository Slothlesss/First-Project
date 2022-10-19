using UnityEngine;

public class MainMenuUI : MainBehaviour
{
    [field: SerializeField] public SelectServerUI SelectServerUI { get; private set; }
    [field: SerializeField] public SelectCharUI SelectCharUI { get; private set; }
    [field: SerializeField] public CustomCharUI CustomCharUI { get; private set; }
    public AudioClip CloseSound;
    public AudioClip Btn_Click;
    public AudioClip UseSound;
    public AudioSource AudioSource;
    static readonly float SfxVolume = 0.5f;
    protected override void AddComponent()
    {
        LoadSelectSeverUI();
        LoadSelectCharUI();
        LoadCustomCharUI();
    }
    protected void LoadSelectSeverUI()
    {
        if (this.SelectServerUI != null) return;
        this.SelectServerUI = transform.Find("SelectServerUI").GetComponent<SelectServerUI>();
    }
    protected void LoadSelectCharUI()
    {
        if (this.SelectCharUI != null) return;
        this.SelectCharUI = transform.Find("SelectCharUI").GetComponent<SelectCharUI>();
    }
    
    protected void LoadCustomCharUI()
    {
        if (this.CustomCharUI != null) return;
        this.CustomCharUI = transform.Find("CustomCharUI").GetComponent<CustomCharUI>();
    }
    public void btn_Click() => AudioSource.PlayOneShot(Btn_Click, SfxVolume);
    public void close_Click() => AudioSource.PlayOneShot(CloseSound, SfxVolume);
}
