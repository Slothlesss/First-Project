using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    


    [SerializeField] int monsterMaxHealth;
    [SerializeField] float monsterSpeed;
    [SerializeField] int monsterDamage;

    Animator anim;
    public Vector3 destination;

    float monsterCurrentHealth;
    public bool IsActive { get; set; }

    private void Update()
    {
        Move();
        UpdateAnimation();
    }
    public void Spawn(Transform MonsterPool, Vector3 randomPos)
    {
        sr = GetComponent<SpriteRenderer>();
        destination = FindObjectOfType<PlayerController>().transform.position;
        anim = GetComponent<Animator>();
        randomPos.x += randomPos.x <= 0 ? -1f : 1f;
        randomPos.y += randomPos.y <= 0 ? -1f : 1f;
        transform.position = randomPos + destination;
        monsterCurrentHealth = monsterMaxHealth;
        transform.SetParent(MonsterPool);
        IsActive = true;
    }
    void Release()
    {
        IsActive = false;
        GameManager.Instance.Pool.ReleaseObject(gameObject);
    }
    private void Move()
    {
        if(IsActive)
        {
            destination = FindObjectOfType<PlayerController>().transform.position;
            transform.position = Vector2.MoveTowards(transform.position, destination, monsterSpeed * Time.deltaTime);
        }
    }
    public void IncreaseStrength(int health, int damage)
    {
        monsterMaxHealth += damage;
        monsterDamage += damage;
    }
    public void TakeDamage(float damage)
    {
        if (IsActive)
        {
            if (monsterCurrentHealth <= 0)
            {
                GameObject goldObject = GameManager.Instance.Pool.GetObject("goldObject");
                GameObject expObject = GameManager.Instance.Pool.GetObject("expObject");
                int num = Mathf.CeilToInt(Random.Range(0.1f, 1.9f));
                for(int i = 0; i <num; i++)
                {
                    Instantiate(goldObject, transform.position + new Vector3(Random.Range(-0.3f,0.3f), Random.Range(-0.3f, 0.3f)), Quaternion.identity);
                }
                Instantiate(expObject, transform.position, Quaternion.identity);
                Release();
            }
        }
    }

    void UpdateAnimation()
    {
        if (transform.position.y > destination.y + 0.3f ) //Move Down
        {
            anim.SetInteger("Horizontal", 0);
            anim.SetInteger("Vertical", 1);
        }
        else if (transform.position.y < destination.y - 0.3f) //Move up
        {
            anim.SetInteger("Horizontal", 0);
            anim.SetInteger("Vertical", -1);
        }

        else
        {
            if (transform.position.x > destination.x) //Move left
            {
                anim.SetInteger("Horizontal", 1);
                anim.SetInteger("Vertical", 0);
                sr.flipX = false;
            }
            else if (transform.position.x < destination.x) //Move right
            {
                anim.SetInteger("Horizontal", 1);
                anim.SetInteger("Vertical", 0);
                sr.flipX = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(monsterDamage);
        }
    }
}
