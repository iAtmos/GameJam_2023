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
    public GameObject _spawnEnemy;

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
            GameObject.Find("Player").GetComponent<PlayerController>().CheckLvlUp();
            GameObject.Find("Player").GetComponent<PlayerController>().SetUI();
            _spawnEnemy.GetComponent<Spawner>().DeathEntity();
            Destroy(gameObject);
        };
    }
    
    public void DestroyAI()
    {
        Destroy(gameObject);
    }
}
