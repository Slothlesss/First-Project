using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkillMove : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField] protected Transform target;
    [SerializeField] protected float rotationSpeed = 1500f;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected bool isOnly = false;
    protected float tParam = 0;

    protected Vector2 p0, p1, p2, p3;

    // Gan transform cho target
    public void SetTarget(Transform target) => this.target = target;

    // Gan gia tri cho IsOnly
    public void SetIsOnly(bool value) => this.isOnly = value;

    private void Awake() => rb = GetComponent<Rigidbody2D>();
    private void Start() => StartCoroutine(GoByTheRoute());
    // Update is called once per frame
    void Update() => p3 = target.transform.position;

    IEnumerator GoByTheRoute()
    {
        SetRoutePos();
        
        while (tParam < 1) // Dieu kien cua thuat toan (0 <= tParam <= 1)
        {
            tParam += Time.deltaTime * speed;

            // Thuat toan Bezier
            Vector2 skillPos = Mathf.Pow(1 - tParam, 3) * p0 + 
                               3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                               3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + 
                               Mathf.Pow(tParam, 3) * p3;

            // Di chuyen skill theo duong Bezier
            transform.position = skillPos;

            RotateSkill(skillPos);

            yield return new WaitForEndOfFrame();        
        }
    }

    // Set vi tri route
    protected void SetRoutePos()
    {
        p0 = this.transform.position;
        if (isOnly)
        {
            var a = (target.transform.position.x + this.transform.position.x) / 2;
            var b = (target.transform.position.y + this.transform.position.y) / 2;
            p1 = new Vector2(a, b);
            p2 = new Vector2(a, b);
            return;
        }
        p1 = (Vector2)this.transform.localPosition + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        p2 = (Vector2)target.transform.position + new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    // Xoay doi tuong skill theo route
    protected void RotateSkill(Vector2 skillPos)
    {
        Vector2 direction = skillPos - rb.position;
        direction.Normalize();
        float angle = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotationSpeed * angle;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform == target) Destroy(this.gameObject);
    }
}
