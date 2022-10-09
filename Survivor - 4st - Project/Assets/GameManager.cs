using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    int gold;
    int exp;
    int level;
    public float timer;
    bool gameOver = false;


    [SerializeField] Transform MonsterPool;

    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI levelText;


    [SerializeField] TextMeshProUGUI timerText;


    [SerializeField] GameObject gameOverMenu;


    List<Monster> activeMonster = new List<Monster>();
    List<Item> activeItem = new List<Item>();
    

    public int Gold 
    { 
        get 
        { 
            return gold; 
        }  
        set
        {
            this.gold = value;
            this.goldText.text = value.ToString(); 
        }
    }



    public int Exp 
    { 
        get 
        { 
            return exp; 
        }
        set
        {
            this.exp = value;
        }
    }

    int[] maxExp;

    WaveEvent[] waveEvents;
    int wave = 0;


    public int MaxExp
    {
        get
        {
            if(maxExp.Length > Level - 1)
            {
                expSlider.maxValue = maxExp[Level - 1]; 
                return maxExp[Level - 1];
            }
            return 0;
        }
    }

    public int Level
    {
        get 
        {
            return level;
        }
        set
        {
            this.level = value;
            this.levelText.text = value.ToString();
        }
    }

    public float Timer
    {
        get
        {
            return timer;
        }
        set
        {
            this.timer = value;
            int minute = Mathf.FloorToInt(value / 60);
            int second = Mathf.FloorToInt(value % 60);
            string minuteText = minute < 10 ? "0" + minute.ToString() : minute.ToString();
            string secondText = second < 10 ? "0" + second.ToString() : second.ToString();
            this.timerText.text = minuteText + ":" + secondText;
        }
    }

    public ObjectPool Pool { get; set; }
    public bool IsCreating;

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        maxExp = new int[] { 20, 30, 50, 60, 80, 100, 150, 160, 190, 200 };
        waveEvents = new WaveEvent[]
        {
            new WaveEvent(1,"FirstMonster",10,1.0f,100),
            new WaveEvent(60,"FirstMonster",15,1.0f,100),
            new WaveEvent(60,"FirstMonster",15,1.5f,120),
            new WaveEvent(120,"FirstMonster",25,0.5f,120),
            new WaveEvent(120,"FirstMonster",25,0.5f,120),
            new WaveEvent(180,"FirstMonster",40,0.25f,200),
        };
        
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        
            CreateWaves();
    }
    public void GetGold()
    {
        Gold += Mathf.CeilToInt(Mathf.Sqrt(Level * 2));
    }
    public void GetExp()
    {
        Exp += Mathf.CeilToInt(Mathf.Sqrt(Level * 3));
        if (Exp > MaxExp)
        {
            Level++;
        }
    }


    public void CreateWaves()
    {
        for(int i = waveEvents.Length - 1; i >=0 ; i--)
        {
            if(Mathf.FloorToInt(Timer) > waveEvents[i].PresentTime)
            {
                wave = i;
                break;
            }
        }
        if (IsCreating == false)
        {
            StartCoroutine(SpawnWave());
        }
    }
    IEnumerator SpawnWave()
    {
        IsCreating = true;
        for (int i = 0; i < waveEvents[wave].NumberMonster; i++)
        {

            if (activeMonster.Count <= waveEvents[wave].MaxMonster)
            {
                Vector3 randomPos = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
                Monster monster = Pool.GetObject(waveEvents[wave].MonsterType).GetComponent<Monster>();
                if (Mathf.CeilToInt(Timer) % 20 == 0)
                {
                    monster.IncreaseStrength(10, 10);
                }
                monster.Spawn(MonsterPool, randomPos);
                activeMonster.Add(monster);
            }
        }
        yield return new WaitForSeconds(waveEvents[wave].CreateTime);
        IsCreating = false;

    }
    public void RemoveMonster(Monster monster)
    {
        activeMonster.Remove(monster);
    }

    public void CreateItems()
    {
        if (Mathf.CeilToInt(Timer) % 10 == 0)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnItems()
    {
        for (int i = 0; i < 5; i++)
        {
            int itemIndex = 0; // Random.Range(0, 4);

            string type = string.Empty;
            switch (itemIndex)
            {
                case 0:
                    type = "BoxObject";
                    break;
                case 1:
                    type = "MagnetObject";
                    break;
            }

            Item item = Pool.GetObject(type).GetComponent<Item>();

            yield return new WaitForSeconds(1f);
        }
    }

}
