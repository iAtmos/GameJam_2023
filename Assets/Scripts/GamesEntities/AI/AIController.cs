using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIController : AbstractEntity
{
    [Header("AI system controller")]
    public NavMeshAgent agent;
    protected Animator animator;

    [Header("Settings AI")]
    private const string SpeedWalk = nameof(SpeedWalk);
    [field: SerializeField] private int angularSpeedAI;
    [field: SerializeField] private int accelerationAI;
    public GameObject _spawnEnemy;

    public static float MaxHPAI = 100f;
    public static float MaxSpeedAI = 2f;

    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        base.Start();
        MaxHP = MaxHPAI;
        CurrentHP = MaxHPAI;
        agent.speed = MaxSpeedAI;

        agent.angularSpeed = angularSpeedAI;
        agent.acceleration = accelerationAI;
    }

    private void LateUpdate()
    {
        var speedAnimation = ConversionRange(agent.velocity.magnitude, MaxSpeedMove);
        animator.SetFloat(SpeedWalk, speedAnimation);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("NPC damage recived");
        CurrentHP -= damage;
        Debug.Log(CheckLiveEyntity());
        if (!CheckLiveEyntity())
        {
            GameObject.Find("Player").GetComponent<PlayerController>().exp++;
            GameObject.Find("Player").GetComponent<PlayerController>().score +=
                GameObject.Find("Player").GetComponent<PlayerController>().exp * 10;
            GameObject.Find("Player").GetComponent<PlayerController>().KillsUI.text =
                GameObject.Find("Player").GetComponent<PlayerController>().exp.ToString();
            GameObject.Find("Player").GetComponent<PlayerController>().ScrollbarEXP.size += 0.1f;
            GameObject.Find("Player").GetComponent<PlayerController>().ScoreUI.text =
                GameObject.Find("Player").GetComponent<PlayerController>().score.ToString();
            GameObject.Find("Player").GetComponent<PlayerController>().CheckLvlUp();
            GameObject.Find("Player").GetComponent<PlayerController>().SetUI();
            _spawnEnemy.GetComponent<Spawner>().DeathEntity();
            Destroy(gameObject);
        }
    }
    
    public void DestroyAI()
    {
        Destroy(gameObject);
    }
}
