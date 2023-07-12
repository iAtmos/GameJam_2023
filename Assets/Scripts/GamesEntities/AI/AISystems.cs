using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISystems : MonoBehaviour
{
    [Header("AI system controller")]
    protected NavMeshAgent agent;
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

    [Header("AI interval of the behavioral model")]
    private float duration—ondition = 0f;
    private float time—ondition = 0f;

    [Header("AI adjustable settings")]
    private float stoppingDistanceBeforeGoal = 2f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        fieldOfView = GetComponent<AIFieldOfView>();
        attack = GetComponent<AIAttack>();

        var pointsAI = GameObject.FindGameObjectWithTag(AISystem).transform.GetChild(0);
        foreach (Transform t in pointsAI)
            pointsCheckingAI.Add(t);
    }

    private void Start()
    {
        DurationCondition();
    }

    private void Update()
    {
        fieldOfView.CheckingFieldView();
        AIBehavioralModel();
        attack.—heckingAttackCondition();
    }

    private void AIBehavioralModel() 
    {
        if (purposePersecution != null)
            AIStatusCurent = AIStatus.chase;

        if (AIStatusCurent == AIStatus.chase)
        {
            if (purposePersecution == null)
            {
                DurationCondition();
                agent.stoppingDistance = 0f;

                var lastPlaceWhereEnemyWasVisible = transform.position + 3 * transform.forward;
                agent.SetDestination(lastPlaceWhereEnemyWasVisible);

                time—ondition = 0f;
                DurationCondition(4, 10);
                AIStatusCurent = AIStatus.idle;
            }
            else
            {
                agent.stoppingDistance = stoppingDistanceBeforeGoal;
                agent.SetDestination(purposePersecution.position);
            }
        }

        if (AIStatusCurent == AIStatus.idle)
        {
            time—ondition += Time.deltaTime;

            if (duration—ondition <= time—ondition)
            {
                AIStatusCurent = AIStatus.patrol;
                var posAIChecking = Random.Range(0, pointsCheckingAI.Count);
                var pathToPoint = new NavMeshPath();
                agent.CalculatePath(pointsCheckingAI[posAIChecking].position, pathToPoint);
                agent.SetPath(pathToPoint);

                time—ondition = 0f;
                DurationCondition();
            }
        }

        if (AIStatusCurent == AIStatus.patrol && agent.remainingDistance - agent.stoppingDistance < 1e-8)
        {
            AIStatusCurent = AIStatus.idle;
        }
    }

    private void DurationCondition(float leftBorderTime = 2f, float rightBorderTime = 8f)
    {
        duration—ondition = Random.Range(leftBorderTime, rightBorderTime);
    }
}