using System.Collections;
using UnityEngine;

public class AIAttack : AISystems
{
    [Header("Preference shooting")]
    [SerializeField] private Transform positionSpawnShot;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootForce;
    [SerializeField] private GameObject target;
    public float _timeBetweenShooting;
    private float _nextShotTime = 0;
    public float distanceAttack;

    public void Shot()
    {
        GameObject currentBullet = Instantiate(_bullet, positionSpawnShot.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce(positionSpawnShot.transform.forward * _shootForce, ForceMode.Impulse);
    }

    public void ÑheckingAttackCondition()
    {
        if (AIStatusCurent == AIStatus.chase
            && agent.remainingDistance - agent.stoppingDistance < distanceAttack)
        {
            if (Time.time > _nextShotTime)
            {
                Shot();
                _nextShotTime = Time.time + _timeBetweenShooting;
            }
        }
    }
}