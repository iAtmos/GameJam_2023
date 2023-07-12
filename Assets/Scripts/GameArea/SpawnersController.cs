using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{
    [field: SerializeField] private List<Spawner> spawners = new List<Spawner>();

    public void UpdateSpawners(int countLiveEntity, float speedSpawn)
    {
        for (int i = 0; i < spawners.Count; i++) 
        {
            spawners[i].countLiveEntity = countLiveEntity;
            spawners[i].speedSpawn = speedSpawn;
        }
    }
}