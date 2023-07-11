using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISystems : MonoBehaviour
{
    [Header("AI system controller")]
    protected NavMeshAgent agent;
    protected Animator animator;
    protected enum AIStatus { idle, patrol, chase, searchTarget }
    protected AIStatus AIStatusCurent { get; set; }

    [Header("AI system component")]
    [field: SerializeField] private const string AISystem = nameof(AISystem);
    private AIFieldOfView fieldOfView;
    
    private AIAttack attack;
    protected static Transform purposePersecution { get; set; }
    protected static bool canShot { get; set; }


    [Header("AI system components settings")]
    private List<Transform> pointsCheckingAI = new List<Transform>();
    private const string SpeedWalk = nameof(SpeedWalk);

    [Header("AI interval of the behavioral model")]
    private float duration—ondition;
    private float time—ondition;
    private enum Interval—ondition { TimeInterval—onditionStart = 2, TimeInterval—onditionEnd = 8 }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        fieldOfView = GetComponent<AIFieldOfView>();
        attack = GetComponent<AIAttack>();

        AIStatusCurent = AIStatus.idle;


        var pointsAI = GameObject.FindGameObjectWithTag(AISystem).transform.GetChild(0);
        foreach (Transform t in pointsAI)
            pointsCheckingAI.Add(t);
    }

    private void Update()
    {
        fieldOfView.CheckingFieldView();
        AIBehavioralModel();
        attack.—heckingAttackCondition();
    }

    private void AIBehavioralModel() 
    {
        if (AIStatusCurent == AIStatus.idle)
        {
            time—ondition += Time.deltaTime;

            if (time—ondition > duration—ondition)
            {
                time—ondition = 0f;
                DurationCondition();

                AIStatusCurent = AIStatus.patrol;
                var posAIChecking = Random.Range(0, pointsCheckingAI.Count);
                var pathToPoint = new NavMeshPath();
                agent.CalculatePath(pointsCheckingAI[posAIChecking].position, pathToPoint);
                agent.SetPath(pathToPoint);

                animator.SetFloat(SpeedWalk, 1f);
            }
        }

        if (AIStatusCurent == AIStatus.patrol && agent.remainingDistance - agent.stoppingDistance < 1e-8)
        {
            AIStatusCurent = AIStatus.idle;
            animator.SetFloat(SpeedWalk, 0f);
        }

        if (AIStatusCurent == AIStatus.chase)
        {
            if (purposePersecution == null)
            {
                DurationCondition();

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
    }

    private void DurationCondition()
    {
        duration—ondition = Random.Range(2f, 8f);
    }
}