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

    private void Update()
    {
        //When to explode:
        if (_collisions > _MaxCollisions)
        {
            Debug.Log("Destroyed by collision");
            Destroy(gameObject);
        }

        //Count down lifetime
        _maxLifeTime -= Time.deltaTime;
        if (_maxLifeTime < 0f)
        {

            Debug.Log("Lifetime esceeded");
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        //Instantiate explosion
        if (_explosion != null)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
        Collider[] enemies = Physics.OverlapSphere(transform.position, _explosionRange, _whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("damage gived");
            enemies[i].GetComponent<AiController>().TakeDamage(_Damage);
        }
        Collider[] player = Physics.OverlapSphere(transform.position, _explosionRange, _whatIsEnemies);
        for(int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("damage gived");
            player[i].GetComponent<PlayerController>().TakeDamage(_Damage);
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
        //count up collisions
        _collisions++;

        //Explode if bullet has an enemy Directly and explodeOnTouch is activated
        if(collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hitted some Entity");
            Explode();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRange);
    }
}