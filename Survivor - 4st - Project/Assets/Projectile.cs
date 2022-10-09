using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    Monster target;
    [SerializeField] float damage;
    [SerializeField] float projectileSpeed;
    [SerializeField] float existTimeProjectile;

    float counter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= existTimeProjectile)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
            counter = 0;
        }
        //MoveToTarget();
    }

    void MoveToTarget()
    {
        if (target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * projectileSpeed);

            //Rotate projectile
            Vector2 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else if (!target.IsActive)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
        else return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (target.gameObject == collision.gameObject)
            {
                target.TakeDamage(damage);
                GameManager.Instance.Pool.ReleaseObject(gameObject);
            }
        }
    }
}
