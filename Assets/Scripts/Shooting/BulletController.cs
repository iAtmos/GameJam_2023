using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //assignables
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private LayerMask _whatIsEnemies;

    //Damage
    [SerializeField] private int _Damage;
    [SerializeField] private float _explosionRange;

    //lifetime
    [SerializeField] private int _MaxCollisions;
    [SerializeField] private float _maxLifeTime;

    //Sounds
    [SerializeField] private AudioSource _explosionSound;

    //References

    private int _collisions;
    private bool _exploaded = false;

    private void Update()
    {
        if (!_exploaded)
        {
            //When to explode:
            if (_collisions > _MaxCollisions)
            {
                Explode();
            }

            //Count down lifetime
            _maxLifeTime -= Time.deltaTime;
            if (_maxLifeTime < 0f)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        _exploaded = true;
        //Instantiate explosion
        if (_explosion != null)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
        Collider[] enemies = Physics.OverlapSphere(transform.position, _explosionRange, _whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {

        }
        //add Delay, to debug
        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //don't compare with other bullets
        if(collision.collider.CompareTag("Bullet"))
        {
            return;
        }

        //count up collisions
        _collisions++;

        //Explode if bullet has an enemy Directly and explodeOnTouch is activated
        if(collision.collider.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}