using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{
    [field: SerializeField] private List<Spawner> spawners = new List<Spawner>();
    public bool canUp = true;

    public void UpdateSpawners(int countLiveEntity, float speedSpawn)
    {
        for (int i = 0; i < spawners.Count; i++) 
        {
            spawners[i].countLiveEntity = countLiveEntity;
            spawners[i].speedSpawn = speedSpawn;
        }
    }

    private void FixedUpdate()
    {
        if (GameObject.Find("Player").GetComponent<PlayerController>().exp == 0 && canUp)
        {
            LevelUpEntity();
            canUp = false;
            Debug.Log(1111);
        }
    }

    public void LevelUpEntity()
    {
        var value = Random.Range(1, 10);
        var entitys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var entity in entitys)
        {
            var controller = entity.GetComponent<AIController>();
            controller.CurrentHP += value;
            controller.agent.speed += 0.2f;

            AIController.MaxHPAI += value;
            AIController.MaxSpeedAI += 0.2f;
        }
    }
}