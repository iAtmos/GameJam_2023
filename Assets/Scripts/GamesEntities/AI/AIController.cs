using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIController : AbstractEntity
{
    [Header("AI system controller")]
    private NavMeshAgent agent;
    protected Animator animator;

    [Header("Settings AI")]
    private const string SpeedWalk = nameof(SpeedWalk);
    [field: SerializeField] private int angularSpeedAI;
    [field: SerializeField] private int accelerationAI;
    private Spawner _spawnEnemy;

    protected override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.angularSpeed = angularSpeedAI;
        agent.speed = MaxSpeedMove;
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
            Invoke("DestroyAI", 0.15f);
            _spawnEnemy.DeathEntity(gameObject);
            Destroy(gameObject);
        };
    }

    public void DestroyAI()
    {
        Destroy(gameObject);
    }
}
