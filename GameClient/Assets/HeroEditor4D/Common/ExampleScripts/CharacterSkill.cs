using System.Collections;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
    private static CharacterSkill instance;
    [SerializeField] protected Transform target;
    [SerializeField] protected GameObject skillPrefab;
    [Range(1, 8)]
    [SerializeField] protected int skillNumber;
    [SerializeField] protected float skillDelay = 0.1f;
    [SerializeField] protected float countDownTime = 1f;
    [SerializeField] protected bool canUseSkill = true;
    protected float currentCDTime;
    [SerializeField] protected Vector2 spawnPos;

    private void Awake()
    {
        skillNumber = 8;
        if (instance != null) return;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        target = Target.instance.MainTarget;
        if (Input.GetKeyDown(KeyCode.Space) && target != null) SpawnSkill();
    }
    public void SpawnSkill()
    {
        if (!canUseSkill) return;
        canUseSkill = false;
        Vector3 randomSpawnPos = new Vector3(Random.Range(spawnPos.x, -spawnPos.x), Random.Range(spawnPos.y + 0.7f, -spawnPos.y + 0.7f)); // Random vi tri spawn skill
        StartCoroutine(SpawnSkillDelay(randomSpawnPos));
        StartCoroutine(SkillCountDown());
    }

    IEnumerator SkillCountDown()
    {
        this.currentCDTime = countDownTime;
        while (!canUseSkill)
        {
            currentCDTime -= Time.deltaTime;
            if (currentCDTime <= 0)
            {
                canUseSkill = true;
                currentCDTime = this.countDownTime;
            }
            yield return new WaitForEndOfFrame();
        }


    }
    IEnumerator SpawnSkillDelay(Vector3 randomSpawnPos)
    {
        // Spawn skill with ( bezier )
        for (int i = 0; i < skillNumber; i++)
        {
            var skill = Instantiate(skillPrefab, transform.position + randomSpawnPos, Quaternion.Euler(0, 0, Random.Range(0, 380))).GetComponent<SkillMove>();
            if (skillNumber % 2 != 0 && i == 0) skill.SetIsOnly(true); // Set skill di thang 
            skill.SetTarget(target);
            yield return new WaitForSeconds(skillDelay);
        }
    }
}


