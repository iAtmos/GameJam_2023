using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Spawner : MonoBehaviour
{
    [field: SerializeField] private GameObject prefabEntity;
    
    private int isLiveEntity;
    [field: NonSerialized] public int countLiveEntity = 2;
    [field: NonSerialized] public float speedSpawn = 8f;
    private bool canSpawn = true;

    private void Update()
    {
        if (isLiveEntity < countLiveEntity && canSpawn)
            SpawnEntity();
    }

    private void SpawnEntity() 
    {
        var enemy = Instantiate(prefabEntity, transform.position, transform.rotation);
        enemy.GetComponent<AIController>()._spawnEnemy = gameObject;
        isLiveEntity++;
  
        canSpawn = false;
        StartCoroutine(RefreshSpawn());
    }

    private IEnumerator RefreshSpawn()
    {
        yield return new WaitForSeconds(speedSpawn);
        canSpawn = true;
    }

    public void DeathEntity()
    {
        isLiveEntity--;
    }
}