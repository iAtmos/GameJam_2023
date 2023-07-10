using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class AiController : AbstractEntity
{
    public Transform target;
    public float minimumDistance;
    public float attackDistance;
    public float _shootForce;
    public float _timeBetweenShooting;
    private float _nextShotTime;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _attackPoint;


    private void Update()
    {
        Follow();
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
            Destroy(gameObject);
        };
    }

}
