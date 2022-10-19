using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Target : MonoBehaviour
{
    [SerializeField] protected List<Transform> Targets = new List<Transform>();
    [SerializeField] protected float radius = 4f;
    [SerializeField] protected CircleCollider2D rangeCollider;
    public Transform MainTarget;
    public static Target instance;
    int MainTargetid = 0;
    public int FindIDtarget ;
    public GameObject TargetIcon;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        if (rangeCollider != null) return;
        rangeCollider = GetComponent<CircleCollider2D>();
    }
    void Start() => rangeCollider.radius = this.radius;
    private void Update()
    {
        rangeCollider.radius = this.radius;
        AddMainTarget();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Boss" ||
             col.tag == "Player" ||
             col.tag == "Npc")
        { 
            Targets.Add(col.transform);
            UITargetFrame.instance.targetUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Boss" || 
            col.tag == "Player" || 
            col.tag == "Npc")
        {
            Targets.Remove(col.transform);
            MainTarget = null;
            UITargetFrame.instance.targetUI.SetActive(false);
        }
    }
    public void AddMainTarget()
    {
        if (Targets.Count > 0 && MainTarget == null)
        {
            var AddMain = Targets[0];
            MainTarget = AddMain;
            FindIDtarget = MainTarget.GetComponent<CharacterID>().Characterid;
        }
    }

    public void NextMainTarget()
    {
        if (Targets.Count > 1 && MainTargetid < Targets.Count - 1)
        {
            MainTargetid++;
            var NextMain = Targets[MainTargetid];
            MainTarget = NextMain;
            FindIDtarget = MainTarget.GetComponent<CharacterID>().Characterid;
        }
        else
        {
            MainTargetid = 0;
            var NextMain = Targets[MainTargetid];
            MainTarget = NextMain;
            FindIDtarget = MainTarget.GetComponent<CharacterID>().Characterid;
        }
    }
}
