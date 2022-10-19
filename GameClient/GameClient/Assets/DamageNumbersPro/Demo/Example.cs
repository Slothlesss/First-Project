using UnityEngine;
using DamageNumbersPro;
using DamageNumbersPro.Demo;

public class Example : MonoBehaviour
{

    //Assign prefab in inspector.
    public DamageNumber numberPrefab;

    float nextShotTime;

    void Start()
    {
        nextShotTime = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        if (DNP_InputHandler.GetLeftClick())
        {
            Shoot();
            nextShotTime = Time.time + 0.3f;
        }
        else if (DNP_InputHandler.GetRightHeld() && Time.time > nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + 0.06f;
        }
    }

    void Shoot()
    {
        Vector2 mousePosition = Vector2.zero;

#if ENABLE_INPUT_SYSTEM && DNP_NewInputSystem
            if (Mouse.current != null) {
                mousePosition = Mouse.current.position.ReadValue();
            }
#else
        mousePosition = Input.mousePosition;
#endif

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.down, 0.2f);

        //Create Damage Number:
        DamageNumber damageNumber = numberPrefab.Spawn(worldPosition, Random.Range(1, 100));

        if (hit.collider != null) damageNumber.SetFollowedTarget(hit.collider.transform);

    }
}



