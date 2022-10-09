using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Character")]
    [SerializeField] float playerSpeed;
    [SerializeField] float playerHealth;
    [SerializeField] Slider healthSlider;

    [Header("Projectile")]
    [SerializeField] string projectileName;
    [SerializeField] float attackCoolDown;
    [SerializeField] Slider attackCoolDownSlider;


    float attackTimer;
    Vector2 movement;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public bool CanMove { get; set; }

    private void Start()
    {
        this.healthSlider.value = playerHealth;
        this.healthSlider.maxValue = playerHealth;
        this.attackCoolDownSlider.value = attackTimer;
        this.attackCoolDownSlider.maxValue = attackCoolDown;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {

        UpdateMovement();
        UpdateAnimation();
    }

    void UpdateMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement.normalized * playerSpeed * Time.fixedDeltaTime);
    }

    void UpdateAnimation()
    {
        if(movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            sr.flipX = movement.x < 0.01 ? true : false;
        }
        animator.SetFloat("Speed", movement.normalized.sqrMagnitude);
    }

    void Shoot()
    {
        attackTimer += Time.deltaTime; 
        this.attackCoolDownSlider.value = attackTimer;
        if (attackTimer > attackCoolDown)
        {
            Projectile projectile = GameManager.Instance.Pool.GetObject(projectileName).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            attackTimer = 0;
        }
    }
    public void TakeDamage(int damage)
    {
        healthSlider.value -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gold"))
        {
            GameManager.Instance.GetGold();
            GameManager.Instance.Pool.ReleaseObject(collision.gameObject);
        }
        if (collision.CompareTag("Exp"))
        {
            GameManager.Instance.GetExp();
            GameManager.Instance.Pool.ReleaseObject(collision.gameObject);
        }
    }
}
