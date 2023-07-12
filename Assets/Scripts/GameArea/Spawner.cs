using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField] private GameObject prefabEntity;
    
    private List<GameObject> isLiveEntity = new List<GameObject>();
    [field: NonSerialized] public int countLiveEntity = 2;
    [field: NonSerialized] public float speedSpawn = 8f;
    private bool canSpawn = true;

    private void Update()
    {
        if (isLiveEntity.Count < countLiveEntity && canSpawn)
            SpawnEntity();
    }

    private void SpawnEntity() 
    {
        var entitu = Instantiate(prefabEntity, transform.position, transform.rotation);
        isLiveEntity.Add(entitu);
        
        canSpawn = false;
        StartCoroutine(RefreshSpawn());
    }

    private IEnumerator RefreshSpawn()
    {
        yield return new WaitForSeconds(speedSpawn);
        canSpawn = true;
    }

    public void DeathEntity(GameObject entity)
    {
        isLiveEntity.Remove(entity);
    }
}