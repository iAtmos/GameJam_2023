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
}
