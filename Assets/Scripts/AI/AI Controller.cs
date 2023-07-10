using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class AiController : AbstractEntity
{
    [Header("AI following")]
    public Transform target;
    public float minimumDistance;
    public float attackDistance;

    [Header("AI patrolling")]
    public Transform[] patrolPoints;
    public float waitTime;
    public float detectionDistance;
    int currentPointIndex;
    bool once;

    [Header("Attack")]
    public float _shootForce;
    public float _timeBetweenShooting;
    private float _nextShotTime;

    [Header("References")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private SpawnEnemy _spawnEnemy;


    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, target.position) <= detectionDistance)
        {
            Debug.Log("Started Pursuit");
            Follow();
        }
        else
        {
            if (transform.position != patrolPoints[currentPointIndex].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, MaxSpeedMove * Time.deltaTime);
            }
            else
            {
                if (once == false)
                {
                    Debug.Log("started waiting");
                    StartCoroutine(Wait());
                    once = true;
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        Debug.Log("waiting ended");
        once = false;
    }

    private void Follow()
    {
        //transform.rotation = Quaternion.LookRotation(target.transform.position);
        if (Vector3.Distance(transform.position, target.position) > minimumDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, MaxSpeedMove * Time.deltaTime);
        }
        if(Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            if(Time.time > _nextShotTime)
            {
                Attack();
                _nextShotTime = Time.time + _timeBetweenShooting;
            }
        }
    }

    private void Attack()
    {
        GameObject currentBullet = Instantiate(_bullet, _attackPoint.position, Quaternion.identity);

        var direction = FindObjectOfType<PlayerController>().transform.position;
        currentBullet.transform.forward = direction.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("NPC damage recived");
        CurrentHP -= damage;
        if (!CheckLiveEyntity())
        {
            _spawnEnemy.enemyCount--;
            Destroy(gameObject);
        };
    }

}
