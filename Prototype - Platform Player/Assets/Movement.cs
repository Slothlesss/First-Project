using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    float dir;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask layerMask;
    Collider2D col;

    TrailRenderer trailRenderer;


    [Header("Bashing")]
    [SerializeField] float radius;
    [SerializeField] GameObject bashAbleObj;
    bool nearToBashAbleObj;
    bool isChoosingDir;
    bool isBashing;
    [SerializeField] float bashPower;
    [SerializeField] float bashTime;
    [SerializeField] GameObject arrow;
    Vector3 bashDir;
    float bashTimeReset;

    [Header("Dashing")]
    [SerializeField] float dashingVelocity;
    [SerializeField] float dashingTime;
    Vector2 dashingDir;
    bool isDashing;
    bool canDash = true;
    float originalGravity;


    void Start()
    {
        bashTimeReset = bashTime;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        originalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        dir = Input.GetAxisRaw("Horizontal") * speed;
        var dashInput = Input.GetButtonDown("Dash");


        if(dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            rb.gravityScale = 0;
            dashingDir = new Vector2(dir, Input.GetAxisRaw("Vertical"));
            
            if(dashingDir == Vector2.zero) //fix
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());

        }
        if(isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        if(isGrounded())
        {
            canDash = true;
        }



        if(dir > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        Jump();
        Bash();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (isBashing == false) rb.velocity = new Vector2(dir * Time.fixedDeltaTime, rb.velocity.y);
    }
    

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;
        rb.gravityScale = originalGravity;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    bool isGrounded()
    {
        float extraHeight = 0.03f;
        RaycastHit2D hitInfo = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, extraHeight, layerMask);
        return hitInfo.collider != null;
    }


    void Bash()
    {
        RaycastHit2D[] rays = Physics2D.CircleCastAll(transform.position, radius, Vector3.forward); 
        foreach(RaycastHit2D ray in rays)
        {
            nearToBashAbleObj = false;
            if(ray.collider.tag == "BashAble")
            {
                nearToBashAbleObj = true;
                bashAbleObj = ray.collider.transform.gameObject;
                break;
            }
        }

        if(nearToBashAbleObj)
        {
            bashAbleObj.GetComponent<SpriteRenderer>().color = Color.red;
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                Time.timeScale = 0;
                arrow.SetActive(true);
                arrow.transform.position = bashAbleObj.transform.transform.position;
                isChoosingDir = true;
            }
            else if(isChoosingDir && Input.GetKeyUp(KeyCode.Mouse1))
            {
                Time.timeScale = 1f;
                isChoosingDir = false;
                isBashing = true;
                rb.velocity = Vector2.zero;
                transform.position = bashAbleObj.transform.position;
                bashDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                bashDir.z = 0;
                if(bashDir.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);

                }
                bashDir = bashDir.normalized;
                bashAbleObj.GetComponent<Rigidbody2D>().AddForce(-bashDir * 50, ForceMode2D.Impulse);
                arrow.SetActive(false);
            }
        }
        else if(bashAbleObj != null)
        {
            bashAbleObj.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //Preform Bashing
        if(isBashing)
        {
            if(bashTime > 0)
            {
                bashTime -= Time.deltaTime;
                rb.velocity = bashDir * bashPower * Time.deltaTime;
            }
            else
            {
                isBashing = false;
                bashTime = bashTimeReset;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
