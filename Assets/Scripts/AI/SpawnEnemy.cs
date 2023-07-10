using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;
    int spawnPoint;
    public float spawnTime;
    public int enemyCount = 0;
    public int enemyMax;

    private void Start()
    {
        StartCoroutine(EnemyDrop());
    }
    void Update()
    {
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount <= enemyMax)
        {
            spawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[spawnPoint].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
            enemyCount++;
        }
    }
}
