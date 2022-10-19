using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButtonBase : MonoBehaviour, IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    protected Vector3 startMousePos;
    protected Vector3 endMousePos;
    protected SkillUI skillUI;

    private void Awake() => this.skillUI = transform.parent.GetComponent<SkillUI>();
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //Player.Instance.SpawnSkill();
    }

    public virtual void OnPointerDown(PointerEventData eventData) => startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distance = endMousePos - startMousePos;
        if (distance.x > 0.5f && distance.y > 0.5f)
        {
            skillUI.SwitchUp();
            return;
        }
        if (distance.x < -0.5f && distance.y < -0.5f)
        {
            skillUI.SwitchDown();
            return;
        }
    }
}
