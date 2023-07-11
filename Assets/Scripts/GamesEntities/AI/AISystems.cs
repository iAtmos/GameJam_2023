using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISystems : MonoBehaviour
{
    [Header("AI systems controller")]
    protected static NavMeshAgent agent;
    private List<Transform> pointsCheckingAI = new List<Transform>();
    [field: SerializeField] private List<Transform> PointsCheckingAI = new List<Transform>();

    [Header("AI systems component")]
    private AIFieldOfView AIFieldOfView;
    private AIAttack AIAttack;

    //[Header("AI behavioral model")]
    protected enum AIStatus { idle, patrol, chase, searchTarget }
    protected static AIStatus AIStatusCurent { get; set; }

    //[Header("Target attacking")]
    protected static Transform purposePersecution { get; set; }
    protected static bool canShot { get; set; }

    [Header("AI animations")]
    private Animator animator;
    private const string SpeedWalk = nameof(SpeedWalk);

    [Header("AI interval of the behavioral model")]
    private int duration—ondition;
    private float timer—ondition;
    private enum Time—ondition { idle, searchTarget }

    protected void Start()
    {
        AIFieldOfView = gameObject.GetComponent<AIFieldOfView>();
        AIAttack = gameObject.GetComponent<AIAttack>();

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        foreach (Transform t in PointsCheckingAI)
            pointsCheckingAI.Add(t);
    }

    private void Update()
    {
        AIFieldOfView.CheckingFieldView();
        StatusLogic();
        AIAttack.—heckingAttackCondition();
    }

    private void StatusLogic()
    {
        if (AIStatusCurent == AIStatus.idle)
        {
            timer—ondition += Time.deltaTime;

            if (timer—ondition > duration—ondition)
            {
                timer—ondition = 0f;
                DurationEvent();
                AIStatusCurent = AIStatus.patrol;
                var pos = Random.Range(0, pointsCheckingAI.Count);
                var path = new NavMeshPath();
                agent.CalculatePath(pointsCheckingAI[pos].position, path);
                agent.SetPath(path);
                animator.SetFloat(SpeedWalk, 1);
            }
        }

        if (AIStatusCurent == AIStatus.patrol && agent.remainingDistance - agent.stoppingDistance < 1e-8)
        {
            AIStatusCurent = AIStatus.idle;
            animator.SetFloat(SpeedWalk, 0);
        }

        if (AIStatusCurent == AIStatus.chase)
        {
            if (purposePersecution == null)
            {
                DurationEvent(Time—ondition.searchTarget);

                var lastPlaceWhereEnemyWasVisible = transform.position + 3 * transform.forward;
                agent.SetDestination(lastPlaceWhereEnemyWasVisible);
                AIStatusCurent = AIStatus.searchTarget;
            }
            else
            {
                animator.SetFloat(SpeedWalk, 1);
                agent.SetDestination(purposePersecution.position);
            }
        }

        if (AIStatusCurent == AIStatus.searchTarget && agent.remainingDistance - agent.stoppingDistance < 1e-8)
        {
            timer—ondition = 0f;
            AIStatusCurent = AIStatus.idle;
            animator.SetFloat(SpeedWalk, 0);
        }
    }

    private void DurationEvent(Time—ondition name = Time—ondition.idle)
    {
        if (name == Time—ondition.idle)
        {
            duration—ondition = Random.Range(2, 8);
        }
        if (name == Time—ondition.searchTarget)
        {
            duration—ondition = Random.Range(6, 14);
        }
    }
}