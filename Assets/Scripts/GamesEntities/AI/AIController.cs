using UnityEngine.AI;

public class AIController : AbstractEntity
{
    private NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = MaxSpeedMove;
    }
}
